using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CompanyName.ApplicationName.CustomControls
{
    /// <summary>
    /// Provides an easy way to output an arc as a graphical object that extends the System.Windows.Shapes.Shape class.
    /// </summary>
    public class Arc : Shape
    {
        /// <summary>
        /// Represents the angle of the start point of the arc.
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(Arc), new FrameworkPropertyMetadata(180.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the angle of the start point of the arc.
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        /// <summary>
        /// Represents the angle of the end point of the arc.
        /// </summary>
        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(Arc), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the angle of the end point of the arc.
        /// </summary>
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        /// <summary>
        /// Gets a value that represents the System.Windows.Media.Geometry of the System.Windows.Shapes.Shape.
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get { return GetArcGeometry(); }
        }

        private Geometry GetArcGeometry()
        {
            Point startPoint = ConvertToPoint(Math.Min(StartAngle, EndAngle));
            Point endPoint = ConvertToPoint(Math.Max(StartAngle, EndAngle));
            Size arcSize = new Size(Math.Max(0, (RenderSize.Width - StrokeThickness) / 2), Math.Max(0, (RenderSize.Height - StrokeThickness) / 2));
            bool isLargeArc = Math.Abs(EndAngle - StartAngle) > 180;
            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext context = streamGeometry.Open())
            {
                context.BeginFigure(startPoint, false, false);
                context.ArcTo(endPoint, arcSize, 0, isLargeArc, SweepDirection.Counterclockwise, true, false);
            }
            streamGeometry.Transform = new TranslateTransform(StrokeThickness / 2, StrokeThickness / 2);
            return streamGeometry;
        }

        private Point ConvertToPoint(double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * Math.PI / 180;
            double radiusX = (RenderSize.Width - StrokeThickness) / 2;
            double radiusY = (RenderSize.Height - StrokeThickness) / 2;
            return new Point(radiusX * Math.Cos(angleInRadians) + radiusX, radiusY * Math.Sin(-angleInRadians) + radiusY);
        }
    }
}