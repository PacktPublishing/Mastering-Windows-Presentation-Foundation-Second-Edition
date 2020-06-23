using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Enums;
using System;
using System.Collections.ObjectModel;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// A View Model that is paired with the AllProductsView class, to display the various Product-related examples from the book.
    /// </summary>
    public class AllProductsViewModel : BaseViewModel
    {
        private BaseViewModel viewModel;
        private PageModel activePage = null;
        private ObservableCollection<PageModel> pages = null;

        /// <summary>
        /// Initializes a new AllProductsViewModel with default values.
        /// </summary>
        public AllProductsViewModel() : base()
        {
            PopulateViewModels();
            ViewModel = new ProductViewModel();
        }

        /// <summary>
        /// Gets or sets the BaseViewModel object that is currently displayed in the application.
        /// </summary>
        public BaseViewModel ViewModel
        {
            get { return viewModel; }
            set { if (viewModel != value) { viewModel = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the collection of Product-related PageModel objects that relate to views to display in the application.
        /// </summary>
        public ObservableCollection<PageModel> Pages
        {
            get { return pages; }
            set { if (pages != value) { pages = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the selected Product-related PageModel instance from the collection of PageModel objects that relate to views to display in the application.
        /// </summary>
        public PageModel ActivePage
        {
            get { return activePage; }
            set
            {
                if (activePage != value)
                {
                    activePage = value;
                    NotifyPropertyChanged();

                    if (activePage != null) ViewModel = Activator.CreateInstance(activePage.Type) as BaseViewModel;
                }
            }
        }

        private void PopulateViewModels()
        {
            pages = new ObservableCollection<PageModel>();
            pages.Add(new PageModel(typeof(ProductViewModel), Page.ProductViewModel, Chapter.Four));
            pages.Add(new PageModel(typeof(ProductViewModelExtended), Page.ProductViewModelExtended, Chapter.Four));
            pages.Add(new PageModel(typeof(ProductNotifyViewModel), Page.ProductNotifyViewModel, Chapter.Four));
            pages.Add(new PageModel(typeof(ProductNotifyViewModelExtended), Page.ProductNotifyViewModelExtended, Chapter.Four));
            pages.Add(new PageModel(typeof(ErrorTemplateViewModel), Page.ErrorTemplate, Chapter.Four));
            pages.Add(new PageModel(typeof(ProductNotifyViewModelGeneric), Page.ProductNotifyViewModelGeneric, Chapter.Four));
            ActivePage = pages[0];
        }
    }
}