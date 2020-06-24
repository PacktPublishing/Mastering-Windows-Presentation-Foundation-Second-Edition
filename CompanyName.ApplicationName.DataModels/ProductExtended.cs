using System;
using System.Collections.ObjectModel;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents a validatable product in the application.
    /// </summary>
    public class ProductExtended : BaseValidationModelExtended
    {
        private Guid id = Guid.Empty;
        private string name = string.Empty;
        private decimal price = 0;

        /// <summary>
        /// Gets or sets the unique identification number of the ProductExtended object.
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { if (id != value) { id = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the name of the ProductExtended object.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { if (name != value) { name = value; NotifyPropertyChanged(nameof(Name), nameof(Errors)); } }
        }

        /// <summary>
        /// Gets or sets the price of the ProductExtended object.
        /// </summary>
        public decimal Price
        {
            get { return price; }
            set { if (price != value) { price = value; NotifyPropertyChanged(nameof(Price), nameof(Errors)); } }
        }

        /// <summary>
        /// Gets a collection containing validation messages for this data model object if there are any validation errors.
        /// </summary>
        public override ObservableCollection<string> Errors
        {
            get
            {
                errors = new ObservableCollection<string>();
                errors.AddUniqueIfNotEmpty(this[nameof(Name)]);
                errors.AddUniqueIfNotEmpty(this[nameof(Price)]);
                errors.AddRange(ExternalErrors);
                return errors;
            }
        }

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