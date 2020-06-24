using System;
using System.ComponentModel;
using System.Linq;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using System.Windows.Input;
using CompanyName.ApplicationName.ViewModels.Commands;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a collection of validatable ProductNotifyGeneric objects to be edited in the View.
    /// </summary>
    public class ProductNotifyViewModelGeneric : BaseViewModel
    {
        private ProductsNotifyGeneric products = new ProductsNotifyGeneric();

        /// <summary>
        /// Initializes a new ProductNotifyViewModelGeneric object with default values.
        /// </summary>
        public ProductNotifyViewModelGeneric()
        {
            Products.Add(new ProductNotifyGeneric() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset", Price = 14.99m });
            Products.Add(new ProductNotifyGeneric() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset" });
            Products.Synchronize();
            Products.CurrentItemChanged += Products_CurrentItemChanged;
            Products.CurrentItem = Products.Last();
            Products.CurrentItem.Validate(nameof(Products.CurrentItem.Name), nameof(Products.CurrentItem.Price));
            ValidateUniqueName(Products.CurrentItem);
        }

        /// <summary>
        /// Gets or sets the ProductsNotifyGeneric collection of validatable ProductNotifyGeneric objects to be edited in the View.
        /// </summary>
        public ProductsNotifyGeneric Products
        {
            get { return products; }
            set { if (products != value) { products = value; NotifyPropertyChanged(); } }
        }

        private void Products_CurrentItemChanged(ProductNotifyGeneric oldProduct, ProductNotifyGeneric newProduct)
        {
            if (newProduct != null) newProduct.PropertyChanged += Product_PropertyChanged;
            if (oldProduct != null) oldProduct.PropertyChanged -= Product_PropertyChanged;
        }

        private void Product_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Products.CurrentItem.Name)) ValidateUniqueName(Products.CurrentItem);
        }

        private void ValidateUniqueName(ProductNotifyGeneric product)
        {
            string errorMessage = "The product name must be unique.";
            if (!IsProductNameUnique(product)) product.ExternalErrors.Add(errorMessage);
            else product.ExternalErrors.Remove(errorMessage);
        }

        private bool IsProductNameUnique(ProductNotifyGeneric product) => Products.Count(p => p.Id != product.Id && p.Name != string.Empty && p.Name == product.Name) == 0;

        public ICommand DeleteCommand
        {
            get { return new ActionCommand(action => Delete(action), canExecute => CanDelete(canExecute)); }
        }

        private bool CanDelete(object parameter)
        {
            return Products.Contains((ProductNotifyGeneric)parameter);
        }

        private void Delete(object parameter)
        {
            if (CanDelete(parameter)) Products.Remove((ProductNotifyGeneric)parameter);
        }
    }
}