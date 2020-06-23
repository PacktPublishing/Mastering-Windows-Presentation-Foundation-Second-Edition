using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides data type objects with validation support, so that they can report when they have validation errors.
    /// </summary>
    public abstract class BaseValidationModelExtended : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// A read only collection of validation messages if there are any validation errors.
        /// </summary>
        protected ObservableCollection<string> errors = new ObservableCollection<string>();
        /// <summary>
        /// A collection of validation messages containing any external validation errors from View Models.
        /// </summary>
        protected ObservableCollection<string> externalErrors = new ObservableCollection<string>();

        /// <summary>
        /// Initializes a new empty BaseValidationModelExtended object.
        /// </summary>
        protected BaseValidationModelExtended()
        {
            ExternalErrors.CollectionChanged += ExternalErrors_CollectionChanged;
        }

        /// <summary>
        /// Gets a collection containing validation messages for this data model object if there are any validation errors.
        /// </summary>
        public virtual ObservableCollection<string> Errors => errors;

        /// <summary>
        /// Gets a collection of validation messages containing any external validation errors from View Models.
        /// </summary>
        public ObservableCollection<string> ExternalErrors => externalErrors;

        /// <summary>
        /// Returns true if this object has any validation errors, or false otherwise.
        /// </summary>
        public virtual bool HasError => errors != null && Errors.Any();

        #region IDataErrorInfo Members

        /// <summary>
        /// Gets the validation message(s) if there are any validation errors.
        /// </summary>
        public string Error
        {
            get
            {
                if (!HasError) return string.Empty;
                StringBuilder errors = new StringBuilder();
                Errors.ForEach(e => errors.AppendUniqueOnNewLineIfNotEmpty(e));
                return errors.ToString();
            }
        }

        /// <summary>
        /// Gets the validation message relating to the propertyName input parameter, if there are any validation errors.
        /// </summary>
        public virtual string this[string propertyName] => string.Empty; 

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The event that is raised when a property that calls the NotifyPropertyChanged method is changed.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

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
                    if (propertyName != nameof(HasError)) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(HasError)));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyName">The optional name of the property to update in the View. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                if (propertyName != nameof(HasError)) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(HasError)));
            }
        }

        #endregion

        private void ExternalErrors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => NotifyPropertyChanged(nameof(Errors));
    }
}