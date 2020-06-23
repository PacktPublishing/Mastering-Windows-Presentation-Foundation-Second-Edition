using System;
using System.Windows.Input;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.ViewModels.Commands;
using CompanyName.ApplicationName.ViewModels.Interfaces;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a pre-populated User object to the UI to demonstrate the use of View Model location.
    /// </summary>
    public class UserViewModel : BaseViewModel, IUserViewModel
    {
        private User user = new User(Guid.NewGuid(), "James Smith", 25) { Address = new Address() { HouseAndStreet = "147 Lucile Street", Town = "Somertown", City = "London", PostCode = "NW17 9RL", Country = "England" } };

        /// <summary>
        /// Gets or sets the User object to be used to demonstrate the use of View Model location with.
        /// </summary>
        public User User
        {
            get { return user; }
            set { user = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// The fictional save command to demonstrate the use of commanding with.
        /// </summary>
        public ICommand SaveCommand
        {
            get { return new ActionCommand(action => Save()); }
        }

        private void Save()
        {
            // Save User object
        }
    }
}