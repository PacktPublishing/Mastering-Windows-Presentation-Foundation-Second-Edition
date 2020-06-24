using CompanyName.ApplicationName.DataModels.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts a FeedbackType enumeration member into an ImageSource object via the default string to ImageSource markup extension.
    /// </summary>
    [ValueConversion(typeof(FeedbackType), typeof(ImageSource))]
    public class FeedbackTypeToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts a FeedbackType enumeration member into an ImageSource object via the default string to ImageSource markup extension.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string file path to use as the ImageSource object via the default string to ImageSource markup extension if the input is valid, or DependencyProperty.UnsetValue otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is FeedbackType feedbackType) || targetType != typeof(ImageSource)) return DependencyProperty.UnsetValue;
            string imageName = string.Empty;
            switch (feedbackType)
            {
                case FeedbackType.None: return DependencyProperty.UnsetValue;
                case FeedbackType.Error: imageName = "Error_16"; break;
                case FeedbackType.Success: imageName = "Success_16"; break;
                case FeedbackType.Validation:
                case FeedbackType.Warning: imageName = "Warning_16"; break;
                case FeedbackType.Information: imageName = "Information_16"; break;
                case FeedbackType.Question: imageName = "Question_16"; break;
                default: return DependencyProperty.UnsetValue;
            }
            return $"pack://application:,,,/CompanyName.ApplicationName;component/Images/{ imageName }.png";
        }

        /// <summary>
        /// This method is not implemented.
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