using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts the two integer values of the input into a string that represents an upward arrow if the current value is higher than the previous value, or a downward arrow otherwise.
    /// </summary>
    public class HigherLowerConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the two integer values of the input into a string that represents an upward or downward arrow, depending if the current value is higher or lower than the previous value.
        /// </summary>
        /// <param name="values">The array of values that the source bindings in the System.Windows.Data.MultiBinding produces. The value System.Windows.DependencyProperty.UnsetValue indicates that the source binding has no value to provide for conversion.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string that represents an upward arrow if the current value is higher than the previous value, or a downward arrow otherwise.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2 || !(values[0] is int currentValue) || !(values[1] is int previousValue)) return DependencyProperty.UnsetValue;
            return currentValue > previousValue ? "->" : "<-";
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An array of values that have been converted from the target value back to the source values.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[2] { DependencyProperty.UnsetValue, DependencyProperty.UnsetValue };
        }
    }
}