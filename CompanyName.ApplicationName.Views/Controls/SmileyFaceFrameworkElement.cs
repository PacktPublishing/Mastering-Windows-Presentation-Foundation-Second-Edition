using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides an easy way to output a smiley face as a graphical object, extending a FrameworkElement object.
    /// </summary>
    public class SmileyFaceFrameworkElement : FrameworkElement
    {
        private VisualCollection visuals;

        /// <summary>
        /// Initializes a new empty SmileyFaceVisual control with default values.
        /// </summary>
        public SmileyFaceFrameworkElement()
        {
            visuals = new VisualCollection(this);
            Loaded += SmileyFace_Loaded;
        }

        private void SmileyFace_Loaded(object sender, RoutedEventArgs e)
        {
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
            drawingContext.DrawEllipse(radialGradientBrush, outerPen, new Point(ActualWidth / 2, ActualHeight / 2), (ActualWidth - outerPen.Thickness) / 2, (ActualHeight - outerPen.Thickness) / 2);
            drawingContext.DrawEllipse(Brushes.Black, null, new Point(ActualWidth / 3.3898305084745761, ActualHeight / 3.0303030303030303), ActualWidth / 14.814814814814815, ActualHeight / 11.764705882352942);
            drawingContext.DrawEllipse(Brushes.Black, null, new Point(ActualWidth / 1.4184397163120568, ActualHeight / 3.0303030303030303), ActualWidth / 14.814814814814815, ActualHeight / 11.764705882352942);
            ArcSegment arcSegment = new ArcSegment(new Point(ActualWidth / 1.2987012987012987, ActualHeight / 1.6), new Size(ActualWidth / 2.4390243902439024, ActualHeight / 2.4390243902439024), 0, false, SweepDirection.Counterclockwise, true);
            PathFigure pathFigure = new PathFigure(new Point(ActualWidth / 4.3478260869565215, ActualHeight / 1.6), new List<PathSegment>() { arcSegment }, false);
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