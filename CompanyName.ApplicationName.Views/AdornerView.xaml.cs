using CompanyName.ApplicationName.Views.Adorners;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CompanyName.ApplicationName.Views
{
    /// <summary>
    /// Interaction logic for AdornerView.xaml
    /// </summary>
    public partial class AdornerView : UserControl
    {
        /// <summary>
        /// Initializes a new AdornerView object.
        /// </summary>
        public AdornerView()
        {
            InitializeComponent();
            Loaded += View_Loaded;
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(Canvas);
            foreach (UIElement uiElement in Canvas.Children)
            {
                adornerLayer.Add(new ResizeAdorner(uiElement));
            }
        }
    }
}