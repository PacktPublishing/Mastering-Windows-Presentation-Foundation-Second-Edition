//using System.Configuration;
using System.Windows;
using System.Windows.Media;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Managers;
using CompanyName.ApplicationName.Managers.Interfaces;
using CompanyName.ApplicationName.Models.DataProviders;
using CompanyName.ApplicationName.Models.Interfaces;
using CompanyName.ApplicationName.ViewModels;
using CompanyName.ApplicationName.ViewModels.Interfaces;

namespace CompanyName.ApplicationName
{
    /// <summary>
    /// The starting point for a WPF application
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new WPF application.
        /// </summary>
        public App()
        {
            StateManager.Instance.RenderingTier = (RenderingTier)(RenderCapability.Tier >> 16);
            //FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
        }

        /// <summary>
        /// Handles the Application.Startup event and is the startup method of the application - the first to be called.
        /// </summary>
        /// <param name="sender">The Application object that represents the application.</param>
        /// <param name="e">The StartupEventArgs object containing application startup arguments if any are used.</param>
        public void App_Startup(object sender, StartupEventArgs e)
        {
            RegisterDependencies();
            new MainWindow().Show();
        }

        private void RegisterDependencies()
        {
            DependencyManager.Instance.ClearRegistrations();
            DependencyManager.Instance.Register<IDataProvider, ApplicationDataProvider>();
            DependencyManager.Instance.Register<IUiThreadManager, UiThreadManager>();
            DependencyManager.Instance.Register<IHardDriveManager, HardDriveManager>();
            DependencyManager.Instance.Register<IUserViewModel, UserViewModel>();
        }
    }
}