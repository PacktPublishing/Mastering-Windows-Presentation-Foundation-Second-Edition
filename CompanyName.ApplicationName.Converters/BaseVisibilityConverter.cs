using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Provides common properties for all converters that convert to the System.Windows.Visibility enum.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public abstract class BaseVisibilityConverter
    {
        /// <summary>
        /// The enum type that provides possible values that can be used as the false visibility state in this converter.
        /// </summary>
        protected Visibility FalseVisibilityValue { get; set; } = Visibility.Collapsed; 

        /// <summary>
        /// The enum type that provides possible values that can be used as the false visibility state in this converter.
        /// </summary>
        public enum FalseVisibility
        {
            /// <summary>
            /// Represents the Visibility.Hidden enumeration.
            /// </summary>
            Hidden,
            /// <summary>
            /// Represents the Visibility.Collapsed enumeration.
            /// </summary>
            Collapsed
        }

        /// <summary>
        /// Gets or sets the particular enumeration from the System.Windows.Visibility enum that will be used as the false visibility state in this converter.
        /// </summary>
        public FalseVisibility FalseVisibilityState
        {
            get { return FalseVisibilityValue == Visibility.Collapsed ? FalseVisibility.Collapsed : FalseVisibility.Hidden; }
            set { FalseVisibilityValue = value == FalseVisibility.Collapsed ? Visibility.Collapsed : Visibility.Hidden; }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the output System.Windows.Visibility value is inverted during conversion or not.
        /// </summary>
        public bool IsInverted { get; set; }
    }
}