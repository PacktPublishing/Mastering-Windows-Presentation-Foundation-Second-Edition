using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Managers.Interfaces;

namespace Test.CompanyName.ApplicationName.Mocks.Managers
{
    /// <summary>
    /// Responsible for opening new windows in the application.
    /// </summary>
    public class MockWindowManager : IWindowManager
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
            switch (buttons)
            {
                case MessageBoxButton.Ok:
                case MessageBoxButton.OkCancel: return MessageBoxButtonSelection.Ok;
                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel: return MessageBoxButtonSelection.Yes;
                default: return MessageBoxButtonSelection.Ok;
            }
        }
    }
}