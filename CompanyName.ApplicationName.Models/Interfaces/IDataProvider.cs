using System;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.Models.Interfaces
{
    /// <summary>
    /// Provides access to the application data source.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Adds the User object specified by the user input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="user">The User object to add to the data source.</param>
        /// <returns>True if the data operation was successful or false otherwise.</returns>
        bool AddUser(User user);

        /// <summary>
        /// Gets the User object that relates to the specified id input parameter from the data source.
        /// </summary>
        /// <param name="id">The unique identification number of the User object to retrieve from the data source.</param>
        /// <returns>A User object that relates to the specified id input parameter, or null otherwise.</returns>
        User GetUser(Guid id);

        /// <summary>
        /// Saves the User object specified by the user input parameter in the data source.
        /// </summary>
        /// <param name="user">The User object to update in the data source.</param>
        /// <returns>A SetDataOperationResult object containing details relating to whether the operation was successful or not.</returns>
        bool SaveUser(User user);

        /// <summary>
        /// Adds the AuditableProduct object specified by the product input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="product">The AuditableProduct object to add to the data source.</param>
        void AddProduct(AuditableProduct user);

        /// <summary>
        /// Deletes the AuditableProduct object specified by the product input parameter from the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to remove from the data source.</param>
        /// <returns>A SetDataOperationResult object containing details relating to whether the operation was successful or not.</returns>
        void DeleteProduct(AuditableProduct user);

        /// <summary>
        /// Updates the AuditableProduct object specified by the product input parameter in the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to update in the data source.</param>
        /// <returns>A SetDataOperationResult object containing details relating to whether the operation was successful or not.</returns>
        void UpdateProduct(AuditableProduct user);

        /// <summary>
        /// Gets all of the AuditableProduct objects in the application in an AuditableProducts collection.
        /// </summary>
        /// <returns>An AuditableProducts collection that contains all of the AuditableProduct objects in the application.</returns>
        AuditableProducts GetProducts();
    }
}