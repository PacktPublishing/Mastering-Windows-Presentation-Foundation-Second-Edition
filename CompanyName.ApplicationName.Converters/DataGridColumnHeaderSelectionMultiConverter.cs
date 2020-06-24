using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts the two string input values that represent column header text to true if they are equal, or false otherwise.
    /// </summary>
    public class DataGridColumnHeaderSelectionMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the two string input values to true if they are equal, or false otherwise.
        /// </summary>
        /// <param name="values">The array of values that the source bindings in the System.Windows.Data.MultiBinding produces. The value System.Windows.DependencyProperty.UnsetValue indicates that the source binding has no value to provide for conversion.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string that represents an upward arrow if the current value is higher than the previous value, or a downward arrow otherwise.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Count() != 2 || values.Any(v => v == null || v == DependencyProperty.UnsetValue)) return false;
            string selectedColumnHeader = values[0].ToString();
            string columnHeaderToCompare = values[1].ToString();
            return selectedColumnHeader.Equals(columnHeaderToCompare);
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
            throw new NotImplementedException();
        }
    }
}