using System.Collections.ObjectModel;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Serves the menu item data for the MenuView class.
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        private ObservableCollection<MenuItemViewModel> menuItems = new ObservableCollection<MenuItemViewModel>();

        /// <summary>
        /// Initializes a new MenuViewModel with default values.
        /// </summary>
        public MenuViewModel()
        {
            MenuItems.Add(new MenuItemViewModel()
            {
                Header = "Users",
                MenuItems = new ObservableCollection<MenuItemViewModel>() { new MenuItemViewModel() { Header = "Details", MenuItems = new ObservableCollection<MenuItemViewModel>() { new MenuItemViewModel() { Header = "Banking" }, new MenuItemViewModel() { Header = "Personal" } } }, new MenuItemViewModel() { Header = "Security" } }
            });
            MenuItems.Add(new MenuItemViewModel() { Header = "Administration" });
            MenuItems.Add(new MenuItemViewModel() { Header = "View" });
            MenuItems.Add(new MenuItemViewModel()
            {
                Header = "Help",
                MenuItems = new ObservableCollection<MenuItemViewModel>() { new MenuItemViewModel() { Header = "About" } }
            });
        }

        /// <summary>
        /// Gets or sets the collection of MenuItemViewModel objects to be displayed in the View.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get { return menuItems; }
            set { if (menuItems != value) { menuItems = value; NotifyPropertyChanged(); } }
        }
    }
}
