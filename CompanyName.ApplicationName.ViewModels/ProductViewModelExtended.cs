using System;
using System.ComponentModel;
using System.Linq;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a collection of validatable ProductExtended objects to be edited in the View.
    /// </summary>
    public class ProductViewModelExtended : BaseViewModel
    {
        private ProductsExtended products = new ProductsExtended();

        /// <summary>
        /// Initializes a new ProductViewModelExtended object with default values.
        /// </summary>
        public ProductViewModelExtended()
        {
            Products.Add(new ProductExtended() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset", Price = 14.99m });
            Products.Add(new ProductExtended() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset" });
            Products.CurrentItemChanged += Products_CurrentItemChanged;
            Products.CurrentItem = Products.Last();
            ValidateUniqueName(Products.CurrentItem);
        }

        /// <summary>
        /// Gets or sets the ProductsExtended collection of validatable ProductExtended objects to be edited in the View.
        /// </summary>
        public ProductsExtended Products
        {
            get { return products; }
            set { if (products != value) { products = value; NotifyPropertyChanged(); } }
        }

        private void Products_CurrentItemChanged(ProductExtended oldProduct, ProductExtended newProduct)
        {
            if (newProduct != null) newProduct.PropertyChanged += Product_PropertyChanged;
            if (oldProduct != null) oldProduct.PropertyChanged -= Product_PropertyChanged;
        }

        private void Product_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Products.CurrentItem.Name)) ValidateUniqueName(Products.CurrentItem);
        }

        private void ValidateUniqueName(ProductExtended product)
        {
            string errorMessage = "The product name must be unique.";
            if (!IsProductNameUnique(product)) product.ExternalErrors.Add(errorMessage);
            else product.ExternalErrors.Remove(errorMessage);
        }

        private bool IsProductNameUnique(ProductExtended product) => !Products.Any(p => p.Id != product.Id && !string.IsNullOrEmpty(p.Name) && p.Name == product.Name);
    }
}