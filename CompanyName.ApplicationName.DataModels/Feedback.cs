using System;
using System.ComponentModel;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.DataModels.Interfaces;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents a container to provide feedback content to the user from data operations.
    /// </summary>
    public class Feedback : IAnimatable, INotifyPropertyChanged
    {
        private string message = string.Empty;
        private FeedbackType type = FeedbackType.None;
        private TimeSpan duration = new TimeSpan(0, 0, 4);
        private bool isPermanent = false;
        private Animatable animatable;

        /// <summary>
        /// Initializes a new Feedback object with the values provided by the input parameters.
        /// </summary>
        /// <param name="message">The message of the Feedback object.</param>
        /// <param name="type">The type of the Feedback object.</param>
        /// <param name="duration">The length of time that the Feedback object should be displayed for.</param>
        public Feedback(string message, FeedbackType type, TimeSpan duration)
        {
            Message = message;
            Type = type;
            Duration = duration == TimeSpan.Zero ? this.duration : duration;
            IsPermanent = false;
            Animatable = new Animatable(this);
        }

        /// <summary>
        /// Initializes a new Feedback object with the values provided by the input parameters.
        /// </summary>
        /// <param name="message">The message of the Feedback object.</param>
        /// <param name="isSuccess">The value that specifies whether the Feedback object has a FeedbackType of FeedbackType.Success or FeedbackType.Error.</param>
        /// <param name="isPermanent">The value that specifies whether the Feedback object should be removed automatically after a time period from a FeedbackControl or not.</param>
        public Feedback(string message, bool isSuccess, bool isPermanent) : this(message, isSuccess ? FeedbackType.Success : FeedbackType.Error, TimeSpan.Zero)
        {
            IsPermanent = isPermanent;
        }

        /// <summary>
        /// Initializes a new Feedback object with the values provided by the input parameters.
        /// </summary>
        /// <param name="message">The message of the Feedback object.</param>
        /// <param name="type">The type of the Feedback object.</param>
        public Feedback(string message, FeedbackType type) : this(message, type, TimeSpan.Zero) { }

        /// <summary>
        /// Initializes a new Feedback object with the values provided by the input parameters. Note that this constructor only enables FeedbackType.Success and FeedbackType.Error types. 
        /// </summary>
        /// <param name="message">The message of the Feedback object.</param>
        /// <param name="isSuccess">The value that specifies whether the Feedback object has a FeedbackType of FeedbackType.Success or FeedbackType.Error.</param>
        public Feedback(string message, bool isSuccess) : this(message, isSuccess ? FeedbackType.Success : FeedbackType.Error, TimeSpan.Zero) { }

        /// <summary>
        /// Initializes a new empty Feedback object.
        /// </summary>
        public Feedback() : this(string.Empty, FeedbackType.None) { }

        /// <summary>
        /// Gets or sets the message of the Feedback object.
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the length of time that the Feedback object should be displayed for.
        /// </summary>
        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the type of the Feedback object.
        /// </summary>
        public FeedbackType Type
        {
            get { return type; }
            set { type = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the value that specifies whether the Feedback object should be removed automatically after a time period from a FeedbackControl or not.
        /// </summary>
        public bool IsPermanent
        {
            get { return isPermanent; }
            set { isPermanent = value; NotifyPropertyChanged(); }
        }

        #region IAnimatable Members

        /// <summary>
        /// Gets or sets a value that describes the current status of the object's animation.
        /// </summary>
        public Animatable Animatable
        {
            get { return animatable; }
            set { animatable = value; }
        }

        #endregion

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
                propertyNames.ForEach(p => PropertyChanged(this, new PropertyChangedEventArgs(p)));
                PropertyChanged(this, new PropertyChangedEventArgs("HasError"));
            }
        }

        #endregion
    }
}