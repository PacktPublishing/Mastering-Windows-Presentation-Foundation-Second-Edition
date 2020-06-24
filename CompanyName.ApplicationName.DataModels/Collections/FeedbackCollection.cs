using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ApplicationName.DataModels.Collections
{
    /// <summary>
    /// Represents a collection of feedback messages to be displayed in the application.
    /// </summary>
    public class FeedbackCollection : BaseAnimatableCollection<Feedback>
    {
        /// <summary>
        /// Initializes a new FeedbackCollection collection and adds the Feedback objects from the collection specified by the feedbackCollection input parameter.
        /// </summary>
        /// <param name="feedbackCollection">The collection of Feedback objects to initialise this collection with.</param>
        public FeedbackCollection(IEnumerable<Feedback> feedbackCollection) : base(feedbackCollection) { }

        /// <summary>
        /// Initializes a new empty FeedbackCollection collection.
        /// </summary>
        public FeedbackCollection() : base() { }

        /// <summary>
        /// Adds a Feedback object to the end of the collection if it is not already in the collection.
        /// </summary>
        /// <param name="feedback">The object to be added to the end of the collection. The value can be null.</param>
        public new void Add(Feedback feedback)
        {
            if (!string.IsNullOrEmpty(feedback.Message) && (Count == 0 || !this.Any(f => f.Message == feedback.Message))) base.Add(feedback);
        }

        /// <summary>
        /// Creates and adds a new Feedback object to the end of the collection if it is not already in the collection.
        /// </summary>
        /// <param name="message">The message of the new Feedback object.</param>
        /// <param name="isSuccess">The value that specifies whether the new Feedback object has a FeedbackType of FeedbackType.Success or FeedbackType.Error.</param>
        public void Add(string message, bool isSuccess)
        {
            Add(new Feedback(message, isSuccess));
        }
    }
}