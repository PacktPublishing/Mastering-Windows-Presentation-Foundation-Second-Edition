using System;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.ApplicationName.Views.Panels
{
    /// <summary>
    /// Evenly arranges the child objects around the circumference of an invisible circle positioned at the bounds of the System.Windows.Controls.Panel.
    /// </summary>
    public class CircumferencePanel : Panel
    {
        /// <summary>
        /// Gets or sets a Thickness struct representing the padding to add to the circumference of the invisible circle that the child objects are evenly arranged around.
        /// </summary>
        public Thickness Padding { get; set; }

        /// <summary>
        /// Measures the size in layout required for child elements and determines a size for the System.Windows.FrameworkElement-derived class.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement element in Children)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }
            return availableSize;
        }

        /// <summary>
        /// Positions child elements and determines a size for a System.Windows.FrameworkElement-derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used by the CircumferencePanel.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0) return finalSize;
            double currentAngle = 90 * (Math.PI / 180);
            double radiansPerElement = (360 / Children.Count) * (Math.PI / 180.0);
            double radiusX = finalSize.Width / 2.0 - Padding.Left;
            double radiusY = finalSize.Height / 2.0 - Padding.Top;
            foreach (UIElement element in Children)
            {
                // Calculate the point on the circle for the element
                Point childPoint = new Point(Math.Cos(currentAngle) * radiusX, -Math.Sin(currentAngle) * radiusY);
                // Offsetting the point to the avalable rectangular area (finalSize).
                Point centeredChildPoint = new Point(childPoint.X + finalSize.Width / 2 - element.DesiredSize.Width / 2, childPoint.Y + finalSize.Height / 2 - element.DesiredSize.Height / 2);
                Rect boundingBox = new Rect(centeredChildPoint, element.DesiredSize);
                element.Arrange(boundingBox);
                currentAngle -= radiansPerElement;
            }
            return finalSize;
        }
    }
}