using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the string data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class StringExtensions
    {
        const int _defaultLength = 8;

        /// <summary>
        /// Returns a random string of 8 characters
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random string value.</returns>
        public static string GetRandom(this string ignored)
        {
            return string.Empty.GetRandom(_defaultLength);
        }

        /// <summary>
        /// Returns a random string of the specified length
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <param name="length">The length of the desired random string.</param>
        /// <returns>The resulting random string value.</returns>
        public static string GetRandom(this string ignored, int length)
        {
            if (length < 1)
                throw new System.ArgumentException("length must be a positive integer", "length");

            string fullString = System.Guid.NewGuid().ToString();
            while (fullString.Length < length)
            {
                fullString += System.Guid.NewGuid().ToString();
            }

            return fullString.Substring(0, length);
        }

        /// <summary>
        /// Returns a random string in a format representing a US phone number
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random US phone number.</returns>
        public static string GetRandomUSPhoneNumber(this string ignored)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}-{1}-{2}", 999.GetRandom(211), 999.GetRandom(111), 9999.GetRandom(1111));
        }

        /// <summary>
        /// Returns a random string in a format representing an email address.
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random email address.</returns>
        public static string GetRandomEmailAddress(this string ignored)
        {
            string[] tlds = new string[] { "com", "net", "org", "int", "edu", "mil", "gov", "me", "ru", "au", "cz", "gb", "hk", "it", "jp" };
            string part1 = string.Empty.GetRandom(8.GetRandom(3));
            string domain = string.Empty.GetRandom(12.GetRandom(3));
            string tld = tlds[15.GetRandom()];
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}@{1}.{2}", part1, domain, tld);
        }

        /// <summary>
        /// Converts the specified string into a System.IO.Stream type.
        /// </summary>
        /// <param name="value">The string value to be converted into a Stream.</param>
        /// <returns>The resulting System.IO.Stream data type.</returns>
        /// <remarks>System.IO.Stream objects implement IDisposable and therefore must
        /// be disposed of once they are no longer needed to avoid memory leaks.</remarks>
        public static System.IO.Stream ToStream(this string value)
        {
            var buffer = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(buffer);
            writer.Write(value);
            writer.Flush();
            buffer.Position = 0;
            return buffer;
        }

        /// <summary>
        ///  Indicates whether the specified regular expression pattern 
        /// finds a match in the specified input string.
        /// </summary>
        /// <param name="value">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>Returns true if the regular expression finds a match, False if no match is found.</returns>
        public static bool RegexMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }



    }
}
