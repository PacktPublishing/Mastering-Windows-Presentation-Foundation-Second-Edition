using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents auditable attributes for an object in the application.
    /// </summary>
    public class Auditable : BaseDataModel
    {
        private DateTime createdOn;
        private DateTime? updatedOn;
        private User createdBy, updatedBy;

        /// <summary>
        /// Initializes a new Auditable object with the values from the input parameters.
        /// </summary>
        /// <param name="createdOn">The date and time that the Auditable object was created.</param>
        /// <param name="createdBy">The User object that created this Auditable object.</param>
        /// <param name="updatedOn">The nullable date and time that the Auditable object was last updated.</param>
        /// <param name="updatedBy">The User object that last updated the Auditable object</param>
        public Auditable(DateTime createdOn, User createdBy, DateTime? updatedOn, User updatedBy)
        {
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            UpdatedOn = updatedOn;
            UpdatedBy = updatedBy;
        }

        /// <summary>
        /// Initializes a new Auditable object with the value from the input parameter.
        /// </summary>
        /// <param name="createdBy">The User object that created this Auditable object.</param>
        public Auditable(User createdBy) : this(DateTime.Now, createdBy, null, new User()) { }

        /// <summary>
        /// Initializes a new empty Auditable object.
        /// </summary>
        /// <remarks>This constructor should not be used directly in code. It is only present for instantiation using reflection.</remarks>
        public Auditable() { }

        /// <summary>
        /// Gets or sets the date and time that this object was created.
        /// </summary>
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the User object that created this object.
        /// </summary>
        public User CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the nullable date and time that this object was last updated. The value can be null.
        /// </summary>
        public DateTime? UpdatedOn
        {
            get { return updatedOn; }
            set { updatedOn = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the User object that last updated this object.
        /// </summary>
        public User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return UpdatedOn.HasValue ? $"Created on {CreatedOn.ToLongDateString()} by {CreatedBy.Name}, last updated on {UpdatedOn.Value.ToLongDateString()} by {UpdatedBy.Name}" : $"Created on {CreatedOn.ToLongDateString()} by {CreatedBy.Name}";
        }
    }
}