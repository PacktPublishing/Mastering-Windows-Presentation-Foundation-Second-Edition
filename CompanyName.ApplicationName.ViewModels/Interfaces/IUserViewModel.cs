using System.Windows.Input;
using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.ViewModels.Interfaces
{
    /// <summary>
    /// Provides data for the UserView class.
    /// </summary>
    public interface IUserViewModel
    {
        /// <summary>
        /// Gets or sets the User object in the related View.
        /// </summary>
        User User { get; set; }

        /// <summary>
        /// Gets or sets the ICommand object for the Save command in the related View.
        /// </summary>
        ICommand SaveCommand { get; }
    }
}
