using System;
using CompanyName.ApplicationName.DataModels.Interfaces;

namespace CompanyName.ApplicationName.DataModels.Business
{
    /// <summary>
    /// Represents a synchronisable and auditable user in the application.
    /// </summary>
    public class User : BaseSynchronizableDataModel<User>, ISynchronizableDataModel<User>, IAuditable
    {
        private Auditable auditable;

        /// <summary>
        /// Initializes a new User object with the values from the input parameters.
        /// </summary>
        /// <param name="id">The unique identification number of the User object.</param>
        /// <param name="name">The name of the User object.</param>
        /// <param name="age">The age of the User object.</param>
        public User(Guid id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        /// <summary>
        /// Initializes a new empty User object with default values.
        /// </summary>
        public User() { }

        /// <summary>
        /// Gets or sets the unique identification number of the User object.
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the name of the User object.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the age of the User object.
        /// </summary>
        public int Age { get; set; } = 0;

        /// <summary>
        /// Gets or sets the Auditable object of the User object that represents its auditable attributes.
        /// </summary>
        public Auditable Auditable
        {
            get { return auditable; }
            set { auditable = value; }
        }

        /// <summary>
        /// Copies all of the values from the input parameter to this object.
        /// </summary>
        /// <param name="user">The User object to copy the values from.</param>
        public override void CopyValuesFrom(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Age = user.Age;
        }

        /// <summary>
        /// Specifies whether the property values of the User object are equal to the property values of the object specified by the otherUser input parameter, or not.
        /// </summary>
        /// <param name="otherUser">The object to compare with the current object.</param>
        /// <returns>True if the property values of the specified object are equal to the property values of the object specified by the otherUser input parameter, otherwise false.</returns>
        public override bool PropertiesEqual(User otherUser)
        {
            if (otherUser == null) return false;
            return Id == otherUser.Id && Name == otherUser.Name && Age == otherUser.Age;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}