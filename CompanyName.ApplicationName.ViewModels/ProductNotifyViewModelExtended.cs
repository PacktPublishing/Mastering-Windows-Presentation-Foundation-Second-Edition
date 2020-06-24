using System;
using System.Linq;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a collection of validatable ProductNotifyExtended objects to be edited in the View.
    /// </summary>
    public class ProductNotifyViewModelExtended : BaseViewModel
    {
        private ProductsNotifyExtended products = new ProductsNotifyExtended();

        /// <summary>
        /// Initializes a new ProductNotifyViewModelExtended object with default values.
        /// </summary>
        public ProductNotifyViewModelExtended()
        {
            Products.Add(new ProductNotifyExtended() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset", Price = 14.99m });
            Products.Add(new ProductNotifyExtended() { Id = Guid.NewGuid(), Name = "super virtual reality headset", Price = 49.99m });
            Products.CurrentItem = Products.Last();
            Products.CurrentItem.Validate(nameof(Products.CurrentItem.Name));
            Products.CurrentItem.Validate(nameof(Products.CurrentItem.Price));
        }

        /// <summary>
        /// Gets or sets the ProductsNotifyExtended collection of validatable ProductNotifyExtended objects to be edited in the View.
        /// </summary>
        public ProductsNotifyExtended Products
        {
            get { return products; }
            set { if (products != value) { products = value; NotifyPropertyChanged(); } }
        }
    }
}