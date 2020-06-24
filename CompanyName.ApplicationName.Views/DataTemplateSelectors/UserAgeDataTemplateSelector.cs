using System.Windows;
using System.Windows.Controls;
using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.Views.DataTemplateSelectors
{
    /// <summary>
    /// Provides a DataTemplateSelector element that selects a DataTemplate according to the age of a User object.
    /// </summary>
    public class UserAgeDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// When overridden in a derived class, returns a System.Windows.DataTemplate based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a System.Windows.DataTemplate or null. The default value is null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement frameworkElement = container as FrameworkElement;
            if (frameworkElement != null && item != null && item is User user)
            {
                if (user.Age < 35) return frameworkElement.FindResource("InverseUserTemplate") as DataTemplate;
                else return frameworkElement.FindResource("UserTemplate") as DataTemplate;
            }
            return null;
        }
    }
}