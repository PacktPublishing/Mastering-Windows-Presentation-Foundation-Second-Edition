using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CompanyName.ApplicationName.CustomControls
{
    /// <summary>
    /// Provides an easy way to output a gauge as a graphical object.
    /// </summary>
    public class Gauge : Control
    {
        /// <summary>
        /// Overrides metadata of an existing DependencyProperty element and specifies the default style key for this class.
        /// </summary>
        static Gauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Gauge), new FrameworkPropertyMetadata(typeof(Gauge)));
            BackgroundProperty.OverrideMetadata(typeof(Gauge), new FrameworkPropertyMetadata(Brushes.Black));
        }

        /// <summary>
        /// Represents the read only angle of the input value in the arc of the Gauge object.
        /// </summary>
        public static readonly DependencyPropertyKey valueAnglePropertyKey = DependencyProperty.RegisterReadOnly(nameof(ValueAngle), typeof(double), typeof(Gauge), new PropertyMetadata(180.0));

        /// <summary>
        /// Represents the angle of the input value in the arc of the Gauge object.
        /// </summary>
        public static readonly DependencyProperty ValueAngleProperty = valueAnglePropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the angle of the input value in the arc of the Gauge object.
        /// </summary>
        public double ValueAngle
        {
            get { return (double)GetValue(ValueAngleProperty); }
            private set { SetValue(valueAnglePropertyKey, value); }
        }

        /// <summary>
        /// Represents the read only rotation angle of the needle of the Gauge object.
        /// </summary>
        public static readonly DependencyPropertyKey rotationAnglePropertyKey = DependencyProperty.RegisterReadOnly(nameof(RotationAngle), typeof(double), typeof(Gauge), new PropertyMetadata(180.0));

        /// <summary>
        /// Represents the rotation angle of the needle of the Gauge object.
        /// </summary>
        public static readonly DependencyProperty RotationAngleProperty = rotationAnglePropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the rotation angle of the needle of the Gauge object.
        /// </summary>
        public double RotationAngle
        {
            get { return (double)GetValue(RotationAngleProperty); }
            private set { SetValue(rotationAnglePropertyKey, value); }
        }

        /// <summary>
        /// Represents the input value of the Gauge object.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(Gauge), new PropertyMetadata(0.0, OnValueChanged));

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Gauge gauge = (Gauge)dependencyObject;
            if (gauge.MaximumValue == 0.0) gauge.ValueAngle = gauge.RotationAngle = 180.0;
            else if ((double)e.NewValue > gauge.MaximumValue)
            {
                gauge.ValueAngle = 0.0;
                gauge.RotationAngle = 360.0;
            }
            else
            {
                double scaledPercentageValue = ((double)e.NewValue / gauge.MaximumValue) * 180.0;
                gauge.ValueAngle = 180.0 - scaledPercentageValue;
                gauge.RotationAngle = 180.0 + scaledPercentageValue;
            }
        }

        /// <summary>
        /// Gets or sets the input value of the Gauge object.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Represents the maximum value of the Gauge object.
        /// </summary>
        public static readonly DependencyProperty MaximumValueProperty = DependencyProperty.Register(nameof(MaximumValue), typeof(double), typeof(Gauge), new PropertyMetadata(100.0));

        /// <summary>
        /// Gets or sets the maximum value of the Gauge object.
        /// </summary>
        public double MaximumValue
        {
            get { return (double)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        /// <summary>
        /// Represents the text title to display in the Gauge object.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(Gauge), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the text title to display in the Gauge object.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}