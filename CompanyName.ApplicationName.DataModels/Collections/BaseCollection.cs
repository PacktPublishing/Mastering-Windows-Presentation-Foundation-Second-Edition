using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels.Collections
{
    /// <summary>
    /// Represents a data collection that maintains a current item and provides a number of helpful utility methods.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public class BaseCollection<T> : ObservableCollection<T>, INotifyPropertyChanged where T : class, INotifyPropertyChanged, new()
    {
        /// <summary>
        /// Represents the currently selected item from the collection.
        /// </summary>
        protected T currentItem;

        /// <summary>
        /// Initializes a new BaseCollection object and adds the items from the collection specified by the input parameter.
        /// </summary>
        public BaseCollection(IEnumerable<T> collection) : this()
        {
            foreach (T item in collection) Add(item);
        }

        /// <summary>
        /// Initializes a new BaseCollection object and adds the items from the collection specified by the input parameter.
        /// </summary>
        public BaseCollection(params T[] collection) : this(collection as IEnumerable<T>) { }

        /// <summary>
        /// Initializes a new empty BaseCollection object.
        /// </summary>
        public BaseCollection() : base()
        {
            currentItem = new T();
        }

        /// <summary>
        /// Gets or sets the currently selected item from the collection. This property should be used to bind to the SelectedItem property of collection controls.
        /// </summary>
        public virtual T CurrentItem
        {
            get { return currentItem; }
            set
            {
                T oldCurrentItem = currentItem;
                currentItem = value;
                CurrentItemChanged?.Invoke(oldCurrentItem, currentItem);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value that specifies whether the collection object is empty or not.
        /// </summary>
        public bool IsEmpty
        {
            get { return !this.Any(); }
        }

        /// <summary>
        /// The delegate that is called when a property of the CurrentItem object is changed.
        /// </summary>
        public delegate void ItemPropertyChanged(T item, string propertyName);

        /// <summary>
        /// Gets or sets the delegate that is called when a property of the CurrentItem object is changed.
        /// </summary>
        public virtual ItemPropertyChanged CurrentItemPropertyChanged { get; set; }

        /// <summary>
        /// The delegate that is called when the CurrentItem object is changing or has changed.
        /// </summary>
        public delegate void CurrentItemChange(T oldItem, T newItem);

        /// <summary>
        /// Gets or sets the delegate that is called when the CurrentItem object is changed.
        /// </summary>
        public virtual CurrentItemChange CurrentItemChanged { get; set; }

        /// <summary>
        /// Returns a new empty object that is not currently in the collection.
        /// </summary>
        public T GetNewItem()
        {
            return new T();
        }

        /// <summary>
        /// Adds a new empty object to the end of the collection.
        /// </summary>
        public virtual void AddEmptyItem()
        {
            Add(new T());
        }

        /// <summary>
        /// Adds the collection of objects specified by the collection input parameter to the end of this collection.
        /// </summary>
        /// <param name="collection">The collection to be added to the end of this collection. The value can be null.</param>
        public virtual void Add(IEnumerable<T> collection)
        {
            collection.ForEach(i => base.Add(i));
        }

        /// <summary>
        /// Adds the collection of objects specified by the items input parameter to the end of this collection.
        /// </summary>
        /// <param name="items">The collection of objects to be added to the end of this collection. The value can be null.</param>
        public virtual void Add(params T[] items)
        {
            if (items.Length == 1) base.Add(items[0]);
            else Add(items as IEnumerable<T>);
        }

        /// <summary>
        /// Inserts an element into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        protected override void InsertItem(int index, T item)
        {
            if (item != null)
            {
                item.PropertyChanged += Item_PropertyChanged;
                base.InsertItem(index, item);
                if (Count == 1) CurrentItem = item;
            }
        }
        
        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        protected override void SetItem(int index, T item)
        {
            if (item != null)
            {
                item.PropertyChanged += Item_PropertyChanged;
                base.SetItem(index, item);
                if (Count == 1) CurrentItem = item;
            }
        }

        /// <summary>
        /// Removes all elements from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (T item in this) item.PropertyChanged -= Item_PropertyChanged;
            base.ClearItems();
        }

        /// <summary>
        /// Removes the element at the specified index of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">index is less than zero, or index is equal to or greater than the Count value of the collection.</exception>
        protected override void RemoveItem(int index)
        {
            T item = this[index];
            if (item != null) item.PropertyChanged -= Item_PropertyChanged;
            base.RemoveItem(index);
        }

        /// <summary>
        /// Sets the index of the CurrentItem to be the first item in the collection.
        /// </summary>
        public void ResetCurrentItemPosition()
        {
            if (this.Any()) CurrentItem = this.First();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((sender as T) == CurrentItem) CurrentItemPropertyChanged?.Invoke(currentItem, e.PropertyName);
            NotifyPropertyChanged(e.PropertyName);
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The event that is raised when a property that calls the NotifyPropertyChanged method is changed.
        /// </summary>
        protected override event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyNames">The names of the properties to update in the View.</param>
        protected void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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