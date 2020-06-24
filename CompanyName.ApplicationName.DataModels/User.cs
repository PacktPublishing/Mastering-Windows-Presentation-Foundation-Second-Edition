using System;
using CompanyName.ApplicationName.DataModels.Interfaces;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : BaseSynchronizableDataModel<User>, ISynchronizableDataModel<User>, IAuditable, IAnimatable
    {
        private Address address = new Address();
        private Animatable animatable;
        private Auditable auditable = new Auditable();
        private Guid id = Guid.Empty;
        private string name = string.Empty;
        private int age = 0;

        /// <summary>
        /// Initializes a new User object with the values provided by the input parameters.
        /// </summary>
        public User(Guid id, string name, int age)
        {
            Animatable = new Animatable(this);
            Id = id;
            Name = name;
            Age = age;
        }

        /// <summary>
        /// Initializes a new empty User object.
        /// </summary>
        public User() { }

        /// <summary>
        /// Gets or sets the unique identification number of the User object.
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { if (id != value) { id = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the name of the User object.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the age of the User object.
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the address of the User object encapsulated in an Address object.
        /// </summary>
        public Address Address
        {
            get { return address; }
            set { address = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the Auditable object of the User object that represents auditable attributes for an object in the application.
        /// </summary>
        public Auditable Auditable
        {
            get { return auditable; }
            set { auditable = value; }
        }

        /// <summary>
        /// Gets or sets the Animatable object of the User object that enables implementing classes to use the animated features of animatable user controls.
        /// </summary>
        public Animatable Animatable
        {
            get { return animatable; }
            set { animatable = value; }
        }

        /// <summary>
        /// Copies all of the values from the user input parameter to this object.
        /// </summary>
        /// <param name="user">The object to copy the values from.</param>
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