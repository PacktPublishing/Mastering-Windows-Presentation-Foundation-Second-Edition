using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts a System.Windows.Controls.DataGridRow object to the integer representation of its row number.
    /// </summary>
    [ValueConversion(typeof(DataGridRow), typeof(int))]
    public class DataGridRowToRowNumberConverter : IValueConverter
    {
        /// <summary>
        /// Converts a System.Windows.Controls.DataGridRow object to the integer representation of its row number.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An integer representation of the row number of the System.Windows.Controls.DataGridRow object specified by the value input parameter.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DataGridRow dataGridRow) return dataGridRow.GetIndex() + 1;
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}