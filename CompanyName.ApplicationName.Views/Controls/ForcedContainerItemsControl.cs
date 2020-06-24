using System.Windows.Controls;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Represents a control that can be used to present a collection of items and will always generate a new item container for each of them.
    /// </summary>
    public class ForcedContainerItemsControl : ItemsControl
    {
        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own container.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is (or is eligible to be) its own container; otherwise, false.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return false;
        }
    }
}