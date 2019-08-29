using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TweakUtility.Helpers
{
    public static class Extensions
    {
        public static int ToBgrInt(this Color color) => (0 << 24) + (color.B << 16) + (color.G << 8) + color.R;

        public static Color ToBgrColor(this int bgrColor)
        {
            byte[] bytes = BitConverter.GetBytes(bgrColor);
            return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        }

        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            int retVal = 0;
            foreach (T item in items)
            {
                if (predicate(item))
                {
                    return retVal;
                }

                retVal++;
            }
            return -1;
        }

        ///<summary>Finds the index of the first occurrence of an item in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="item">The item to find.</param>
        ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf<T>(this IEnumerable<T> items, T item) => items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i));

        public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        {
            IEnumerable<T> enumarable = source;

            foreach (T item in source)
            {
                enumarable = enumarable.Concat(recursion.Invoke(item));
            }

            return enumarable;
        }

        /// <summary>
        /// Returns a readable color generated from the input <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The background color</param>
        /// <returns><see cref="true"/> for white, <see cref="false"/> for black</returns>
        /// <remarks>https://www.codeproject.com/Articles/16565/Determining-Ideal-Text-Color-Based-on-Specified-Ba</remarks>
        public static bool GetReadableColor(this Color color)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114));
            return !(255 - bgDelta < nThreshold);
        }
    }
}