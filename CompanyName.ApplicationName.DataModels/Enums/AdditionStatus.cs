namespace CompanyName.ApplicationName.DataModels.Enums
{
    /// <summary>
    /// Represents the various addition statuses used for object animation in the application.
    /// </summary>
    public enum AdditionStatus
    {
        /// <summary>
        /// Represents an object that has no current entrance animations.
        /// </summary>
        None = -1,
        /// <summary>
        /// Represents an object that is ready to have its entrance animated.
        /// </summary>
        ReadyToAnimate = 0,
        /// <summary>
        /// Represents an object that will not have its entrance animated.
        /// </summary>
        DoNotAnimate = 1,
        /// <summary>
        /// Represents an object that has had its entrance animated and is now added.
        /// </summary>
        Added = 2
    }
}