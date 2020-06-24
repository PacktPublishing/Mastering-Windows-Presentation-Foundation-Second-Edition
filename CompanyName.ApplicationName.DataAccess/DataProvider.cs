using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.DataAccess
{
    /// <summary>
    /// Provides access to the application data source.
    /// </summary>
    public class DataProvider
    {
        /// <summary>
        /// Adds the AuditableProduct object specified by the product input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="product">The AuditableProduct object to add to the data source.</param>
        public void AddProduct(AuditableProduct product)
        {            
        }

        /// <summary>
        /// Deletes the AuditableProduct object specified by the product input parameter from the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to remove from the data source.</param>
        public void DeleteProduct(AuditableProduct product)
        {            
        }

        /// <summary>
        /// Updates the AuditableProduct object specified by the product input parameter in the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to update in the data source.</param>
        public void UpdateProduct(AuditableProduct product)
        {
        }

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