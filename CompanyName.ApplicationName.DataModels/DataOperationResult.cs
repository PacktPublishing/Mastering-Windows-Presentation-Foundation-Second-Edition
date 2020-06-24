using System;
using System.Data.SqlClient;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents the result of a generic data operation and contains either the requested data object(s) or error details.
    /// </summary>
    /// <typeparam name="T">The type of element(s) involved in the data operation.</typeparam>
    public abstract class DataOperationResult<T>
    {
        /// <summary>
        /// Initializes a new DataOperationResult with the values provided by the input parameters.
        /// </summary>
        /// <param name="successText">The feedback text to display in the case of a successful data operation.</param>
        public DataOperationResult(string successText)
        {
            Description = string.IsNullOrEmpty(successText) ? "The data operation was successful" : successText;
        }

        /// <summary>
        /// Initializes a new DataOperationResult with the values provided by the input parameters in cases where an Exception occurred during the data operation.
        /// </summary>
        /// <param name="exception">The Exception that occurred during the data operation.</param>
        /// <param name="successText">The feedback text to display in the case of a successful data operation.</param>
        /// <param name="errorText">The feedback text to display in the case of an unsuccessful data operation.</param>
        public DataOperationResult(Exception exception, string errorText)
        {
            Exception = exception;
            if (Exception is SqlException)
            {
                if (exception.Message.Contains("The server was not found")) Error = DataOperationError.DatabaseConnectionError;
                else if (exception.Message.Contains("constraint")) Error = DataOperationError.DatabaseConstraintError;
                // else Description = Exception.Message;
            }
            if (Error != DataOperationError.None) Description = Error.GetDescription();
            else 
            {
                Error = DataOperationError.UndeterminedDataOperationError;
                Description = string.IsNullOrEmpty(errorText) ? Error.GetDescription() : errorText;
            }
        }

        /// <summary>
        /// Initializes a new DataOperationResult in cases where an Exception occurred during the data operation.
        /// </summary>
        /// <param name="exception">The Exception that occurred during the data operation.</param>
        public DataOperationResult(Exception exception) : this(exception, string.Empty) { }

        /// <summary>
        /// Gets or sets the error description of the data operation if one occurred or an empty string otherwise.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The DataOperationError enumeration that represents which error occurred during the data operation.
        /// </summary>
        public DataOperationError Error { get; set; } = DataOperationError.None;

        /// <summary>
        /// Gets or sets the Exception that occurred during the data operation, or null if no Exception occurred.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Gets or sets the value that specifies whether the data operation was successful, or not.
        /// </summary>
        public bool IsSuccess => Error == DataOperationError.None && Exception == null;
    }
}