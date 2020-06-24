using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents the result of a set data operation and returns true if successful, or false otherwise, along with error details.
    /// </summary>
    public class SetDataOperationResult : DataOperationResult<bool>
    {
        /// <summary>
        /// Initializes a new SetDataOperationResult with the values provided by the input parameters in cases where an Exception occurred during the data operation.
        /// </summary>
        /// <param name="exception">The Exception that occurred during the data operation.</param>
        /// <param name="successText">The feedback text to display in the case of a successful data operation.</param>
        /// <param name="errorText">The feedback text to display in the case of an unsuccessful data operation.</param>
        public SetDataOperationResult(Exception exception, string errorText) : base(exception, errorText) { }

        /// <summary>
        /// Initializes a new SetDataOperationResult with the values provided by the input parameters.
        /// </summary>
        /// <param name="successText">The feedback text to display in the case of a successful data operation.</param>
        /// <param name="errorText">The feedback text to display in the case of an unsuccessful data operation.</param>
        public SetDataOperationResult(string successText) : base(successText) { }
    }
}