using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Enums;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Responsible for maintaining the state of commonly used data in the application.
    /// </summary>
    public class StateManager
    {
        private static StateManager instance;

        private StateManager() { }

        /// <summary>
        /// Gets the single available instance of the application StateManager object.
        /// </summary>
        public static StateManager Instance
        {
            get { return instance ?? (instance = new StateManager()); }
        }

        /// <summary>
        /// Gets or sets the User that is currently logged in to the application.
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the value that specifies whether the current view should display audit fields or not.
        /// </summary>
        public bool AreAuditFieldsVisible { get; set; }

        /// <summary>
        /// Gets or sets the value that specifies whether the user's search terms should be persisted or not.
        /// </summary>
        public bool AreSearchTermsSaved { get; set; }

        /// <summary>
        /// Gets or sets the value that represents the rendering tier of the user's graphics card.
        /// </summary>
        public RenderingTier RenderingTier { get; set; }
    }
}