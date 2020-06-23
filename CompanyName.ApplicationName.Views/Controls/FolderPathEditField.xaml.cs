using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides an easy way to select a file path in the application.
    /// </summary>
    public partial class FolderPathEditField : UserControl
    {
        /// <summary>
        /// Initializes a new empty FolderPathEditField control with default values.
        /// </summary>
        public FolderPathEditField()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents the text value that is displayed in the read only FolderPathBox in the FolderPathEditField control.
        /// </summary>
        public static readonly DependencyProperty FolderPathProperty = DependencyProperty.Register(nameof(FolderPath), typeof(string), typeof(FolderPathEditField), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Gets or sets the text value that is displayed in the read only TextBox in the FolderPathEditField control.
        /// </summary>
        public string FolderPath
        {
            get { return (string)GetValue(FolderPathProperty); }
            set { SetValue(FolderPathProperty, value); }
        }

        /// <summary>
        /// Represents the text value that is displayed in the read only TextBox in the FolderPathEditField control.
        /// </summary>
        public static readonly DependencyProperty OpenFolderTitleProperty = DependencyProperty.Register(nameof(OpenFolderTitle), typeof(string), typeof(FolderPathEditField), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Gets or sets the text value that is displayed in the read only TextBox in the FolderPathEditField control.
        /// </summary>
        public string OpenFolderTitle
        {
            get { return (string)GetValue(OpenFolderTitleProperty); }
            set { SetValue(OpenFolderTitleProperty, value); }
        }

        private void TextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (((TextBox)sender).SelectedText.Length == 0 && e.GetPosition(this).X <= ((TextBox)sender).ActualWidth - SystemParameters.VerticalScrollBarWidth) ShowFolderPathEditWindow();
        }

        private void ShowFolderPathEditWindow()
        {
            string defaultFolderPath = string.IsNullOrEmpty(FolderPath) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : FolderPath;
            string folderPath = ShowFolderBrowserDialog(defaultFolderPath);
            if (string.IsNullOrEmpty(folderPath)) return;
            FolderPath = folderPath;
        }

        private string ShowFolderBrowserDialog(string defaultFolderPath)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = OpenFolderTitle;
                folderBrowserDialog.ShowNewFolderButton = true;
                folderBrowserDialog.SelectedPath = defaultFolderPath;
                folderBrowserDialog.ShowDialog();
                return folderBrowserDialog.SelectedPath;
            }
        }
    }
}