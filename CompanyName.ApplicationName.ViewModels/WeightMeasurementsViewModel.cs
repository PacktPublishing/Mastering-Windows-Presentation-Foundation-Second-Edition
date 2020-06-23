using System.Collections.Generic;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a pre-populated collection of integers to the UI to demonstrate the use of the IMultiValueConverter interface.
    /// </summary>
    public class WeightMeasurementsViewModel : BaseViewModel
    {
        private List<int> weights = new List<int>() { 90, 89, 92, 91, 94, 95, 98, 99, 101 };

        /// <summary>
        /// Gets or sets the collection of integers to be used to demonstrate the use of the IMultiValueConverter interface.
        /// </summary>
        public List<int> Weights
        {
            get { return weights; }
            set { weights = value; NotifyPropertyChanged(); }
        }
    }
}