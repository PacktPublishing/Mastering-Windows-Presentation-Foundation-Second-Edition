using System;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.Models.Interfaces;
using CompanyName.ApplicationName.DataModels.Collections;

namespace Test.CompanyName.ApplicationName.Models.DataProviders
{
    public class MockDataProvider : IDataProvider
    {
        /// <summary>
        /// Adds the User object specified by the user input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="user">The User object to add to the data source.</param>
        /// <returns>True if the data operation was successful, or false otherwise.</returns>
        public bool AddUser(User user)
        {
            return true;
        }

        /// <summary>
        /// Gets the User object that relates to the specified id input parameter from the data source.
        /// </summary>
        /// <param name="id">The unique identification number of the User object to retrieve from the data source.</param>
        /// <returns>A User object that relates to the specified id input parameter, or null otherwise.</returns>
        public User GetUser(Guid id)
        {
            return new User(id, "James Smith", 25);
        }

        /// <summary>
        /// Saves the User object specified by the user input parameter in the data source.
        /// </summary>
        /// <param name="user">The User object to update in the data source.</param>
        /// <returns>True if the data operation was successful, or false otherwise.</returns>
        public bool SaveUser(User user)
        {
            return true;
        }

        /// <summary>
        /// Adds the AuditableProduct object specified by the product input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="product">The AuditableProduct object to add to the data source.</param>
        public void AddProduct(AuditableProduct user) { }

        /// <summary>
        /// Deletes the AuditableProduct object specified by the product input parameter from the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to remove from the data source.</param>
        public void DeleteProduct(AuditableProduct user) { }

        /// <summary>
        /// Updates the AuditableProduct object specified by the product input parameter in the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to update in the data source.</param>
        public void UpdateProduct(AuditableProduct user) { }

        /// <summary>
        /// Gets all of the AuditableProduct objects in the application in an AuditableProducts collection.
        /// </summary>
        /// <returns>An AuditableProducts collection that contains all of the AuditableProduct objects in the application.</returns>
        public AuditableProducts GetProducts()
        {
            return new AuditableProducts();
        }
    }
}