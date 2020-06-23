using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.ApplicationName.Views.Attached
{
    /// <summary>
    /// Contains attached properties for the System.Windows.Controls.TextBlock control.
    /// </summary>
    public class TextBlockProperties : DependencyObject
    {
        #region OnMouseLeftButtonUp

        /// <summary>
        /// Provides a TextBlock with a bindable ICommand property that will be executed when the TextBlock.OnMouseLeftButtonUp event is raised.
        /// </summary>
        public static DependencyProperty OnMouseLeftButtonUpProperty = DependencyProperty.RegisterAttached("OnMouseLeftButtonUp", typeof(ICommand), typeof(TextBlockProperties), new
            PropertyMetadata(OnOnMouseLeftButtonUpChanged));

        /// <summary>
        /// Gets the value of the OnMouseLeftButtonUp property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to return the OnMouseLeftButtonUp property value from.</param>
        /// <returns>The value of the OnMouseLeftButtonUp property.</returns>
        public static ICommand GetOnMouseLeftButtonUp(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(OnMouseLeftButtonUpProperty);
        }

        /// <summary>
        /// Sets the value of the OnMouseLeftButtonUp property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to set the OnMouseLeftButtonUp property value of.</param>
        /// <param name="value">The value to be assigned to the OnMouseLeftButtonUp property.</param>
        public static void SetOnMouseLeftButtonUp(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(OnMouseLeftButtonUpProperty, value);
        }

        /// <summary>
        /// Attaches an event handler for the TextBlock.OnMouseLeftButtonUp event if an ICommand is bound to the OnMouseLeftButtonUp property and removes it if an ICommand is removed.
        /// </summary>
        /// <param name="dependencyObject">The TextBlock object.</param>
        /// <param name="e">The DependencyPropertyChangedEventArgs object containing event specific information.</param>
        private static void OnOnMouseLeftButtonUpChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBlock TextBlock = (TextBlock)dependencyObject;
            if (e.OldValue == null && e.NewValue != null) TextBlock.MouseLeftButtonUp += TextBlock_MouseLeftButtonUp;
            else if (e.OldValue != null && e.NewValue == null) TextBlock.MouseLeftButtonUp -= TextBlock_MouseLeftButtonUp;
        }

        private static void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            ICommand command = GetOnMouseLeftButtonUp(textBlock);
            if (command != null && command.CanExecute(textBlock)) command.Execute(textBlock);
        }

        #endregion
    }
}