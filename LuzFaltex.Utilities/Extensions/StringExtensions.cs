using System;
using System.Collections.Generic;
using System.Text;

namespace LuzFaltex.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns whether the string contains the specified substring, accepting a comparison parameter.
        /// </summary>
        /// <param name="toCheck">The substring</param>
        /// <param name="comparison">Comparison options</param>
        /// <returns>True if a match was found; otherwise false.</returns>
        public static bool Contains(this string source, string toCheck, StringComparison comparison)
            => source?.IndexOf(toCheck, comparison) >= 0;

        /// <summary>
        /// Shortens the string to be no longer than the specified <paramref name="length"/>
        /// </summary>
        /// <param name="length">The maximum length of the string</param>
        /// <returns>A truncated string of specified <paramref name="length"/>, or if original string is shorter, then the original string.</returns>
        public static string TruncateTo(this string source, int length)
        {
            if (source.Length <= length)
                return source;

            return source.Substring(0, length);
        }

        /// <summary>
        /// Adds padding to the end of the string to ensure it is at least as long as the provided <paramref name="length"/>.
        /// </summary>
        /// <param name="length">The length to expand the string size to.</param>
        /// <returns>A padded string of specified <paramref name="length"/>, or if the original string was equal to or longer, the original string. </returns>
        public static string ExpandTo(this string source, int length)
        {
            if (source.Length <= length)
            {
                int difference = length - source.Length;
                source += new string(' ', difference);
            }

            return source;
        }

        /// <summary>
        /// Takes the specified number of characters starting at the left side of the string.
        /// </summary>
        /// <param name="length">The number of characters to take</param>
        /// <returns>A substring starting at position 0 and ending at the specified position.</returns>
        public static string Left(this string source, int length)
            => source.Substring(0, length);

        /// <summary>
        /// Takes the specified number of characters from the end of the string
        /// </summary>
        /// <param name="length">The number of characters to take</param>
        /// <returns>A substring starting at the specified number of characters from the right and ending at the end of the string.</returns>
        public static string Right(this string source, int length)
            => source.Substring(source.Length - length);
    }
}
