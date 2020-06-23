using CompanyName.ApplicationName.Managers;
using CompanyName.ApplicationName.ViewModels.Interfaces;

namespace CompanyName.ApplicationName.Views.ViewModelLocators
{
    /// <summary>
    /// Supplies View Models that implement particular View Model interfaces.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Gets a View Model that implements the IUserViewModel interface.
        /// </summary>
        public IUserViewModel UserViewModel
        {
            get { return DependencyManager.Instance.Resolve<IUserViewModel>(); }
        }
    }
}