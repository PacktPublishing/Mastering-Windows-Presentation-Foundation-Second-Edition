namespace CompanyName.ApplicationName.DataModels.Interfaces
{
    /// <summary>
    /// Provides the Animatable member that is required to enable objects to participate in various animations in the View.
    /// </summary>
    public interface IAnimatable
    {
        /// <summary>
        /// Gets or sets the Animatable object that enables the use of the animated features of various animatable controls in the View.
        /// </summary>
        Animatable Animatable { get; set; }
    }
}