using System.Windows;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides a borderless window in the shape of a speech bubble, to populate with custom content.
    /// </summary>
    public partial class CalloutWindow : Window
    {
        /// <summary>
        /// Overrides metadata of existing DependencyProperty elements.
        /// </summary>
        static CalloutWindow()
        {
            BorderBrushProperty.OverrideMetadata(typeof(CalloutWindow), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 238, 156, 88))));
            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(CalloutWindow), new FrameworkPropertyMetadata(HorizontalAlignment.Center));
            VerticalContentAlignmentProperty.OverrideMetadata(typeof(CalloutWindow), new FrameworkPropertyMetadata(VerticalAlignment.Center));
        }

        /// <summary>
        /// Initializes a new empty CalloutWindow object with default values.
        /// </summary>
        public CalloutWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents the Brush object to color the background of the window with.
        /// </summary>
        public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(CalloutWindow), new PropertyMetadata(new LinearGradientBrush(Colors.White, Color.FromArgb(255, 250, 191, 143), 90)));

        /// <summary>
        /// Gets or sets the Brush object to color the background of the window with.
        /// </summary>
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
    }
}