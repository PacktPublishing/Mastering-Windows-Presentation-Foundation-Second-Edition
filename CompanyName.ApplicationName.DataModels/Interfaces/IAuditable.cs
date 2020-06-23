namespace CompanyName.ApplicationName.DataModels.Interfaces
{
    /// <summary>
    /// Provides the Auditable member that is required to audit changes made to data objects.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the Auditable object that provides members required to audit changes made to data objects.
        /// </summary>
        Auditable Auditable { get; set; }
    }
}