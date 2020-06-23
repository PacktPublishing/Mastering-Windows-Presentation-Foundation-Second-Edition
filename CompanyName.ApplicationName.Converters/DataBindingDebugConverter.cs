using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Provides a way to debug problematic data bindings in the application.
    /// </summary>
    public class DataBindingDebugConverter : IValueConverter
    {
        /// <summary>
        /// Breaks execution whenever a data bound value is set or updated.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The unchanged input value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Debugger.IsAttached) Debugger.Break();
            return value;
        }

        /// <summary>
        /// Breaks execution whenever a data bound value is set or updated.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The unchanged input value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Debugger.IsAttached) Debugger.Break();
            return value;
        }
    }
}