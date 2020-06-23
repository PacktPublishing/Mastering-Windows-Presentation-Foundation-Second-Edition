using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Extensions;
using CompanyName.ApplicationName.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// A View Model that is paired with the FeedbackView class, which demonstrates the use of the FeedbackControl class.
    /// </summary>
    public class FeedbackViewModel : BaseViewModel
    {
        private ObservableCollection<FeedbackType> feedbackTypes = new ObservableCollection<FeedbackType>();
        private FeedbackType selectedFeedbackType = FeedbackType.Error;
        private string feedbackMessage = string.Empty;
        private bool isPermanent = false;

        /// <summary>
        /// Initializes a new FeedbackViewModel object.
        /// </summary>
        public FeedbackViewModel()
        {
            feedbackTypes.FillWithMembers();
            feedbackTypes.Remove(FeedbackType.None);

            FeedbackManager.Feedback.Clear();
            Feedback feedback = new Feedback("Enter some text to feed into the feedback system.", FeedbackType.Information);
            feedback.IsPermanent = true;
            FeedbackManager.Add(feedback);
        }

        /// <summary>
        /// Gets or sets the collection of FeedbackType members, to be displayed in the View.
        /// </summary>
        public ObservableCollection<FeedbackType> FeedbackTypes
        {
            get { return feedbackTypes; }
            set { if (feedbackTypes != value) { feedbackTypes = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the selected FeedbackType member, to be used in the View.
        /// </summary>
        public FeedbackType SelectedFeedbackType
        {
            get { return selectedFeedbackType; }
            set { if (selectedFeedbackType != value) { selectedFeedbackType = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the feedback message to be displayed in the View.
        /// </summary>
        public string FeedbackMessage
        {
            get { return feedbackMessage; }
            set { if (feedbackMessage != value) { feedbackMessage = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the next Feedback element should be permanently displayed, or not.
        /// </summary>
        public bool IsPermanent
        {
            get { return isPermanent; }
            set { if (isPermanent != value) { isPermanent = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the ICommand instance that adds new Feedback items into the collection in the View.
        /// </summary>
        public ICommand AddFeedbackCommand
        {
            get { return new ActionCommand(action => AddFeedback()); }
        }

        private void AddFeedback()
        {
            Feedback feedback = new Feedback(FeedbackMessage, SelectedFeedbackType);
            feedback.IsPermanent = IsPermanent;
            FeedbackManager.Add(feedback);
        }
    }
}