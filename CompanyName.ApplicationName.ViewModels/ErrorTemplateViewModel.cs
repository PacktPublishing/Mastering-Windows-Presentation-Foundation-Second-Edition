using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a validatable ProductNotifyExtended object to highlight the validation error template.
    /// </summary>
    public class ErrorTemplateViewModel : BaseViewModel
    {
        private ProductNotifyExtended product = new ProductNotifyExtended();

        /// <summary>
        /// Initializes a new ErrorTemplateViewModel object.
        /// </summary>
        public ErrorTemplateViewModel()
        {
            Product.ValidateAllProperties();
        }

        /// <summary>
        /// Gets or sets the validatable ProductNotifyExtended object to be edited in the View.
        /// </summary>
        public ProductNotifyExtended Product
        {
            get { return product; }
            set { if (product != value) { product = value; NotifyPropertyChanged(); } }
        }
    }
}