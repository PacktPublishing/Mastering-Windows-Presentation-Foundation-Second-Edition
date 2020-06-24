using CompanyName.ApplicationName.DataModels.Enums;

namespace CompanyName.ApplicationName.DataModels.Interfaces
{
    /// <summary>
    /// Provides data type objects with synchronization support, so that they can be aware when they have changes and have their original values restored if required.
    /// </summary>
    /// <typeparam name="T">The type of the data model object requiring synchronization support.</typeparam>
    public interface ISynchronizableDataModel<T> where T : class, new()
    {
        /// <summary>
        /// Gets or sets a value that specifies the current state of the object.
        /// </summary>
        ObjectState ObjectState { get; set; }

        /// <summary>
        /// Gets a value that specifies whether any properties in any object in the collection have changed since they were last synchronized or not.
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value that specifies whether the object has been synchronized or not.
        /// </summary>
        bool IsSynchronized { get; }

        /// <summary>
        /// Reverts the values of all of the properties in this object to the state that they were in when they were last synchronized.
        /// </summary>
        void RevertState();

        /// <summary>
        /// Synchronizes the value of the internal synchronization member with the current property values of this object.
        /// </summary>
        void Synchronize();

        /// <summary>
        /// Specifies whether the property values of the object are equal to the property values of the object specified by the dataModel input parameter, or not.
        /// </summary>
        /// <param name="dataModel">The object to compare with the current object.</param>
        /// <returns>True if the property values of the specified object are equal to the property values of the object specified by the dataModel input parameter, otherwise false.</returns>
        bool PropertiesEqual(T dataModel);

        /// <summary>
        /// Copies all of the values from the dataModel input parameter to this object.
        /// </summary>
        /// <param name="dataModel">The object to copy the values from.</param>
        void CopyValuesFrom(T dataModel);

        /// <summary>
        /// Returns a deep copy of the object being cloned.
        /// </summary>
        /// <returns>A deep copy of the object being cloned.</returns>
        T Clone();
    }
}