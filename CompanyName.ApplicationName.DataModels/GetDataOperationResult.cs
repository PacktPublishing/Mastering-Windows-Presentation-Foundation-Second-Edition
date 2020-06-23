using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents the result of a generic get data operation and contains either the requested data object(s) or error details.
    /// </summary>
    /// <typeparam name="T">The type of element(s) involved in the data operation.</typeparam>
    public class GetDataOperationResult<T> : DataOperationResult<T>
    {
        /// <summary>
        /// Initializes a new GetDataOperationResult with the values provided by the input parameters in cases where an Exception occurred during the data operation.
        /// </summary>
        /// <param name="exception">The Exception that occurred during the data operation.</param>
        /// <param name="successText">The feedback text to display in the case of a successful data operation.</param>
        /// <param name="errorText">The feedback text to display in the case of an unsuccessful data operation.</param>
        public GetDataOperationResult(Exception exception, string errorText) : base(exception, errorText)
        {
            ReturnValue = default(T);
        }

        /// <summary>
        /// Initializes a new GetDataOperationResult in cases where an Exception occurred during the data operation.
        /// </summary>
        /// <param name="exception">The Exception that occurred during the data operation.</param>
        public GetDataOperationResult(Exception exception) : this(exception, string.Empty) { }

        /// <summary>
        /// Initializes a new GetDataOperationResult with the values provided by the input parameters.
        /// </summary>
        /// <param name="returnValue">The generic result of a data operation.</param>
        /// <param name="successText">The feedback text to display in the case of a successful data operation.</param>
        /// <param name="errorText">The feedback text to display in the case of an unsuccessful data operation.</param>
        public GetDataOperationResult(T returnValue, string successText) : base(successText)
        {
            ReturnValue = returnValue;
        }

        /// <summary>
        /// Initializes a new GetDataOperationResult with the values provided by the input parameter.
        /// </summary>
        /// <param name="returnValue">The generic result of a data operation.</param>
        public GetDataOperationResult(T returnValue) : this(returnValue, string.Empty) { }
        
        /// <summary>
        /// Gets or sets the generic result of a data operation.
        /// </summary>
        public T ReturnValue { get; private set; }
    }
}