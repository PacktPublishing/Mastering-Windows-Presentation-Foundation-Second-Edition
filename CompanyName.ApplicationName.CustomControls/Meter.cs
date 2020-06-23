using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CompanyName.ApplicationName.CustomControls
{
    /// <summary>
    /// Represents a UI control that displays an input value in a colorful meter.
    /// </summary>
    [TemplatePart(Name = "PART_Rectangle", Type = typeof(Rectangle))]
    public class Meter : Control
    {
        /// <summary>
        /// Specifies the default style key for this class.
        /// </summary>
        static Meter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Meter), new FrameworkPropertyMetadata(typeof(Meter)));
        }

        /// <summary>
        /// Represents the input value of the Meter object to display in the control.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(Meter), new PropertyMetadata(0.0, OnValueChanged, CoerceValue));

        private static object CoerceValue(DependencyObject dependencyObject, object value)
        {
            return Math.Min(Math.Max((double)value, 0.0), 1.0);
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Meter meter = (Meter)dependencyObject;
            meter.SetClipRect(meter);
        }

        /// <summary>
        /// Gets or sets the input value of the Meter object to display in the control.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Represents the read only Rect element that is internally used in the Meter object.
        /// </summary>
        public static readonly DependencyPropertyKey clipRectPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ClipRect), typeof(Rect), typeof(Meter), new PropertyMetadata(new Rect()));

        /// <summary>
        /// Represents the Rect element that is internally used in the Meter object.
        /// </summary>
        public static readonly DependencyProperty ClipRectProperty = clipRectPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the Rect element that is internally used in the Meter object.
        /// </summary>
        public Rect ClipRect
        {
            get { return (Rect)GetValue(ClipRectProperty); }
            private set { SetValue(clipRectPropertyKey, value); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            SetClipRect(this);
            Rectangle rectangle = (Rectangle)Template.FindName("PART_Rectangle", this);
            if (rectangle != null)
            {
                // Do something with rectangle
            }
        }

        private void SetClipRect(Meter meter)
        {
            double barSize = meter.Value * meter.Height;
            meter.ClipRect = new Rect(0, meter.Height - barSize, meter.Width, barSize);
        }
    }
}