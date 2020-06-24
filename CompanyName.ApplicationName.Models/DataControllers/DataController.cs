using System;
using System.Threading.Tasks;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.DataModels.Interfaces;
using CompanyName.ApplicationName.Managers;
using CompanyName.ApplicationName.Models.Interfaces;

namespace CompanyName.ApplicationName.Models.DataControllers
{
    /// <summary>
    /// Provides the application with the opportunity to make data source requests re-tryable and to wrap the results in DataOperationResult objects.
    /// </summary>
    public class DataController
    {
        private IDataProvider dataProvider;
        private DataOperationManager dataOperationManager;

        /// <summary>
        /// Initializes a new DataController with the values from the input parameters.
        /// </summary>
        /// <param name="dataProvider">The IDataProvider object that provides access to the application data source.</param>
        /// <param name="dataOperationManager">The DataOperationManager object used to perform asynchronous and retryable data operations with.</param>
        /// <param name="currentUser">The User that is currently logged in to the application.</param>
        public DataController(IDataProvider dataProvider, DataOperationManager dataOperationManager, User currentUser)
        {
            DataProvider = dataProvider;
            DataOperationManager = dataOperationManager;
            CurrentUser = currentUser?.Clone();
        }

        /// <summary>
        /// Gets the IDataProvider object that provides access to the application data source.
        /// </summary>
        protected IDataProvider DataProvider
        {
            get { return dataProvider; }
            private set { dataProvider = value; }
        }

        /// <summary>
        /// The DataOperationManager object used to perform asynchronous and retryable data operations with.
        /// </summary>
        protected DataOperationManager DataOperationManager
        {
            get { return dataOperationManager; }
            private set { dataOperationManager = value; }
        }

        /// <summary>
        /// Gets or sets the User that is currently logged in to the application.
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Adds the User object specified by the user input parameter to the data source and updates its Id property.
        /// </summary>
        /// <param name="user">The User object to add to the data source.</param>
        /// <returns>True if the data operation was successful or false otherwise.</returns>
        public bool AddUser(User user)
        {
            return DataProvider.AddUser(SetAuditCreateFields(user));
        }

        /// <summary>
        /// Gets the User object that relates to the specified id input parameter from the data source.
        /// </summary>
        /// <param name="id">The unique identification number of the User object to retrieve from the data source.</param>
        /// <returns>A User object that relates to the specified id input parameter, or null otherwise.</returns>
        public User GetUser(Guid id)
        {
            return DataProvider.GetUser(id);
        }

        /// <summary>
        /// Saves the User object specified by the user input parameter in the data source.
        /// </summary>
        /// <param name="user">The User object to update in the data source.</param>
        /// <returns>A SetDataOperationResult object containing details relating to whether the operation was successful or not.</returns>
        public bool SaveUser(User user)
        {
            return DataProvider.SaveUser(SetAuditUpdateFields(user));
        }

        /// <summary>
        /// Adds the AuditableProduct object specified by the product input parameter to the data source and updates its Id property asynchronously.
        /// </summary>
        /// <param name="product">The AuditableProduct object to add to the data source.</param>
        public Task<SetDataOperationResult> AddProductAsync(AuditableProduct product)
        {
            return DataOperationManager.TrySetAsync(() => DataProvider.AddProduct(InitializeDataModel(product)), $"{product.Name} was added to the data source successfully", $"A problem occurred and {product.Name} was not added to the data source.");
        }

        /// <summary>
        /// Deletes the AuditableProduct object specified by the product input parameter from the data source asynchronously.
        /// </summary>
        /// <param name="product">The AuditableProduct object to remove from the data source.</param>
        /// <returns>A SetDataOperationResult object containing details relating to whether the operation was successful or not.</returns>
        public Task<SetDataOperationResult> DeleteProductAsync(AuditableProduct product)
        {
            return DataOperationManager.TrySetAsync(() => DataProvider.DeleteProduct(DeleteDataModel(product)), $"{product.Name} has been deleted from the data source successfully.", $"A problem occurred and {product.Name} was not deleted from the data source.", true, false);
        }

        /// <summary>
        /// Gets all of the AuditableProduct objects in the application in an AuditableProducts collection asynchronously.
        /// </summary>
        /// <returns>An AuditableProducts collection that contains all of the AuditableProduct objects in the application.</returns>
        public Task<GetDataOperationResult<AuditableProducts>> GetProductsAsync()
        {
            return DataOperationManager.TryGetAsync(() => DataProvider.GetProducts(), string.Empty, "A problem occurred when trying to retrieve the products.", true);
        }

        /// <summary>
        /// Updates the AuditableProduct object specified by the product input parameter in the data source.
        /// </summary>
        /// <param name="product">The AuditableProduct object to update in the data source.</param>
        /// <returns>A SetDataOperationResult object containing details relating to whether the operation was successful or not.</returns>
        public SetDataOperationResult UpdateProduct(AuditableProduct product)
        {
            return DataOperationManager.TrySet(() => DataProvider.UpdateProduct(UpdateDataModel(product)), $"{product.Name} was saved in the data source successfully.", $"A problem occurred and {product.Name} was not updated in the data source.", false, false);
        }

        private T InitializeDataModel<T>(T dataModel) where T : class, IAuditable, new()
        {
            dataModel.Auditable = new Auditable(CurrentUser);
            if (dataModel is ISynchronizableDataModel<T>)
            {
                ISynchronizableDataModel<T> synchronisableDataModel = (ISynchronizableDataModel<T>)dataModel;
                synchronisableDataModel.ObjectState = ObjectState.Active;
            }
            return dataModel;
        }

        private T DeleteDataModel<T>(T dataModel) where T : class, IAuditable, new()
        {
            dataModel.Auditable.UpdatedOn = DateTime.Now;
            dataModel.Auditable.UpdatedBy = CurrentUser;
            if (dataModel is ISynchronizableDataModel<T>)
            {
                ISynchronizableDataModel<T> synchronisableDataModel = (ISynchronizableDataModel<T>)dataModel;
                synchronisableDataModel.ObjectState = ObjectState.Deleted;
            }
            return dataModel;
        }

        private T UpdateDataModel<T>(T dataModel) where T : class, IAuditable, new()
        {
            dataModel.Auditable.UpdatedOn = DateTime.Now;
            dataModel.Auditable.UpdatedBy = CurrentUser;
            return dataModel;
        }

        private T SetAuditCreateFields<T>(T dataModel) where T : IAuditable
        {
            dataModel.Auditable.CreatedOn = DateTime.Now;
            dataModel.Auditable.CreatedBy = CurrentUser;
            return dataModel;
        }

        private T SetAuditUpdateFields<T>(T dataModel) where T : IAuditable
        {
            dataModel.Auditable.UpdatedOn = DateTime.Now;
            dataModel.Auditable.UpdatedBy = CurrentUser;
            return dataModel;
        }
    }
}