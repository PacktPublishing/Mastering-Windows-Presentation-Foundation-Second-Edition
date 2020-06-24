using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CompanyName.ApplicationName.CustomControls.Enums;

namespace CompanyName.ApplicationName.CustomControls
{
    /// <summary>
    /// Represents a UI control that displays a colored sphere.
    /// </summary>
    [TemplatePart(Name = "PART_Background", Type = typeof(Ellipse))]
    [TemplatePart(Name = "PART_Glow", Type = typeof(Ellipse))]
    public class Sphere : Control
    {
        private RadialGradientBrush greenBackground = new RadialGradientBrush(new GradientStopCollection() { new GradientStop(System.Windows.Media.Color.FromRgb(0, 254, 0), 0), new GradientStop(System.Windows.Media.Color.FromRgb(1, 27, 0), 0.974) });
        private RadialGradientBrush greenGlow = new RadialGradientBrush(new GradientStopCollection() { new GradientStop(System.Windows.Media.Color.FromArgb(205, 67, 255, 46), 0), new GradientStop(System.Windows.Media.Color.FromArgb(102, 88, 254, 72), 0.426), new GradientStop(System.Windows.Media.Color.FromArgb(0, 44, 191, 32), 1) });
        private RadialGradientBrush redBackground = new RadialGradientBrush(new GradientStopCollection() { new GradientStop(System.Windows.Media.Color.FromRgb(254, 0, 0), 0), new GradientStop(System.Windows.Media.Color.FromRgb(27, 0, 0), 0.974) });
        private RadialGradientBrush redGlow = new RadialGradientBrush(new GradientStopCollection() { new GradientStop(System.Windows.Media.Color.FromArgb(205, 255, 46, 46), 0), new GradientStop(System.Windows.Media.Color.FromArgb(102, 254, 72, 72), 0.426), new GradientStop(System.Windows.Media.Color.FromArgb(0, 191, 32, 32), 1) });

        /// <summary>
        /// Specifies the default style key for this class.
        /// </summary>
        static Sphere()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Sphere), new FrameworkPropertyMetadata(typeof(Sphere)));
        }

        /// <summary>
        /// Represents the input value of the Sphere object that is used to size the control.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(Sphere), new PropertyMetadata(50.0));

        /// <summary>
        /// Gets or sets the input value of the Sphere object that is used to size the control.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Represents the SphereColor value that specifies the color to paint the Sphere object with.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(SphereColor), typeof(Sphere), new PropertyMetadata(SphereColor.Green, OnColorChanged));

        /// <summary>
        /// Gets or sets the SphereColor value that specifies the color to paint the Sphere object with.
        /// </summary>
        public SphereColor Color
        {
            get { return (SphereColor)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void OnColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ((Sphere)dependencyObject).SetEllipseColors();
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            SetEllipseColors();
        }

        private void SetEllipseColors()
        {
            Ellipse backgroundEllipse = GetTemplateChild("PART_Background") as Ellipse;
            Ellipse glowEllipse = GetTemplateChild("PART_Glow") as Ellipse;
            if (backgroundEllipse != null) backgroundEllipse.Fill = Color == SphereColor.Green ? greenBackground : redBackground;
            if (glowEllipse != null) glowEllipse.Fill = Color == SphereColor.Green ? greenGlow : redGlow;
        }
    }
}