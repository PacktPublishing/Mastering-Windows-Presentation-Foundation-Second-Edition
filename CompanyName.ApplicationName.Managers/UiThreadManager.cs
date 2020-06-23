using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CompanyName.ApplicationName.Managers.Interfaces;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Responsible for managing UI thread access to asynchronous data in the application.
    /// </summary>
    public class UiThreadManager : IUiThreadManager
    {
        /// <summary>
        /// Executes the specified delegate synchronously on the main UI thread.
        /// </summary>
        /// <param name="method">A delegate to a method, which is pushed onto the System.Windows.Threading.Dispatcher event queue.</param>
        /// <returns>The return value from the delegate being invoked, or null if the delegate has no return value.</returns>
        /// <exception cref="AccessViolationException">An AccessViolationException will be thrown if the UiThreadManager.Dispatcher object is not running on the main thread.</exception>
        public object RunOnUiThread(Delegate method)
        {
            return Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, method);
        }

        /// <summary>
        /// Executes the specified Action asynchronously on a background thread.
        /// </summary>
        /// <param name="method">An Action delegate to execute asynchronously.</param>
        /// <exception cref="System.ArgumentNullException">The action input parameter is null.</exception>
        /// <returns>The started asynchronous Task object.</returns>
        public Task RunAsynchronously(Action method)
        {
            return Task.Run(method);
        }

        /// <summary>
        /// Queues the specified work to run on the ThreadPool and returns a Task&lt;TResult&gt; handle for that work.
        /// </summary>
        /// <typeparam name="TResult">The result type of the task.</typeparam>
        /// <param name="method">The work to execute asynchronously.</param>
        /// <exception cref="System.ArgumentNullException">The function parameter was null.</exception>
        /// <returns>A Task&lt;TResult&gt; that represents the work queued to execute in the ThreadPool.</returns>
        public Task<TResult> RunAsynchronously<TResult>(Func<TResult> method)
        {
            return Task.Run(method);
        }
    }
}