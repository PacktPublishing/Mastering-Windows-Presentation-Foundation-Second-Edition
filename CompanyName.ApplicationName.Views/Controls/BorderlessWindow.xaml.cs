using System.Windows;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides a borderless window to populate with custom content.
    /// </summary>
    public partial class BorderlessWindow : Window
    {
        /// <summary>
        /// Initializes a new empty BorderlessWindow object.
        /// </summary>
        public BorderlessWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += (sender, e) => DragMove();
        }
    }
}