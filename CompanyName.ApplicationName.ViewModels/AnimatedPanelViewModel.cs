using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using CompanyName.ApplicationName.ViewModels.Commands;
using System;
using System.Linq;
using System.Windows.Input;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// A View Model that is paired with the AnimatedPanelView class, to demonstrate the AnimatedPanel control from the book.
    /// </summary>
    public class AnimatedPanelViewModel : BaseViewModel
    {
        private Products products;
        private Product selectedProduct;
        private int index = 0;

        /// <summary>
        /// Initializes a new AnimatedPanelViewModel object with default values.
        /// </summary>
        public AnimatedPanelViewModel()
        {
            products = new Products();
            for (index = 0; index < 5; index++) products.Add(new Product() { Id = Guid.NewGuid(), Name = $"Product { index + 1 }" });
        }

        /// <summary>
        /// Gets or sets the collection of Product objects to be used in the AnimatedPanel example in the View.
        /// </summary>
        public Products Products
        {
            get { return products; }
            set { products = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the selected Product to be used in the AnimatedPanel example in the View.
        /// </summary>
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the ICommand object that adds new Product instances to the Products collection in the AnimatedPanel example in the View.
        /// </summary>
        public ICommand AddProductCommand
        {
            get { return new ActionCommand(action => AddProduct()); }
        }

        /// <summary>
        /// Gets or sets the ICommand object that removes existing Product instances from the Products collection in the AnimatedPanel example in the View.
        /// </summary>
        public ICommand RemoveProductCommand
        {
            get { return new ActionCommand(action => RemoveProduct()); }
        }

        private void AddProduct()
        {
            products.Add(new Product() { Id = Guid.NewGuid(), Name = $"Product { ++index }" });
        }

        private void RemoveProduct()
        {
            if (products.Any()) products.Remove(products.First());
        }
    }
}