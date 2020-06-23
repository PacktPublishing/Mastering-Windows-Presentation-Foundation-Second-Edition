using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides an easy way to output a smiley face as a graphical object, extending a UIElement object.
    /// </summary>
    public class SmileyFaceUIElement : UIElement
    {
        private VisualCollection visuals;

        /// <summary>
        /// Initializes a new empty SmileyFaceVisual control with default values.
        /// </summary>
        public SmileyFaceUIElement()
        {
            visuals = new VisualCollection(this);
            visuals.Add(GetFaceDrawingVisual());
        }

        private DrawingVisual GetFaceDrawingVisual()
        {
            RadialGradientBrush radialGradientBrush = new RadialGradientBrush(Colors.Yellow, Colors.Orange);
            radialGradientBrush.RadiusX = 0.8;
            radialGradientBrush.RadiusY = 0.8;
            radialGradientBrush.Freeze();
            Pen outerPen = new Pen(Brushes.Black, 5.25);
            outerPen.Freeze();
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawEllipse(radialGradientBrush, outerPen, new Point(75, 75), 72.375, 72.375);
            drawingContext.DrawEllipse(Brushes.Black, null, new Point(44.25, 49.5), 10.125, 12.75);
            drawingContext.DrawEllipse(Brushes.Black, null, new Point(105.75, 49.5), 10.125, 12.75);
            ArcSegment arcSegment = new ArcSegment(new Point(115.5, 93.75), new Size(61.5, 61.5), 0, false, SweepDirection.Counterclockwise, true);
            PathFigure pathFigure = new PathFigure(new Point(34.5, 93.75), new List<PathSegment>() { arcSegment }, false);
            PathGeometry pathGeometry = new PathGeometry(new List<PathFigure>() { pathFigure });
            pathGeometry.Freeze();
            Pen smilePen = new Pen(Brushes.Black, 10.5);
            smilePen.StartLineCap = PenLineCap.Round;
            smilePen.EndLineCap = PenLineCap.Round;
            smilePen.Freeze();
            drawingContext.DrawGeometry(null, smilePen, pathGeometry);
            drawingContext.Close();
            return drawingVisual;
        }

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        /// <summary>
        /// Overrides System.Windows.Media.Visual.GetVisualChild(System.Int32), and returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        /// <returns>The requested child element. This should not return null; if the provided index is out of range, an exception is thrown.</returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= visuals.Count) throw new ArgumentOutOfRangeException();
            return visuals[index];
        }
    }
}