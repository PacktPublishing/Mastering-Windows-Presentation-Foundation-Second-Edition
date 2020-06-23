using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides an easy way to output a text string as a graphical object using an efficient FormattedText object.
    /// </summary>
    public class FormattedTextOutput : FrameworkElement
    {
        /// <summary>
        /// Represents the text value that is to be displayed in the graphical FormattedText object.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(FormattedTextOutput), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the text value that is to be displayed in the graphical FormattedText object.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            DpiScale dpiScale = VisualTreeHelper.GetDpi(this);            
            FormattedText formattedText = new FormattedText(Text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Times New Roman"), 50, Brushes.Red, dpiScale.PixelsPerDip);
            formattedText.SetFontStyle(FontStyles.Italic);
            formattedText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(formattedText, new Point(10, 0));
        }
    }
}