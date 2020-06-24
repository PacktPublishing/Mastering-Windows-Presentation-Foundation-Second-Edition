using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.DataModels.Interfaces;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides data type objects with synchronisation support, so that they can be aware when they have changes and have their original values restored if required.
    /// </summary>
    /// <typeparam name="T">The type of the data model object requiring synchronisation support.</typeparam>
    public abstract class BaseSynchronizableDataModel<T> : BaseDataModel, ISynchronizableDataModel<T> where T : BaseDataModel, ISynchronizableDataModel<T>, new()
    {
        private T originalState;
        private ObjectState objectState = ObjectState.Active;

        /// <summary>
        /// Gets or sets a value that specifies the current state of the object.
        /// </summary>
        public ObjectState ObjectState
        {
            get { return objectState; }
            set { if (objectState != value) { objectState = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets a value that specifies whether any properties in this object have changed since they were last synchronised or not.
        /// </summary>
        public bool HasChanges
        {
            get { return originalState != null && !PropertiesEqual(originalState); }
        }

        /// <summary>
        /// Gets a value that specifies whether the object has been synchronised or not.
        /// </summary>
        public bool IsSynchronized
        {
            get { return originalState != null; }
        }

        /// <summary>
        /// Synchronises the value of the internal synchronisation member with the current property values of this object.
        /// </summary>
        public void Synchronize()
        {
            originalState = this.Clone();
            if (originalState == null) { }
            NotifyPropertyChanged(nameof(HasChanges));
        }

        /// <summary>
        /// Reverts the values of all of the properties in this object to the state that they were in when they were last synchronised.
        /// </summary>
        public void RevertState()
        {
            Debug.Assert(originalState != null, "Object not yet synchronized.");
            CopyValuesFrom(originalState);
            Synchronize();
            NotifyPropertyChanged(nameof(HasChanges));
        }

        /// <summary>
        /// Specifies whether the property values of the BaseSynchronisableDataType object are equal to the property values of the object specified by the dataModel input parameter, or not.
        /// </summary>
        /// <param name="dataModel">The object to compare with the current object.</param>
        /// <returns>True if the property values of the specified object are equal to the property values of the object specified by the dataModel input parameter, otherwise false.</returns>
        public abstract bool PropertiesEqual(T dataModel);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public abstract override string ToString();

        /// <summary>
        /// Copies all of the values from the dataModel input parameter to this object.
        /// </summary>
        /// <param name="dataModel">The object to copy the values from.</param>
        public abstract void CopyValuesFrom(T dataModel);

        /// <summary>
        /// Returns a deep copy of the object being cloned.
        /// </summary>
        /// <returns>A deep copy of the object being cloned.</returns>
        public virtual T Clone()
        {
            T clone = new T();
            clone.CopyValuesFrom(this as T);
            return clone;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The event that is raised when a property that calls the NotifyPropertyChanged method is changed.
        /// </summary>
        public override event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyNames">The names of the properties to update in the View.</param>
        protected override void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    if (propertyName != nameof(HasChanges)) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(HasChanges)));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyName">The optional name of the property to update in the View. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        protected override void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                if (propertyName != nameof(HasChanges)) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(HasChanges)));
            }
        }

        #endregion
    }
}