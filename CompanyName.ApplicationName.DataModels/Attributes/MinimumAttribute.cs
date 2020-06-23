using System.ComponentModel.DataAnnotations;

namespace CompanyName.ApplicationName.DataModels.Attributes
{
    /// <summary>
    /// Validates that the input value is more than a specified minimum value.
    /// </summary>
    public class MinimumAttribute : ValidationAttribute
    {
        private double minimumValue = 0.0;

        /// <summary>
        /// Initializes a new MinimumAttribute object with the value from the input parameter.
        /// </summary>
        /// <param name="minimumValue">The minimum value to compare with the input value.</param>
        public MinimumAttribute(double minimumValue)
        {
            this.minimumValue = minimumValue;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the System.ComponentModel.DataAnnotations.ValidationResult class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.GetType() != typeof(decimal) || (decimal)value < (decimal)minimumValue)
            {
                string[] memberNames = new string[] { validationContext.MemberName };
                return new ValidationResult(ErrorMessage, memberNames);
            }
            return ValidationResult.Success;
        }
    }
}