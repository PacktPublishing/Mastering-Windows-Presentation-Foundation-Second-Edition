namespace CompanyName.ApplicationName.DataModels.Enums
{
    /// <summary>
    /// Represents the various transition statuses used for object animation in the application.
    /// </summary>
    public enum TransitionStatus
    {
        /// <summary>
        /// Represents an object that has no current transition animations.
        /// </summary>
        None = -1,
        /// <summary>
        /// Represents an object that is ready to have its transition animated.
        /// </summary>
        ReadyToAnimate = 0,
        /// <summary>
        /// Represents an object that has had its transition animated.
        /// </summary>
        AnimationComplete = 1
    }
}