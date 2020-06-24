using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides a way to enable the text to leak out of the bounds of the TextBlock element.
    /// </summary>
    public partial class BoundlessTextBlock : TextBlock
    {
        /// <summary>
        /// Initializes a new empty BoundlessTextBlock object.
        /// </summary>
        public BoundlessTextBlock()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns a geometry for a clipping mask. The mask applies if the layout system attempts to arrange an element that is larger than the available display space.
        /// </summary>
        /// <param name="layoutSlotSize">The size of the part of the element that does visual presentation.</param>
        /// <returns>The clipping geometry.</returns>
        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            return null;
        }
    }
}