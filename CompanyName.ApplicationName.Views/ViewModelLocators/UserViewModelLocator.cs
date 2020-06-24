using CompanyName.ApplicationName.ViewModels;
using CompanyName.ApplicationName.ViewModels.Interfaces;

namespace CompanyName.ApplicationName.Views.ViewModelLocators
{
    /// <summary>
    /// Supplies different User View Models at runtime and design time.
    /// </summary>
    public class UserViewModelLocator : BaseViewModelLocator<IUserViewModel>
    {
        /// <summary>
        /// Initializes a new UserViewModelLocator control with default values.
        /// </summary>
        public UserViewModelLocator()
        {
            DesignTimeViewModel = new UserViewModel();
        }
    }
}