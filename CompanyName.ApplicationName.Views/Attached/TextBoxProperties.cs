using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.ApplicationName.Views.Attached
{
    /// <summary>
    /// Contains attached properties for the System.Windows.Controls.TextBox control.
    /// </summary>
    public class TextBoxProperties : DependencyObject
    {
        #region IsNumericOnly

        /// <summary>
        /// Provides the ability to restrict the text input of the TextBox control to only allow numeric values to be entered.
        /// </summary>
        public static readonly DependencyProperty IsNumericOnlyProperty = DependencyProperty.RegisterAttached("IsNumericOnly", typeof(bool), typeof(TextBoxProperties), new UIPropertyMetadata(default(bool), OnIsNumericOnlyChanged));

        /// <summary>
        /// Gets the value of the IsNumericOnly property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to return the IsNumericOnly property value from.</param>
        /// <returns>The value of the IsNumericOnly property.</returns>
        public static bool GetIsNumericOnly(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsNumericOnlyProperty);
        }

        /// <summary>
        /// Sets the value of the IsNumericOnly property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to set the IsNumericOnly property value of.</param>
        /// <param name="value">The value to be assigned to the IsNumericOnly property.</param>
        public static void SetIsNumericOnly(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsNumericOnlyProperty, value);
        }

        private static void OnIsNumericOnlyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)dependencyObject;
            bool newIsNumericOnlyValue = (bool)e.NewValue;
            if (newIsNumericOnlyValue)
            {
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
                textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                DataObject.AddPastingHandler(textBox, TextBox_Pasting);
            }
            else
            {
                textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                DataObject.RemovePastingHandler(textBox, TextBox_Pasting);
            }
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = GetFullText((TextBox)sender, e.Text);
            e.Handled = !IsTextValid(text);
        }

        private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 1 && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                textBox.Text = "0";
                textBox.CaretIndex = 1;
                e.Handled = true;
            }
            else if (textBox.Text == "0") textBox.Clear();
            else e.Handled = e.Key == Key.Space;
        }

        private static void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = GetFullText((TextBox)sender, (string)e.DataObject.GetData(typeof(string)));
                if (!IsTextValid(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private static string GetFullText(TextBox textBox, string input)
        {
            return textBox.SelectedText.Length > 0 ? string.Concat(textBox.Text.Substring(0, textBox.SelectionStart), input, textBox.Text.Substring(textBox.SelectionStart + textBox.SelectedText.Length)) : textBox.Text.Insert(textBox.SelectionStart, input);
        }

        private static bool IsTextValid(string text)
        {
            return Regex.Match(text, @"^\d*\.?\d*$").Success;
        }

        #endregion

        #region OnEnterKeyDown

        /// <summary>
        /// Provides a TextBox with a bindable ICommand property that will be executed when the TextBox.PreviewKeyDown event registers that the Enter key has been pressed.
        /// </summary>
        public static readonly DependencyProperty OnEnterKeyDownProperty = DependencyProperty.RegisterAttached("OnEnterKeyDown", typeof(ICommand), typeof(TextBoxProperties), new PropertyMetadata(OnOnEnterKeyDownChanged));

        /// <summary>
        /// Gets the value of the OnEnterKeyDown property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to return the OnEnterKeyDown property value from.</param>
        /// <returns>The value of the OnEnterKeyDown property.</returns>
        public static ICommand GetOnEnterKeyDown(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(OnEnterKeyDownProperty);
        }

        /// <summary>
        /// Sets the value of the OnEnterKeyDown property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to set the OnEnterKeyDown property value of.</param>
        /// <param name="value">The value to be assigned to the OnEnterKeyDown property.</param>
        public static void SetOnEnterKeyDown(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(OnEnterKeyDownProperty, value);
        }

        private static void OnOnEnterKeyDownChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)dependencyObject;
            if (e.OldValue == null && e.NewValue != null) textBox.PreviewKeyDown += TextBox_OnEnterKeyDown;
            else if (e.OldValue != null && e.NewValue == null) textBox.PreviewKeyDown -= TextBox_OnEnterKeyDown;
        }

        private static void TextBox_OnEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                TextBox textBox = sender as TextBox;
                ICommand command = GetOnEnterKeyDown(textBox);
                if (command != null && command.CanExecute(textBox)) command.Execute(textBox);
            }
        }

        #endregion

        #region IsFocused

        /// <summary>
        /// Provides a TextBox with a bindable boolean property that causes the TextBox to become focused as it is set to true.
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(TextBoxProperties), new PropertyMetadata(false, OnIsFocusedChanged));

        /// <summary>
        /// Gets the value of the IsFocused property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to return the IsFocused property value from.</param>
        /// <returns>The value of the IsFocused property.</returns>
        public static bool GetIsFocused(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsFocusedProperty);
        }

        /// <summary>
        /// Sets the value of the IsFocused property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to set the IsFocused property value of.</param>
        /// <param name="value">The value to be assigned to the IsFocused property.</param>
        public static void SetIsFocused(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsFocusedProperty, value);
        }

        private static void OnIsFocusedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = dependencyObject as TextBox;
            if ((bool)e.NewValue && !(bool)e.OldValue && !textBox.IsFocused) textBox.Focus();
        }

        #endregion
    }
}