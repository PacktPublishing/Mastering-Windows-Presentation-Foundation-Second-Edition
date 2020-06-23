using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ApplicationName.Extensions
{
    /// <summary>
    /// Provides a variety of commonly used methods that extend the functionality of the System.Collections.Generic.IEnumerable class.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Provides a shorthand lambda notation to execute an action on each item in the collection.
        /// </summary>
        /// <typeparam name="T">The System.Type of the objects in the collection.</typeparam>
        /// <param name="collection">The this object.</param>
        /// <param name="action">The generic Action&lt;T&gt; instance that represents the action to perform on each item in the collection.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection) action(item);
        }

        /// <summary>
        /// Converts the contents of the collection into a comma separated string by calling the Object.ToString() method on each item.
        /// </summary>
        /// <param name="collection">The this object.</param>
        /// <returns>A string with all occurences of the collection in a comma separated list.</returns>
        public static string ToCommaSeparatedString<T>(this IEnumerable<T> collection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 0;
            foreach (T item in collection)
            {
                if (index > 0)
                {
                    if (index < collection.Count() - 1) stringBuilder.Append(", ");
                    else if (index == collection.Count() - 1) stringBuilder.Append(" and ");
                }
                stringBuilder.Append(item.ToString());
                index++;
            }
            return stringBuilder.ToString();
        }
    }
}