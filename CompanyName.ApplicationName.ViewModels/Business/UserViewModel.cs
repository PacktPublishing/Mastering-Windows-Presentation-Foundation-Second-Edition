using System;
using CompanyName.ApplicationName.DataModels.Business;

namespace CompanyName.ApplicationName.ViewModels.Business
{
    /// <summary>
    /// Wraps a business User object and adds additional properties that are required to enable the editing of the internal User object in the View.
    /// </summary>
    public class UserViewModel : BaseBusinessViewModel
    {
        private User model;
        private bool isSelected = false;

        /// <summary>
        /// Initializes a new UsersViewModel object with the value specified by the input parameter.
        /// </summary>
        /// <param name="model">The business model that this View Model will use to access data.</param>
        public UserViewModel(User model)
        {
            Model = model;
        }

        /// <summary>
        /// Gets or sets the User object that is wrapped by this class.
        /// </summary>
        public User Model
        {
            get { return model; }
            set { model = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the unique identification number of the User object that is wrapped by this class.
        /// </summary>
        public Guid Id
        {
            get { return Model.Id; }
            set { Model.Id = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the name of the User object that is wrapped by this class.
        /// </summary>
        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the age of the User object that is wrapped by this class.
        /// </summary>
        public int Age
        {
            get { return Model.Age; }
            set { Model.Age = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the User object that is wrapped by this class is selected in the View or not.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; NotifyPropertyChanged(); }
        }
    }
}