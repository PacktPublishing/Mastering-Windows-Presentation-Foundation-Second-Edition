namespace CompanyName.ApplicationName.DataModels.Enums
{
    /// <summary>
    /// Represents the various validation levels used on the CompanyName.ApplicationName.Models classes in the application.
    /// </summary>
    public enum ValidationLevel
    {
        /// <summary>
        /// Represents an object that will not be validated.
        /// </summary>
        None,
        /// <summary>
        /// Represents a validation level that enables partial validation to occur on the object.
        /// </summary>
        Partial,
        /// <summary>
        /// Represents a validation level that enables full validation to occur on the object.
        /// </summary>
        Full
    }
}