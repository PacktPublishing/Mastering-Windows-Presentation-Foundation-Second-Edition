using System;
using System.Threading.Tasks;
using CompanyName.ApplicationName.Managers.Interfaces;

namespace Test.CompanyName.ApplicationName.Mocks.Managers
{
    /// <summary>
    /// Responsible for running asynchronous data operations synchronously for testing purposes.
    /// </summary>
    public class MockDataAsynchronyManager : IDataAsynchronyManager
    {
        /// <summary>
        /// Queues the specified work to run on the ThreadPool and returns a Task(TResult) handle for that work.
        /// </summary>
        /// <typeparam name="TResult">The result type of the task.</typeparam>
        /// <param name="method">The work to execute asynchronously.</param>
        /// <exception cref="System.ArgumentNullException">The function parameter was null.</exception>
        /// <returns>A Task&lt;TResult&gt; that represents the work queued to execute in the ThreadPool.</returns>
        public Task<TResult> RunAsynchronously<TResult>(Func<TResult> method)
        {
            Task<TResult> task = new Task<TResult>(() => method());
            task.RunSynchronously();
            return task;
        }
    }
}