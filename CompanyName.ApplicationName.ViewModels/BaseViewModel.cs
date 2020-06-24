using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using CompanyName.ApplicationName.DataModels.Interfaces;
using CompanyName.ApplicationName.Managers;
using CompanyName.ApplicationName.Managers.Interfaces;
using CompanyName.ApplicationName.Models.DataControllers;
using CompanyName.ApplicationName.Models.Interfaces;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides access to the INotifyPropertyChanged interface, application data provider and global ViewModel properties.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool isFocused = false;

        /// <summary>
        /// Initializes a new empty BaseViewModel object.
        /// </summary>
        public BaseViewModel()
        {
            if (FeedbackManager.UiThreadManager == null) FeedbackManager.UiThreadManager = UiThreadManager;
        }

        /// <summary>
        /// The delegate that is called to signal that something has occurred.
        /// </summary>
        public delegate void Signal();

        /// <summary>
        /// Gets or sets a handler for the delegate that is called to signal that something has occurred.
        /// </summary>
        public static Signal OnSecurityPermissionChanged { get; set; }

        /// <summary>
        /// Provides access to the application data source.
        /// </summary>
        protected DataController Model
        {
            get { return new DataController(DependencyManager.Instance.Resolve<IDataProvider>(), new DataOperationManager(UiThreadManager), StateManager.CurrentUser); }
        }

        /// <summary>
        /// Gets the IUiThreadManager object that is responsible for managing UI access to asynchronous data in the application.
        /// </summary>
        public IUiThreadManager UiThreadManager
        {
            get { return DependencyManager.Instance.Resolve<IUiThreadManager>(); }
        }

        /// <summary>
        /// Gets the IHardDriveManager object that provides access to store, retrieve and delete files from isolated storage stores on the users' computers.
        /// </summary>
        public IHardDriveManager HardDriveManager
        {
            get { return DependencyManager.Instance.Resolve<IHardDriveManager>(); }
        }

        /// <summary>
        /// Gets the StateManager object that is responsible for maintaining global state in the application.
        /// </summary>
        public StateManager StateManager
        {
            get { return StateManager.Instance; }
        }

        /// <summary>
        /// Gets the FeedbackManager object that is responsible for managing all user feedback in the application.
        /// </summary>
        public FeedbackManager FeedbackManager
        {
            get { return FeedbackManager.Instance; }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether a particular control in the View that has the FocusableTextBoxStyle applied to it is focused or not.
        /// </summary>
        public bool IsFocused
        {
            get { return isFocused; }
            set { if (isFocused != value) { isFocused = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Adds a new item to the collection specified by the collection input parameter and sets the BaseCollection.CurrentItem property to this item.
        /// </summary>
        /// <typeparam name="S">The type of the collection specified by the collection input parameter. This must either be or extend a BaseSynchronizableCollection of type T.</typeparam>
        /// <typeparam name="T">The type of the item specified by the item input parameter and the items inside the collection specified by the collection input parameter. This must either be or extend a BaseSynchronizableDataModel of type T.</typeparam>
        /// <param name="collection">The collection to add the new item to.</param>
        /// <returns>The new item that was added to the collection specified by the collection input parameter.</returns>
        public T AddNewDataTypeToCollection<S, T>(S collection) where S : BaseSynchronizableCollection<T> where T : BaseSynchronizableDataModel<T>, new()
        {
            T item = collection.GetNewItem();
            if (item is IAuditable) ((IAuditable)item).Auditable.CreatedOn = DateTime.Now;
            item.Synchronize();
            collection.Add(item);
            collection.CurrentItem = item;
            return item;
        }

        /// <summary>
        /// Inserts a new item to the collection specified by the collection input parameter at the index specified by the index input parameter and sets the BaseCollection.CurrentItem property to this item.
        /// </summary>
        /// <typeparam name="S">The type of the collection specified by the collection input parameter. This must either be or extend a BaseSynchronizableCollection of type T.</typeparam>
        /// <typeparam name="T">The type of the item specified by the item input parameter and the items inside the collection specified by the collection input parameter. This must either be or extend a BaseSynchronizableDataModel of type T.</typeparam>
        /// <param name="collection">The collection to add the new item to.</param>
        /// <param name="index">The index in the collection in which to insert the new item.</param>
        /// <returns>The new item that was added to the collection specified by the collection input parameter.</returns>
        public T InsertNewDataTypeToCollection<S, T>(S collection, int index) where S : BaseSynchronizableCollection<T> where T : BaseSynchronizableDataModel<T>, new()
        {
            T item = collection.GetNewItem();
            if (item is IAuditable) ((IAuditable)item).Auditable.CreatedOn = DateTime.Now;
            item.Synchronize();
            collection.Insert(index, item);
            collection.CurrentItem = item;
            return item;
        }

        /// <summary>
        /// Removes the item specified by the item input parameter from the collection specified by the collection input parameter and sets the BaseCollection.CurrentItem to the previous or next item in the collection if possible.
        /// </summary>
        /// <typeparam name="S">The type of the collection specified by the collection input parameter. This must either be or extend a BaseSynchronizableCollection of type T.</typeparam>
        /// <typeparam name="T">The type of the item specified by the item input parameter and the items inside the collection specified by the collection input parameter. This must either be or extend a BaseSynchronizableDataModel of type T.</typeparam>
        /// <param name="collection">The collection to remove the item specified by the item input parameter from.</param>
        /// <param name="item">The item to remove from the collection specified by the collection input parameter.</param>
        public void RemoveDataTypeFromCollection<S, T>(S collection, T item) where S : BaseSynchronizableCollection<T> where T : BaseSynchronizableDataModel<T>, new()
        {
            int index = collection.IndexOf(item);
            collection.RemoveAt(index);
            if (index > collection.Count) index = collection.Count;
            else if (index < 0) index++;
            if (index > 0 && index < collection.Count && collection.CurrentItem != collection[index]) collection.CurrentItem = collection[index];
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The event that is raised when a property that calls the NotifyPropertyChanged method is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyNames">The names of the properties to update in the View.</param>
        protected virtual void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyName">The optional name of the property to update in the View. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}