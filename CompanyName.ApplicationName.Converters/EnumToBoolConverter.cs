using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts an Enum value into a true boolean value if the Enum instance specified by the value input parameter matches the value of the parameter input parameter, or a false boolean value otherwise.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets a value that specifies whether the boolean output value is inverted during conversion or not.
        /// </summary>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Converts an Enum value into a true boolean value if the Enum instance specified by the value input parameter matches the value of the parameter input parameter, or a false boolean value otherwise.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A true boolean value if the Enum instance specified by the value input parameter matches the value of the parameter input parameter, or a false boolean value otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || (value.GetType() != typeof(Enum) && value.GetType().BaseType != typeof(Enum))) return DependencyProperty.UnsetValue;
            string enumValue = value.ToString();
            string targetValue = parameter.ToString();
            bool boolValue = enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
            return IsInverted ? !boolValue : boolValue;
        }

        /// <summary>
        /// Converts a true boolean value into the Enum instance specified by the parameter input parameter and a false boolean value into DependencyProperty.UnsetValue.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The Enum instance specified by the parameter input parameter if the input value is true, or DependencyProperty.UnsetValue otherwise.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return DependencyProperty.UnsetValue;
            bool boolValue = (bool)value;
            string targetValue = parameter.ToString();
            if ((boolValue && !IsInverted) || (!boolValue && IsInverted)) return Enum.Parse(targetType, targetValue);
            return DependencyProperty.UnsetValue;
        }
    }
}