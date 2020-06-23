using CompanyName.ApplicationName.DataModels.Interfaces;
using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents a validatable product in the application.
    /// </summary>
    public class Product : BaseValidationModel, IAnimatable
    {
        private Guid id = Guid.Empty;
        private string name = string.Empty;
        private decimal price = 0;

        public Product()
        {
            Animatable = new Animatable(this);
        }

        /// <summary>
        /// Gets or sets the unique identification number of the Product object.
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { if (id != value) { id = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the name of the Product object.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { if (name != value) { name = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the price of the Product object.
        /// </summary>
        public decimal Price
        {
            get { return price; }
            set { if (price != value) { price = value; NotifyPropertyChanged(); } }
        }

        public Animatable Animatable { get; set; }

        /// <summary>
        /// Gets the validation message relating to the propertyName input parameter, if there are any validation errors.
        /// </summary>
        public override string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                if (propertyName == nameof(Name))
                {
                    if (string.IsNullOrEmpty(Name)) error = "Please enter the product name.";
                    else if (Name.Length > 25) error = "The product name cannot be longer than twenty-five characters.";
                }
                else if (propertyName == nameof(Price) && Price == 0) error = "Please enter a valid price for the product.";
                return error;
            }
        }
    }
}