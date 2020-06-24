namespace CompanyName.ApplicationName.Extensions
{
    /// <summary>
    /// Provides a variety of commonly used methods that extend the functionality of the int struct.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Makes the wordToAdjust parameter plural if the int value does not equal 1 and combines it with the value in a string with a format of 'value wordToAdjust(s)'.
        /// </summary>
        /// <param name="input">The this int.</param>
        /// <param name="wordToAdjust">The word to pluralise and combine with the int value.</param>
        /// <returns>The int value followed by the plural of the wordToAdjust parameter if the int value does not equal 1 and the unadjusted wordToAdjust parameter if not.</returns>
        public static string Combine(this int input, string wordToAdjust)
        {
            return $"{input} {wordToAdjust}{(input == 1 ? string.Empty : "s")}";
        }

        /// <summary>
        /// Appends a letter 's' to the wordToAdjust input parameter if the int value does not equal 1.
        /// </summary>
        /// <param name="input">The this int.</param>
        /// <param name="wordToAdjust">The word to pluralize if the int value does not equal 1.</param>
        /// <returns>The wordToAdjust input parameter followed by a letter 's' if the int value does not equal 1, or otherwise wordToAdjust is returned.</returns>
        public static string Pluralize(this int input, string wordToAdjust)
        {
            return $"{wordToAdjust}{(input == 1 ? string.Empty : "s")}";
        }
    }
}