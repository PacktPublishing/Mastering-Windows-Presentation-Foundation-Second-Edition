namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides the properties necessary to demonstrate how to make data visually appealing.
    /// </summary>
    public class VisuallyAppealingViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the value of the fictional number of support tickets that have come in.
        /// </summary>
        public int InCount { get; set; } = 148;

        /// <summary>
        /// Gets or sets the value of the fictional number of support tickets that have been dealt with, or gone out.
        /// </summary>
        public int OutCount { get; set; } = 112;
    }
}