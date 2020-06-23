using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CompanyName.ApplicationName.Extensions
{
    /// <summary>
    /// Provides a variety of commonly used methods that extend the functionality of the .NET Framework classes.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Adds the items from the collection specified by the range input parameter into this collection.
        /// </summary>
        /// <typeparam name="T">The type of items inside the collections.</typeparam>
        /// <param name="collection">This collection.</param>
        /// <param name="range">The collection containing the items to add to this collection.</param>
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> range)
        {
            foreach (T item in range) collection.Add(item);
        }

        /// <summary>
        /// Adds the text as an item into the collection if it does not already exist in it.
        /// </summary>
        /// <param name="collection">The this collection.</param>
        /// <param name="text">The text to add as an item if it is unique.</param>
        public static void AddUniqueIfNotEmpty(this ObservableCollection<string> collection, string text)
        {
            if (!string.IsNullOrEmpty(text) && !collection.Contains(text)) collection.Add(text);
        }

        /// <summary>
        /// Appends the text input parameter to the end of the StringBuilder text if it does not already contain it.
        /// </summary>
        /// <param name="stringBuilder">The this StringBuilder.</param>
        /// <param name="text">The text to append to the StringBuilder object if it is unique.</param>
        public static void AppendUniqueOnNewLineIfNotEmpty(this StringBuilder stringBuilder, string text)
        {
            if (text.Trim().Length > 0 && !stringBuilder.ToString().Contains(text)) stringBuilder.AppendFormat("{0}{1}", stringBuilder.ToString().Trim().Length == 0 ? string.Empty : Environment.NewLine, text);
        }

        /// <summary>
        /// Returns the number of items in the collection specified by the collection input parameter.
        /// </summary>
        /// <param name="collection">The collection to return the number items of.</param>
        /// <returns>The number of items in the collection specified by the collection input parameter.</returns>
        public static int Count(this IEnumerable collection)
        {
            int count = 0;
            foreach (object item in collection) count++;
            return count;
        }

        /// <summary>
        /// Removes the items from the collection specified by the range input parameter from this collection.
        /// </summary>
        /// <typeparam name="T">The type of items inside the collections.</typeparam>
        /// <param name="collection">This collection.</param>
        /// <param name="range">The collection containing the items to remove from this collection.</param>
        public static void Remove<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Remove(range.ElementAt(index));
        }

        #region LINQ Extentions

        /// <summary>
        /// Returns elements with distinct property values from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source collection.</typeparam>
        /// <typeparam name="TKey">The type of the property key that will be used to find distinct values for.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">The property key that will be used to find distinct values for.</param>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains distinct elements from the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the source collection is null.</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> keys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (keys.Add(keySelector(element))) yield return element;
            }
        }

        #endregion
    }
}