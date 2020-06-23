using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CompanyName.ApplicationName.Views.Panels
{
    /// <summary>
    /// Animates the entry transition of child objects in the style of a StackPanel and can be used in an ItemsPanelTemplate as the ItemsPanel in a number of collection controls.
    /// </summary>
    public class AnimatedEntryOnlyStackPanel : Panel
    {
        /// <summary>
        /// A System.Windows.Controls.Orientation value that indicates the dimension by which child elements are stacked.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(AnimatedEntryOnlyStackPanel), new PropertyMetadata(Orientation.Vertical));

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
        /// <returns>The actual size used by the AnimatedEntryOnlyStackPanel.</returns>
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
                AnimatePosition(child, endPosition, TimeSpan.FromMilliseconds(300));
            }
            return finalSize;
        }

        private void AnimatePosition(UIElement child, Point endPosition, TimeSpan animationDuration)
        {
            if (Orientation == Orientation.Vertical) GetTranslateTransform(child).BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(endPosition.Y, animationDuration));
            else GetTranslateTransform(child).BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(endPosition.X, animationDuration));
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