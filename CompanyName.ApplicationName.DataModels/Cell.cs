namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Proivides properties that describe a cell in a spreadsheet.
    /// </summary>
    public class Cell : BaseDataModel
    {
        private string address = string.Empty, content = string.Empty;
        private double width = 0;

        /// <summary>
        /// Initializes a new Cell object with the values from the input parameters.
        /// </summary>
        public Cell(string address, string content, double width)
        {
            Address = address;
            Content = content;
            Width = width;
        }

        /// <summary>
        /// Gets or sets the address of the Cell object within the spreadsheet.
        /// </summary>
        public string Address
        {
            get { return address; }
            set { if (address != value) { address = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the content of the Cell object.
        /// </summary>
        public string Content
        {
            get { return content; }
            set { if (content != value) { content = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the width of the Cell object.
        /// </summary>
        public double Width
        {
            get { return width; }
            set { if (width != value) { width = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Address}: {Content}";
        }
    }
}