namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// A View Model that is paired with the MeterView class, which provides the data to recreate the Meter example from the book.
    /// </summary>
    public class MeterViewModel : BaseViewModel
    {
        private double cpuActivity = 0.9, diskActivity = 0.2, networkActivity = 0.6;

        /// <summary>
        /// Gets or sets the value of the CPU Activity meter, to be displayed in the View.
        /// </summary>
        public double CpuActivity
        {
            get { return cpuActivity; }
            set { cpuActivity = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the value of the Disk Activity meter, to be displayed in the View.
        /// </summary>
        public double DiskActivity
        {
            get { return diskActivity; }
            set { diskActivity = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the value of the Network Activity meter, to be displayed in the View.
        /// </summary>
        public double NetworkActivity
        {
            get { return networkActivity; }
            set { networkActivity = value; NotifyPropertyChanged(); }
        }
    }
}