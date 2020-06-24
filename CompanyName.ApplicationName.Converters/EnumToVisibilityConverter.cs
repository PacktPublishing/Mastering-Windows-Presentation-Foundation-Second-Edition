using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts an Enum value into a System.Windows.Visibility value dependant on its value and the values of the FalseVisibilityState and IsInverted properties.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(Visibility))]
    public class EnumToVisibilityConverter : BaseVisibilityConverter, IValueConverter
    {
        /// <summary>
        /// Converts an Enum value into a System.Windows.Visibility value, dependant on its value and the values of the FalseVisibilityState and IsInverted properties.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter that determines the output of the IValueConverter.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A System.Windows.Visibility value, dependant on the value of the value input parameter and the FalseVisibilityState and IsInverted properties.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value.GetType() != typeof(Enum) && value.GetType().BaseType != typeof(Enum)) || parameter == null) return false;
            string enumValue = value.ToString();
            string targetValue = parameter.ToString();
            bool boolValue = enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
            boolValue = IsInverted ? !boolValue : boolValue;
            return boolValue ? Visibility.Visible : FalseVisibilityValue;
        }

        /// <summary>
        /// Converts a System.Windows.Visibility value into an Enum value, dependant on the value and parameter input parameters and the IsInverted property value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter that determines the output of the IValueConverter.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An Enum value, dependant on the value and parameter input parameters and the IsInverted property value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Visibility) || parameter == null) return null;
            Visibility usedValue = (Visibility)value;
            string targetValue = parameter.ToString();
            if (IsInverted && usedValue != Visibility.Visible) return Enum.Parse(targetType, targetValue);
            else if (!IsInverted && usedValue == Visibility.Visible) return Enum.Parse(targetType, targetValue);
            return DependencyProperty.UnsetValue;
        }
    }
}