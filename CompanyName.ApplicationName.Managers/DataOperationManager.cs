using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.Managers.Interfaces;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Provides a way to try and retry anonymous delegate methods if they fail, to catch exceptions and wrap the results in DataOperationResult&lt;T&gt; objects.
    /// </summary>
    public class DataOperationManager
    {
        private const int maximumRetryCount = 2;
        private IUiThreadManager uiThreadManager;

        /// <summary>
        /// Initializes a new DataOperationManager object with the value provided by the input parameter.
        /// </summary>
        /// <param name="uiThreadManager">The IUiThreadManager object to use to run data operations asynchronously.</param>
        public DataOperationManager(IUiThreadManager uiThreadManager)
        {
            UiThreadManager = uiThreadManager;
        }

        private IUiThreadManager UiThreadManager
        {
            get { return uiThreadManager; }
            set { uiThreadManager = value; }
        }
        
        private FeedbackManager FeedbackManager
        {
            get { return FeedbackManager.Instance; }
        }

        /// <summary>
        /// Tries to execute the specified getter method with the number of retry attempts specified by the maximumRetryCount input parameter.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The getter method to try to execute.</param>
        /// <param name="successText">The text to use to describe a successfull data operation.</param>
        /// <param name="errorText">The text to use to describe an  unsuccessfull data operation.</param>
        /// <param name="isMessageSupressed">The value that sepecifies whether the related Feeback.Message should be supressed or not.</param>
        /// <returns>A GetDataOperationResult object containing the result of the method specified by the method input parameter if it was successfull, or details specifying the error or exception if not.</returns>
        public GetDataOperationResult<TResult> TryGet<TResult>(Func<TResult> method, string successText, string errorText, bool isMessageSupressed)
        {
            Debug.Assert(method != null, "The method input parameter of the DataOperationManager.TryGet<TResult>() method must not be null.");
            for (int index = 0; index < maximumRetryCount; index++)
            {
                try
                {
                    TResult result = method();
                    return WithFeedback(new GetDataOperationResult<TResult>(result, successText), isMessageSupressed);
                }
                catch (Exception exception)
                {
                    if (index == maximumRetryCount - 1)
                    {
                        return WithFeedback(new GetDataOperationResult<TResult>(exception, errorText), isMessageSupressed);
                    }
                    Task.Delay(TimeSpan.FromMilliseconds(300));
                }
            } 
            return WithFeedback(new GetDataOperationResult<TResult>(default(TResult), successText), isMessageSupressed);
        }

        private GetDataOperationResult<TResult> WithFeedback<TResult>(GetDataOperationResult<TResult> dataOperationResult, bool isMessageSupressed)
        {
            if (isMessageSupressed && dataOperationResult.IsSuccess) return dataOperationResult;
            FeedbackManager.Add(dataOperationResult, false);
            return dataOperationResult;
        }
        
        /// <summary>
        /// Tries to execute the specified getter method asynchronously with the number of retry attempts specified by the maximumRetryCount input parameter.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The getter method to try to execute.</param>
        /// <param name="successText">The text to use to describe a successfull data operation.</param>
        /// <param name="errorText">The text to use to describe an unsuccessfull data operation.</param>
        /// <param name="isMessageSupressed">The value that sepecifies whether the related Feeback.Message should be supressed or not.</param>
        /// <returns>A GetDataOperationResult object containing the result of the method specified by the method input parameter if it was successfull, or details specifying the error or exception if not.</returns>
        public Task<GetDataOperationResult<TResult>> TryGetAsync<TResult>(Func<TResult> method, string successText, string errorText, bool isMessageSupressed)
        {
            return UiThreadManager.RunAsynchronously(() => TryGet(method, successText, errorText, isMessageSupressed));
        }

        /// <summary>
        /// Tries to execute the specified setter method with the number of retry attempts specified by the maximumRetryCount input parameter.
        /// </summary>
        /// <param name="method">The setter method to try to execute.</param>
        /// <param name="successText">The text to use to describe a successfull data operation.</param>
        /// <param name="errorText">The text to use to describe an  unsuccessfull data operation.</param>
        /// <param name="isMessagePermanent">The value that specifies whether the Feedback object should be removed automatically after a time period or not.</param>
        /// <param name="isMessageSupressed">The value that sepecifies whether the related Feeback.Message should be supressed or not.</param>
        /// <returns>A SetDataOperationResult object containing the result and details specifying whether the data operation was a success or not.</returns>
        public SetDataOperationResult TrySet(Action method, string successText, string errorText, bool isMessagePermanent, bool isMessageSupressed)
        {
            Debug.Assert(method != null, "The method input parameter of the DataOperationManager.TrySet<TResult>() method must not be null.");
            for (int index = 0; index < maximumRetryCount; index++)
            {
                try
                {
                    method();
                    return WithFeedback(new SetDataOperationResult(successText), isMessagePermanent, isMessageSupressed);
                }
                catch (Exception exception)
                {
                    if (index == maximumRetryCount - 1)
                    {
                        return WithFeedback(new SetDataOperationResult(exception, errorText), isMessagePermanent, isMessageSupressed);
                    }
                    Task.Delay(TimeSpan.FromMilliseconds(300));
                }
            }
            return WithFeedback(new SetDataOperationResult(successText), isMessagePermanent, isMessageSupressed);
        }

        private SetDataOperationResult WithFeedback(SetDataOperationResult dataOperationResult, bool isMessagePermanent, bool isMessageSupressed)
        {
            if (isMessageSupressed && dataOperationResult.IsSuccess) return dataOperationResult;
            FeedbackManager.Add(dataOperationResult, isMessagePermanent);
            return dataOperationResult;
        }

        /// <summary>
        /// Tries to execute the specified setter method specified by the input parameter asynchronously a maximum of two times.
        /// </summary>
        /// <param name="method">The setter method to try to execute.</param>
        /// <returns>A SetDataOperationResult object containing the result and details specifying whether the data operation was a success or not.</returns>
        public Task<SetDataOperationResult> TrySetAsync(Action method)
        {
            return TrySetAsync(method, string.Empty, string.Empty);
        }

        /// <summary>
        /// Tries to execute the specified setter method specified by the input parameter asynchronously a maximum of two times.
        /// </summary>
        /// <param name="method">The setter method to try to execute.</param>
        /// <param name="successText">The text to use to describe a successfull data operation.</param>
        /// <param name="errorText">The text to use to describe an  unsuccessfull data operation.</param>
        /// <returns>A SetDataOperationResult object containing the result and details specifying whether the data operation was a success or not.</returns>
        public Task<SetDataOperationResult> TrySetAsync(Action method, string successText, string errorText)
        {
            return TrySetAsync(method, successText, errorText, false, false);
        }

        /// <summary>
        /// Tries to execute the specified setter method asynchronously with the number of retry attempts specified by the maximumRetryCount input parameter.
        /// </summary>
        /// <param name="method">The setter method to try to execute.</param>
        /// <param name="successText">The text to use to describe a successfull data operation.</param>
        /// <param name="errorText">The text to use to describe an  unsuccessfull data operation.</param>
        /// <param name="isMessagePermanent">The value that specifies whether the Feedback object should be removed automatically after a time period or not.</param>
        /// <param name="isMessageSupressed">The value that sepecifies whether the related Feeback.Message should be supressed or not.</param>
        /// <returns>A SetDataOperationResult object containing the result and details specifying whether the data operation was a success or not.</returns>
        public Task<SetDataOperationResult> TrySetAsync(Action method, string successText, string errorText, bool isMessagePermanent, bool isMessageSupressed)
        {
            return UiThreadManager.RunAsynchronously(() => TrySet(method, successText, errorText, isMessagePermanent, isMessageSupressed));
        }
    }
}