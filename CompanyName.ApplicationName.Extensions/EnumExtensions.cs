using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CompanyName.ApplicationName.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the content of the System.ComponentModel.DescriptionAttribute that relates to this enum if one is set, or the name of the enum otherwise.
        /// </summary>
        /// <param name="value">The this enum.</param>
        /// <returns>The content of the System.ComponentModel.DescriptionAttribute that relates to this enum if one is set, or the name of the enum otherwise.</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return Enum.GetName(value.GetType(), value);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            return Enum.GetName(value.GetType(), value);
        }

        /// <summary>
        /// Returns a generic ICollection containing an item for each member of the type of enum specified by the genric T type parameter. This method will throw an ArgumentException if the the provided genric T type parameter is not an enum.
        /// </summary>
        /// <typeparam name="T">The type of enum to fill the collection with.</typeparam>
        /// <param name="collection">The this ICollection.</param>
        /// <exception cref="ArgumentException" />
        public static void FillWithMembers<T>(this ICollection<T> collection)
        {
            if (typeof(T).BaseType != typeof(Enum)) throw new ArgumentException("The FillWithMembers<T> method can only be called with an enum as the generic type.");
            collection.Clear();
            foreach (string name in Enum.GetNames(typeof(T))) collection.Add((T)Enum.Parse(typeof(T), name));
        }
    }
}