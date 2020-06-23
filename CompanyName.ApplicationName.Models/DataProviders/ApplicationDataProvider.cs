using System;
using System.Data.Linq;
using System.Linq;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using CompanyName.ApplicationName.Models.Interfaces;

namespace CompanyName.ApplicationName.Models.DataProviders
{
    /// <summary>
    /// Provides access to the application data source.
    /// </summary>
    public class ApplicationDataProvider : IDataProvider
    {
        /// <summary>
        /// Gets the ApplicationDataContext object that provides access to the actual application data source.
        /// </summary>
        public ApplicationDataContext DataContext
        {
            get { return new ApplicationDataContext(""); }
        }

        /// <summary>
        /// Adds the User object specified by the user input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="user">The User object to add to the data source.</param>
        /// <returns>True if the data operation was successful, or false otherwise.</returns>
        public bool AddUser(User user)
        {
            using (ApplicationDataContext dataContext = DataContext)
            {
                DbUser dbUser = new DbUser();
                dbUser.Name = user.Name;
                dbUser.Age = user.Age;
                dataContext.DbUsers.InsertOnSubmit(dbUser);
                dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                user.Id = dbUser.Id;
                return true;
            }
        }

        /// <summary>
        /// Gets the User object that relates to the specified id input parameter from the data source.
        /// </summary>
        /// <param name="id">The unique identification number of the User object to retrieve from the data source.</param>
        /// <returns>A User object that relates to the specified id input parameter, or null otherwise.</returns>
        public User GetUser(Guid id)
        {
            DbUser dbUser = DataContext.DbUsers.SingleOrDefault(u => u.Id == id);
            if (dbUser == null) return null;
            return new User(dbUser.Id, dbUser.Name, dbUser.Age);
        }

        /// <summary>
        /// Saves the User object specified by the user input parameter in the data source.
        /// </summary>
        /// <param name="user">The User object to update in the data source.</param>
        /// <returns>True if the data operation was successful, or false otherwise.</returns>
        public bool SaveUser(User user)
        {
            using (ApplicationDataContext dataContext = DataContext)
            {
                DbUser dbUser = dataContext.DbUsers.SingleOrDefault(u => u.Id == user.Id);
                if (dbUser == null) return false;
                dbUser.Name = user.Name;
                dbUser.Age = user.Age;
                dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                return true;
            }
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