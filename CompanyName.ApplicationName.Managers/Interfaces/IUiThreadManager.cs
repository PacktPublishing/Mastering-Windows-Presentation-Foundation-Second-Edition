using System;
using System.Threading.Tasks;

namespace CompanyName.ApplicationName.Managers.Interfaces
{
    /// <summary>
    /// Responsible for managing UI access to asynchronous data in the application.
    /// </summary>
    public interface IUiThreadManager
    {
        /// <summary>
        /// Executes the specified delegate synchronously on the main UI thread.
        /// </summary>
        /// <param name="method">A delegate to a method, which is pushed onto the System.Windows.Threading.Dispatcher event queue.</param>
        /// <returns>The return value from the delegate being invoked or null if the delegate has no return value.</returns>
        object RunOnUiThread(Delegate method);

        /// <summary>
        /// Executes the specified Action asynchronously on a background thread.
        /// </summary>
        /// <param name="method">An Action delegate to execute asynchronously.</param>
        /// <exception cref="System.ArgumentNullException">The action input parameter is null.</exception>
        /// <returns>The started asynchronous Task object.</returns>
        Task RunAsynchronously(Action method);

        /// <summary>
        /// Queues the specified work to run on the ThreadPool and returns a Task(TResult) handle for that work.
        /// </summary>
        /// <typeparam name="TResult">The result type of the task.</typeparam>
        /// <param name="method">The work to execute asynchronously.</param>
        /// <exception cref="System.ArgumentNullException">The function parameter was null.</exception>
        /// <returns>A Task&lt;TResult&gt; that represents the work queued to execute in the ThreadPool.</returns>
        Task<TResult> RunAsynchronously<TResult>(Func<TResult> method);
    }
}