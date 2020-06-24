using System.Collections.ObjectModel;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides the properties necessary to demonstrate how to data bind enumeration members to UI controls and to convert data bound values.
    /// </summary>
    public class BitRateViewModel : BaseViewModel
    {
        private ObservableCollection<BitRate> bitRates = new ObservableCollection<BitRate>();
        private BitRate bitRate = BitRate.Sixteen;

        /// <summary>
        /// Initializes a new BitRateViewModel with default values.
        /// </summary>
        public BitRateViewModel()
        {
            bitRates.FillWithMembers();
        }

        /// <summary>
        /// Gets or sets the collection of BitRate members.
        /// </summary>
        public ObservableCollection<BitRate> BitRates
        {
            get { return bitRates; }
            set { if (bitRates != value) { bitRates = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the BitRate member that is selected in the View.
        /// </summary>
        public BitRate BitRate
        {
            get { return bitRate; }
            set { if (bitRate != value) { bitRate = value; NotifyPropertyChanged(); } }
        }
    }
}