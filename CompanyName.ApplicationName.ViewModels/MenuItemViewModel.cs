using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Serves as the View Model for each menu item in the MenuView class.
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        private string header = string.Empty;
        private ICommand command = null;
        private ObservableCollection<MenuItemViewModel> menuItems = new ObservableCollection<MenuItemViewModel>();

        /// <summary>
        /// Gets or sets the value to be displayed in the menu item control in the View.
        /// </summary>
        public string Header
        {
            get { return header; }
            set { if (header != value) { header = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the command to be executed when the menu item control is clicked in the View.
        /// </summary>
        public ICommand Command
        {
            get { return command; }
            set { if (command != value) { command = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the collection of sub menu items to be displayed in the menu item control in the View.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get { return menuItems; }
            set { if (menuItems != value) { menuItems = value; NotifyPropertyChanged(); } }
        }
    }
}