using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents the basic details of a product in the application for use in large collections.
    /// </summary>
    public class ThinProduct : BaseDataModel
    {
        private Guid id = Guid.Empty;
        private string name = string.Empty;

        /// <summary>
        /// Initializes a new ThinProduct object with the values provided by the input parameter.
        /// </summary>
        public ThinProduct(AuditableProduct product)
        {
            Id = product.Id;
            Name = product.Name;
        }

        /// <summary>
        /// Initializes a new empty ThinProduct object.
        /// </summary>
        public ThinProduct() { }

        /// <summary>
        /// Gets or sets the unique identification number of the ThinProduct object.
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { if (id != value) { id = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the name of the ThinProduct object.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { if (name != value) { name = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}