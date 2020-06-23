using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides data model objects with validation support from the INotifyDataErrorInfo interface, so that they can report when they have validation errors.
    /// </summary>
    public abstract class BaseNotifyValidationModelExtended : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private ValidationLevel validationLevel = ValidationLevel.Full;

        /// <summary>
        /// Initializes a new empty BaseNotifyValidationModel object.
        /// </summary>
        protected BaseNotifyValidationModelExtended()
        {
            ExternalErrors.CollectionChanged += ExternalErrors_CollectionChanged;
        }

        /// <summary>
        /// Gets a collection containing validation error messages for this data model object, if there are any validation errors.
        /// </summary>
        protected Dictionary<string, List<string>> AllPropertyErrors { get; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Gets or sets the ValidationLevel member of the BaseNotifyValidationModelExtended object, that specifies the level of validation required on it.
        /// </summary>
        public ValidationLevel ValidationLevel
        {
            get { return validationLevel; }
            set { if (validationLevel != value) { validationLevel = value; } }
        }

        /// <summary>
        /// Gets a collection containing validation error messages for this data model object, if there are any validation errors.
        /// </summary>
        public virtual ObservableCollection<string> Errors
        {
            get
            {
                ObservableCollection<string> errors = new ObservableCollection<string>(AllPropertyErrors.Values.SelectMany(e => e).Distinct());
                ExternalErrors.Where(e => !errors.Contains(e)).ForEach(e => errors.Add(e));
                return errors;
            }
        }

        /// <summary>
        /// Gets a collection of validation error messages containing any external validation errors from View Models.
        /// </summary>
        public ObservableCollection<string> ExternalErrors { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets the validation error message relating to the propertyName input parameter, if there are any validation errors.
        /// </summary>
        public abstract IEnumerable<string> this[string propertyName] { get; }

        /// <summary>
        /// Validates all of the validatable properties of the data model object.
        /// </summary>
        public abstract void ValidateAllProperties();

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI and then validates each of the object properties specified by the propertyNames input parameter.
        /// </summary>
        /// <param name="propertyNames">The names of the properties to validate and update in the View.</param>
        public void NotifyPropertyChangedAndValidate(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames) NotifyPropertyChangedAndValidate(propertyName);
        }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI and then validates each of the object properties specified by the propertyNames input parameter.
        /// </summary>
        /// <param name="propertyName">The optional name of the property to validate and update in the View. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        public void NotifyPropertyChangedAndValidate([CallerMemberName]string propertyName = "")
        {
            NotifyPropertyChanged(propertyName);
            Validate(propertyName);
        }

        /// <summary>
        /// Validates the value associated with each property name in the collection specified by the propertyNames input parameter.
        /// </summary>
        /// <param name="propertyNames"></param>
        public void Validate(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames) Validate(propertyName);
        }

        /// <summary>
        /// Updates the errors in the AllPropertyErrors property collection and raises the ErrorsChanged event, dependent upon the errors returned from the this indexer property for the property specified by the propertyName input parameter.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        public void Validate(string propertyName)
        {
            if (ValidationLevel == ValidationLevel.None) return;
            ValidationContext validationContext = new ValidationContext(this);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            IEnumerable<string> allErrors = validationResults.Where(v => v.MemberNames.Contains(propertyName)).Select(v => v.ErrorMessage).Concat(this[propertyName]);
            UpdateErrors(propertyName, allErrors);
        }

        private void UpdateErrors(string propertyName, IEnumerable<string> errors)
        {
            if (errors.Any())
            {
                if (AllPropertyErrors.ContainsKey(propertyName)) AllPropertyErrors[propertyName].Clear();
                else AllPropertyErrors.Add(propertyName, new List<string>());
                AllPropertyErrors[propertyName].AddRange(errors);
                OnErrorsChanged(propertyName);
            }
            else
            {
                if (AllPropertyErrors.ContainsKey(propertyName)) AllPropertyErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void ExternalErrors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Errors), nameof(HasErrors));
        }

        #region INotifyDataErrorInfo Members

        /// <summary>
        /// The event that is raised when the validation errors have changed for a property or for the entire entity. 
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Raises the ErrorsChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that the error relates to.</param>
        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            NotifyPropertyChanged(nameof(Errors), nameof(HasErrors));
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation error messages for; or null or Empty, to retrieve entity-level errors.</param>
        /// <returns>The validation error messages for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> propertyErrors = new List<string>();
            if (string.IsNullOrEmpty(propertyName)) return propertyErrors;
            AllPropertyErrors.TryGetValue(propertyName, out propertyErrors);
            return propertyErrors;
        }

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors. 
        /// </summary>
        public bool HasErrors => ExternalErrors.Any() || AllPropertyErrors.Any(p => p.Value != null && p.Value.Any());

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
            if (PropertyChanged != null) propertyNames.ForEach(p => PropertyChanged(this, new PropertyChangedEventArgs(p)));
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