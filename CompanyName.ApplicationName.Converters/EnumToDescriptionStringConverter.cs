using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts an Enum instance into the content of its System.ComponentModel.DescriptionAttribute if one is set, or its name otherwise.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumToDescriptionStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts an Enum instance into the content of its System.ComponentModel.DescriptionAttribute if one is set, or its name otherwise.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The content of the System.ComponentModel.DescriptionAttribute that relates to the Enum instance specified by the vale input parameter if one is set, or its name otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value.GetType() != typeof(Enum) && value.GetType().BaseType != typeof(Enum))) return false;
            Enum enumInstance = (Enum)value;
            return enumInstance.GetDescription();
        }

        /// <summary>
        /// Returns DependencyProperty.UnsetValue.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>DependencyProperty.UnsetValue.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}