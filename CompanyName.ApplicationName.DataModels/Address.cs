namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Proivides properties that describe an address.
    /// </summary>
    public class Address : BaseDataModel
    {
        private string houseAndStreet, town, city, postCode, country;

        /// <summary>
        /// Gets or sets the house name or number and the name of the street part of the Address object.
        /// </summary>
        public string HouseAndStreet
        {
            get { return houseAndStreet; }
            set { if (houseAndStreet != value) { houseAndStreet = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the town portion of the Address object.
        /// </summary>
        public string Town
        {
            get { return town; }
            set { if (town != value) { town = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the city portion of the Address object.
        /// </summary>
        public string City
        {
            get { return city; }
            set { if (city != value) { city = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the postcode portion of the Address object.
        /// </summary>
        public string PostCode
        {
            get { return postCode; }
            set { if (postCode != value) { postCode = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the country portion of the Address object.
        /// </summary>
        public string Country
        {
            get { return country; }
            set { if (country != value) { country = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{HouseAndStreet}, {Town}, {City}, {PostCode}, {Country}";
        }
    }
}