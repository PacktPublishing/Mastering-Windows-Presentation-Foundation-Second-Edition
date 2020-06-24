using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts the two string input values that represent row header text to true if they are equal, or false otherwise.
    /// </summary>
    public class DataGridRowHeaderSelectionMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the two string input values that represent row header text to true if they are equal, or false otherwise.
        /// </summary>
        /// <param name="values">The array of values that the source bindings in the System.Windows.Data.MultiBinding produces. The value System.Windows.DependencyProperty.UnsetValue indicates that the source binding has no value to provide for conversion.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string that represents an upward arrow if the current value is higher than the previous value, or a downward arrow otherwise.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Count() != 2 || !(values[0] is DataRow selectedDataRow) || !(values[1] is DataRow dataRowToCompare)) return false;
            return selectedDataRow.Equals(dataRowToCompare);
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