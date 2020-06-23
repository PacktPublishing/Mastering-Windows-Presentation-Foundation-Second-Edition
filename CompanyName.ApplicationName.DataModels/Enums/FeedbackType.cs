namespace CompanyName.ApplicationName.DataModels.Enums
{
    /// <summary>
    /// Represents the various types of Feedback used in the application.
    /// </summary>
    public enum FeedbackType
    {
        /// <summary>
        /// Represents no Feedback.
        /// </summary>
        None = -1,
        /// <summary>
        /// Represents the type of Feedback that comes from a failed operation.
        /// </summary>
        Error,
        /// <summary>
        /// Represents the type of Feedback that displays information.
        /// </summary>
        Information,
        /// <summary>
        /// Represents the type of Feedback that asks a question.
        /// </summary>
        Question,
        /// <summary>
        /// Represents the type of Feedback that comes from a successful operation.
        /// </summary>
        Success,
        /// <summary>
        /// Represents the type of Feedback that validates a data field.
        /// </summary>
        Validation,
        /// <summary>
        /// Represents the type of Feedback that holds a warning for the user.
        /// </summary>
        Warning
    }
}