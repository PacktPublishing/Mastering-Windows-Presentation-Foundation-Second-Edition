using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CompanyName.ApplicationName.DataModels.Enums;
using Animatable = CompanyName.ApplicationName.DataModels.Animatable;
using IAnimatable = CompanyName.ApplicationName.DataModels.Interfaces.IAnimatable;

namespace CompanyName.ApplicationName.Views.Panels
{
    /// <summary>
    /// Animates the transition of child objects in the style of a StackPanel and can be used in an ItemsPanelTemplate as the ItemsPanel in a number of collection controls.
    /// </summary>
    public class AnimatedStackPanel : Panel
    {
        private List<UIElement> elementsToBeRemoved = new List<UIElement>();

        /// <summary>
        /// A System.Windows.Controls.Orientation value that indicates the dimension by which child elements are stacked.
        /// </summary>
        public static DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(AnimatedStackPanel), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// Gets or sets a System.Windows.Controls.Orientation value that indicates the dimension by which child elements are stacked.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Measures the size in layout required for child elements and determines a size for the System.Windows.FrameworkElement-derived class.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            double x = 0, y = 0;
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
                if (Orientation == Orientation.Horizontal)
                {
                    x += child.DesiredSize.Width;
                    y = Math.Max(y, child.DesiredSize.Height);
                }
                else
                {
                    x = Math.Max(x, child.DesiredSize.Width);
                    y += child.DesiredSize.Height;
                }
            }
            return new Size(x, y);
        }

        /// <summary>
        /// Positions child elements and determines a size for a System.Windows.FrameworkElement-derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used by the AnimatedStackPanel.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Point endPosition = new Point();
            foreach (UIElement child in Children)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    child.Arrange(new Rect(-child.DesiredSize.Width, 0, child.DesiredSize.Width, finalSize.Height));
                    endPosition.X += child.DesiredSize.Width;
                }
                else
                {
                    child.Arrange(new Rect(0, -child.DesiredSize.Height, finalSize.Width, child.DesiredSize.Height));
                    endPosition.Y += child.DesiredSize.Height;
                }
                BeginAnimations(child, finalSize, endPosition);
            }
            return finalSize;
        }

        private void BeginAnimations(UIElement child, Size finalSize, Point endPosition)
        {
            FrameworkElement frameworkChild = (FrameworkElement)child;
            if (frameworkChild.DataContext is IAnimatable)
            {
                Animatable animatable = ((IAnimatable)frameworkChild.DataContext).Animatable;
                animatable.OnRemovalStatusChanged -= Item_OnRemovalStatusChanged;
                animatable.OnRemovalStatusChanged += Item_OnRemovalStatusChanged;
                if (animatable.AdditionStatus == AdditionStatus.DoNotAnimate)
                {
                    child.Arrange(new Rect(endPosition.X, endPosition.Y, frameworkChild.ActualWidth, frameworkChild.ActualHeight));
                }
                else if (animatable.AdditionStatus == AdditionStatus.ReadyToAnimate)
                {
                    AnimateEntry(child, endPosition);
                    animatable.AdditionStatus = AdditionStatus.Added;
                    animatable.TransitionStatus = TransitionStatus.ReadyToAnimate;
                }
                else if (animatable.RemovalStatus == RemovalStatus.ReadyToAnimate) AnimateExit(child, endPosition, finalSize);
                else if (animatable.TransitionStatus == TransitionStatus.ReadyToAnimate) AnimateTransition(child, endPosition);
            }
        }

        private void Item_OnRemovalStatusChanged(object sender, EventArgs e)
        {
            if (((Animatable)sender).RemovalStatus == RemovalStatus.ReadyToAnimate) InvalidateArrange();
        }

        private void AnimateEntry(UIElement child, Point endPosition)
        {
            AnimatePosition(child, endPosition, TimeSpan.FromMilliseconds(300));
        }

        private void AnimateTransition(UIElement child, Point endPosition)
        {
            AnimatePosition(child, endPosition, TimeSpan.FromMilliseconds(300));
        }

        private void AnimateExit(UIElement child, Point startPosition, Size finalSize)
        {
            SetZIndex(child, 100);
            Point endPosition = new Point(startPosition.X + finalSize.Width, startPosition.Y);
            AnimatePosition(child, startPosition, endPosition, TimeSpan.FromMilliseconds(300), RemovalAnimation_Completed);
            elementsToBeRemoved.Add(child);
        }

        private void AnimatePosition(UIElement child, Point endPosition, TimeSpan animationDuration)
        {
            if (Orientation == Orientation.Vertical) GetTranslateTransform(child).BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(endPosition.Y, animationDuration));
            else GetTranslateTransform(child).BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(endPosition.X, animationDuration));
        }

        private void AnimatePosition(UIElement child, Point startPosition, Point endPosition, TimeSpan animationDuration, EventHandler animationCompletedHandler)
        {
            if (startPosition.X != endPosition.X)
            {
                DoubleAnimation xAnimation = new DoubleAnimation(startPosition.X, endPosition.X, animationDuration);
                xAnimation.AccelerationRatio = 1.0;
                if (animationCompletedHandler != null) xAnimation.Completed += animationCompletedHandler;
                GetTranslateTransform(child).BeginAnimation(TranslateTransform.XProperty, xAnimation);
            }
            if (startPosition.Y != endPosition.Y)
            {
                DoubleAnimation yAnimation = new DoubleAnimation(startPosition.Y, endPosition.Y, animationDuration);
                yAnimation.AccelerationRatio = 1.0;
                if (startPosition.X == endPosition.X && animationCompletedHandler != null) yAnimation.Completed += animationCompletedHandler;
                GetTranslateTransform(child).BeginAnimation(TranslateTransform.YProperty, yAnimation);
            }
        }

        private void RemovalAnimation_Completed(object sender, EventArgs e)
        {
            for (int index = elementsToBeRemoved.Count - 1; index >= 0; index--)
            {
                FrameworkElement frameworkElement = elementsToBeRemoved[index] as FrameworkElement;
                if (frameworkElement.DataContext is IAnimatable)
                {
                    ((IAnimatable)frameworkElement.DataContext).Animatable.RemovalStatus = RemovalStatus.ReadyToRemove;
                    elementsToBeRemoved.Remove(frameworkElement);
                }
            }
        }

        private TranslateTransform GetTranslateTransform(UIElement child)
        {
            return child.RenderTransform as TranslateTransform ?? AddTranslateTransform(child);
        }

        private TranslateTransform AddTranslateTransform(UIElement child)
        {
            TranslateTransform translateTransform = new TranslateTransform();
            child.RenderTransform = translateTransform;
            return translateTransform;
        }
    }
}