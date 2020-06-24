namespace CompanyName.ApplicationName.DataModels.Enums
{
    /// <summary>
    /// Represents the various removal statuses used for object animation in the application.
    /// </summary>
    public enum RemovalStatus
    {
        /// <summary>
        /// Represents an object that has no current exit animations.
        /// </summary>
        None = -1,
        /// <summary>
        /// Represents an object that is ready to have its exit animated.
        /// </summary>
        ReadyToAnimate = 0,
        /// <summary>
        /// Represents an object that has had its exit animated and is now ready to be removed.
        /// </summary>
        ReadyToRemove = 1
    }
}