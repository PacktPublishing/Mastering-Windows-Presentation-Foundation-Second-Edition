using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a validatable Product object to be edited in the View.
    /// </summary>
    public class ProductViewModel : BaseViewModel
    {
        private Product product = new Product();

        /// <summary>
        /// Gets or sets the validatable Product object to be edited in the View.
        /// </summary>
        public Product Product
        {
            get { return product; }
            set { if (product != value) { product = value; NotifyPropertyChanged(); } }
        }
    }
}