using System;
using System.Windows.Input;

namespace CompanyName.ApplicationName.ViewModels.Commands
{
    /// <summary>
    /// Provides a reusable implementation of the ICommand interface that enables user to create commands from delegates.
    /// </summary>
    public class ActionCommand : ICommand
    {
        readonly Action<object> action;
        readonly Predicate<object> canExecute;
        private EventHandler eventHandler;

        /// <summary>
        /// Initializes a new ActionCommand object with the value provided by the input parameter.
        /// </summary>
        /// <param name="action">The Action&gt;object&lt; that will be performed when the command is executed.</param>
        public ActionCommand(Action<object> action) : this(action, null) { }

        /// <summary>
        /// Initializes a new ActionCommand object with the values provided by the input parameters.
        /// </summary>
        /// <param name="action">The Action&gt;object&gt; that will be performed when the command is executed.</param>
        /// <param name="canExecute">A Predicate&lt;object&gt; that determines whether the Action&gt;object&gt; object specified by the action input parameter can execute or not.</param>
        public ActionCommand(Action<object> action, Predicate<object> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the related command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                eventHandler += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                eventHandler -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Raises the CanExecuteChanged event, to alert the command system that the conditions in the CanExecute method have changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            eventHandler?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Returns a bool value that specifies whether the Action&gt;object&gt; object specified by the action input parameter can execute or not.
        /// </summary>
        /// <param name="parameter">The ICommand.Parameter object that can be used in the method.</param>
        /// <returns>A bool value that specifies whether the Action&gt;object&gt; object specified by the action input parameter can execute or not.</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        /// <summary>
        /// Executes the Action&gt;object&gt; object specified by the action input parameter in the constructor if the CanExecute method returns true.
        /// </summary>
        /// <param name="parameter">The ICommand.Parameter object that can be used in the method.</param>
        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}