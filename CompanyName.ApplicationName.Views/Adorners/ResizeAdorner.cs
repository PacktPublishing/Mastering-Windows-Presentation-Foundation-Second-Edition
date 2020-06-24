using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Views.Adorners
{
    /// <summary>
    /// Provides an Adorner object to display resize handles on adorned elements.
    /// </summary>
    public class ResizeAdorner : Adorner
    {
        private VisualCollection visualChildren;
        private Thumb top, left, bottom, right;

        /// <summary>
        /// Initializes a new ResizeAdorner object with the value from the input parameter.
        /// </summary>
        /// <param name="adornedElement">The UIElement object that represents the UI element to attach the Adorner control to.</param>
		public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);
            top = InitializeThumb(Cursors.SizeNS, Top_DragDelta);
            left = InitializeThumb(Cursors.SizeWE, Left_DragDelta);
            bottom = InitializeThumb(Cursors.SizeNS, Bottom_DragDelta);
            right = InitializeThumb(Cursors.SizeWE, Right_DragDelta);
        }

        private Thumb InitializeThumb(Cursor cursor, DragDeltaEventHandler eventHandler)
        {
            Thumb thumb = new Thumb();
            thumb.BorderBrush = Brushes.Black;
            thumb.BorderThickness = new Thickness(1);
            thumb.Cursor = cursor;
            thumb.DragDelta += eventHandler;
            thumb.Height = thumb.Width = 6.0;
            visualChildren.Add(thumb);
            return thumb;
        }

        private void Top_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = (FrameworkElement)AdornedElement;
            adornedElement.Height = Math.Max(adornedElement.Height - e.VerticalChange, 6);
            Canvas.SetTop(adornedElement, Canvas.GetTop(adornedElement) + e.VerticalChange);
        }

        private void Left_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = (FrameworkElement)AdornedElement;
            adornedElement.Width = Math.Max(adornedElement.Width - e.HorizontalChange, 6);
            Canvas.SetLeft(adornedElement, Canvas.GetLeft(adornedElement) + e.HorizontalChange);
        }

        private void Bottom_DragDelta(object sender, DragDeltaEventArgs e)
       { 
            FrameworkElement adornedElement = (FrameworkElement)AdornedElement;
            adornedElement.Height = Math.Max(adornedElement.Height + e.VerticalChange, 6);
       } 

        private void Right_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = (FrameworkElement)AdornedElement;
            adornedElement.Width = Math.Max(adornedElement.Width + e.HorizontalChange, 6);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);
            Pen pen = new Pen(new SolidColorBrush(Colors.DeepSkyBlue), 1.0);
            drawingContext.DrawRectangle(brush, pen, new Rect(-2, -2, AdornedElement.DesiredSize.Width + 4, AdornedElement.DesiredSize.Height + 4));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            top.Arrange(new Rect(AdornedElement.DesiredSize.Width / 2 - 3, -8, 6, 6));
            left.Arrange(new Rect(-8, AdornedElement.DesiredSize.Height / 2 - 3, 6, 6));
            bottom.Arrange(new Rect(AdornedElement.DesiredSize.Width / 2 - 3, AdornedElement.DesiredSize.Height + 2, 6, 6));
            right.Arrange(new Rect(AdornedElement.DesiredSize.Width + 2, AdornedElement.DesiredSize.Height / 2 - 3, 6, 6));
            return finalSize;
        }

        protected override int VisualChildrenCount
        {
            get { return visualChildren.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visualChildren[index];
        }
    }
}