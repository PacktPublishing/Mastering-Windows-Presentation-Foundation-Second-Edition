using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using CompanyName.ApplicationName.Managers.Interfaces;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Responsible for managing all user feedback in the application.
    /// </summary>
    public class FeedbackManager : INotifyPropertyChanged
    {
        private IUiThreadManager uiThreadManager = null;
        private static FeedbackCollection feedback = new FeedbackCollection();
        private static FeedbackManager instance = null;

        private FeedbackManager() { }

        /// <summary>
        /// Gets the single available instance of the application FeedbackManager object.
        /// </summary>
        public static FeedbackManager Instance => instance ?? (instance = new FeedbackManager());
        
        /// <summary>
        /// Gets or sets the IUiThreadManager object that ensures that the Feedback items are added to the FeedbackControl on the UI thread.
        /// </summary>
        public IUiThreadManager UiThreadManager
        {
            get { return uiThreadManager; }
            set { uiThreadManager = value; }
        }

        /// <summary>
        /// Gets or sets a collection of feedback content that will be displayed to the user in a FeedbackControl in MainWindow.xaml.
        /// </summary>
        public FeedbackCollection Feedback
        {
            get { return feedback; }
            set { feedback = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Adds the Feedback object specified by the feedback input parameter to the end of the FeedbackCollection if it is not already in the collection.
        /// </summary>
        /// <param name="feedback">The Feedback object to display.</param>
        public void Add(Feedback feedback)
        {
            UiThreadManager.RunOnUiThread((Action)delegate
            {
                Feedback.Add(feedback);
            });
        }

        /// <summary>
        /// Creates a new Feedback object and adds it to the end of the FeedbackCollection if it is not already in the collection.
        /// </summary>
        /// <param name="message">The feedback message to display.</param>
        /// <param name="isSuccess">The value that specifies whether the relating Feedback object has a FeedbackType of FeedbackType.Success or FeedbackType.Error.</param>
        public void Add(string message, bool isSuccess)
        {
            Add(new Feedback(message, isSuccess));
        }

        /// <summary>
        /// Creates a new Feedback object and adds it to the end of the FeedbackCollection if it is not already in the collection.
        /// </summary>
        /// <param name="result">The DataOperationResult object that contains the feedback to be added to the end of the collection. The value can be null.</param>
        /// <param name="isPermanent">The value that specifies whether the relating Feedback object should be removed automatically after a time period from a FeedbackControl or not.</param>
        public void Add<T>(DataOperationResult<T> result, bool isPermanent)
        {
            Add(new Feedback(result.Description, result.IsSuccess, isPermanent));
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