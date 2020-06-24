using System.ComponentModel;
using System.Windows;
using CompanyName.ApplicationName.Managers;

namespace CompanyName.ApplicationName.Views.ViewModelLocators
{
    /// <summary>
    /// Provides a generic framework for supplying different View Models at runtime and design time.
    /// </summary>
    /// <typeparam name="T">The interface type that specifies which types of View Model to be used.</typeparam>
    public abstract class BaseViewModelLocator<T> : DependencyObject where T : class
    {
        private T runtimeViewModel, designTimeViewModel;

        /// <summary>
        /// Gets a value that specifies whether the application is currently in design time, or runtime and therefore, which View Model to supply from the ViewModel property.
        /// </summary>
        protected bool IsDesignTime
        {
            get { return DesignerProperties.GetIsInDesignMode(this); }
        }

        /// <summary>
        /// Gets a View Model dependent upon whether the application is currently in design time, or runtime.
        /// </summary>
        public T ViewModel
        {
            get { return IsDesignTime ? DesignTimeViewModel : RuntimeViewModel; }
        }

        /// <summary>
        /// Gets the View Model to be used at runtime.
        /// </summary>
        protected T RuntimeViewModel
        {
            get { return runtimeViewModel ?? (runtimeViewModel = DependencyManager.Instance.Resolve<T>()); }
        }

        /// <summary>
        /// Gets or set the View Model to be used at design time.
        /// </summary>
        protected T DesignTimeViewModel
        {
            set { designTimeViewModel = value; }
            get { return designTimeViewModel; }
        }
    }
}