using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts a boolean value into a System.Windows.Visibility value dependant on the value of the FalseVisibilityState and IsInverted properties.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : BaseVisibilityConverter, IValueConverter
    {
        /// <summary>
        /// Converts a boolean value into a System.Windows.Visibility value dependant on the value of the FalseVisibilityState and IsInverted properties.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A System.Windows.Visibility value dependant on the value of the FalseVisibilityState and IsInverted properties.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(bool)) return null;
            bool boolValue = IsInverted ? !(bool)value : (bool)value;
            return boolValue ? Visibility.Visible : FalseVisibilityValue;
        }

        /// <summary>
        /// Converts a System.Windows.Visibility value into an boolean value dependant on the Visibility input value and the IsInverted property.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A boolean value dependant on the Visibility input value and the IsInverted property.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Visibility)) return null;
            if (IsInverted) return (Visibility)value != Visibility.Visible;
            return (Visibility)value == Visibility.Visible;
        }
    }
}