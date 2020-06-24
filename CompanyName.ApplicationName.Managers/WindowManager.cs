using System.Windows;
using CompanyName.ApplicationName.Managers.Interfaces;
using MessageBoxButton = CompanyName.ApplicationName.DataModels.Enums.MessageBoxButton;
using MessageBoxButtonSelection = CompanyName.ApplicationName.DataModels.Enums.MessageBoxButtonSelection;
using MessageBoxIcon = CompanyName.ApplicationName.DataModels.Enums.MessageBoxIcon;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Responsible for opening new windows and dialogs in the application.
    /// </summary>
    public class WindowManager : IWindowManager
    {
        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon and that returns a result.
        /// </summary>
        /// <param name="message">The text to display.</param>
        /// <param name="title">The title bar caption to display.</param>
        /// <param name="buttons">A MessageBoxButton enumeration value that specifies which button or buttons to display.</param>
        /// <param name="icon">A MessageBoxIcon enumeration value that specifies the icon to display.</param>
        /// <returns>A MessageBoxButtonSelection value that specifies which message box button is clicked by the user.</returns>
        public MessageBoxButtonSelection ShowMessageBox(string message, string title, MessageBoxButton buttons, MessageBoxIcon icon)
        {
            System.Windows.MessageBoxButton messageBoxButtons;
            switch (buttons)
            {
                case MessageBoxButton.Ok: messageBoxButtons = System.Windows.MessageBoxButton.OK; break;
                case MessageBoxButton.OkCancel: messageBoxButtons = System.Windows.MessageBoxButton.OKCancel; break;
                case MessageBoxButton.YesNo: messageBoxButtons = System.Windows.MessageBoxButton.YesNo; break;
                case MessageBoxButton.YesNoCancel: messageBoxButtons = System.Windows.MessageBoxButton.YesNoCancel; break;
                default: messageBoxButtons = System.Windows.MessageBoxButton.OKCancel; break;
            }
            MessageBoxImage messageBoxImage;
            switch (icon)
            {
                case MessageBoxIcon.Asterisk: messageBoxImage = MessageBoxImage.Asterisk; break;
                case MessageBoxIcon.Error: messageBoxImage = MessageBoxImage.Error; break;
                case MessageBoxIcon.Exclamation: messageBoxImage = MessageBoxImage.Exclamation; break;
                case MessageBoxIcon.Hand: messageBoxImage = MessageBoxImage.Hand; break;
                case MessageBoxIcon.Information: messageBoxImage = MessageBoxImage.Information; break;
                case MessageBoxIcon.None: messageBoxImage = MessageBoxImage.None; break;
                case MessageBoxIcon.Question: messageBoxImage = MessageBoxImage.Question; break;
                case MessageBoxIcon.Stop: messageBoxImage = MessageBoxImage.Stop; break;
                case MessageBoxIcon.Warning: messageBoxImage = MessageBoxImage.Warning; break;
                default: messageBoxImage = MessageBoxImage.Stop; break;
            }
            MessageBoxButtonSelection messageBoxButtonSelection = MessageBoxButtonSelection.None;
            switch (MessageBox.Show(message, title, messageBoxButtons, messageBoxImage))
            {
                case MessageBoxResult.Cancel: messageBoxButtonSelection = MessageBoxButtonSelection.Cancel; break;
                case MessageBoxResult.No: messageBoxButtonSelection = MessageBoxButtonSelection.No; break;
                case MessageBoxResult.OK: messageBoxButtonSelection = MessageBoxButtonSelection.Ok; break;
                case MessageBoxResult.Yes: messageBoxButtonSelection = MessageBoxButtonSelection.Yes; break;
            }
            return messageBoxButtonSelection;
        }
    }
}