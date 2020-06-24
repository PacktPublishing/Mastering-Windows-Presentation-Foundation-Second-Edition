using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// A View Model that is paired with the PanelView class, which provides the data to recreate the various panel-related examples from the book.
    /// </summary>
    public class PanelViewModel : BaseViewModel
    {
        private List<int> hours = new List<int>() { 12 }, days = Enumerable.Range(1, 31).ToList();

        /// <summary>
        /// Initializes a new PanelViewModel object.
        /// </summary>
        public PanelViewModel()
        {
            hours.AddRange(Enumerable.Range(1, 11));
        }

        /// <summary>
        /// Gets or sets the collection of hour numerals, to be displayed in the Circumference Panel example in the View.
        /// </summary>
        public List<int> Hours
        {
            get { return hours; }
            set { hours = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the collection of day numerals, to be displayed in the Uniform Grid example in the View.
        /// </summary>
        public List<int> Days
        {
            get { return days; }
            set { days = value; NotifyPropertyChanged(); }
        }
    }
}