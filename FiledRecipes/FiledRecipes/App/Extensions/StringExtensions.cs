using System;
using System.Collections.Generic;

namespace FiledRecipes
{
    /// <summary>
    /// Extension methods of the String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///  Center aligns a string within this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="other">The string to center align.</param>
        /// <returns>A string that contains the center aligned value of the parameter other. </returns>
        public static string CenterAlignString(this string s, string other)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (s.Length > other.Length)
            {
                throw new InvalidOperationException();
            }

            int halfSpace = (other.Length - s.Length) / 2;

            return s.PadLeft(halfSpace + s.Length).PadRight(other.Length);
        }

        /// <summary>
        /// Returns a collection of strings of the specified maximum length.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="maxLength">Maximum lenght of a splitted string.</param>
        /// <returns></returns>
        public static IEnumerable<string> SplitByLength(this string s, int maxLength)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            for (int index = 0; index < s.Length; index += maxLength)
            {
                yield return s.Substring(index, Math.Min(maxLength, s.Length - index));
            }
        }
    }
}
