using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to any Array of strings
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class StringArrayExtensions
    {
        /// <summary>
        /// Determines if any of the items in the specified data array match
        /// the specified key using the current culture's string comparison settings.
        /// </summary>
        /// <param name="dataArray">A list of string values.</param>
        /// <param name="key">The value being searched for in the list of strings.</param>
        /// <returns>A boolean indicating if the key value was found in the list of values.</returns>
        public static bool Contains(this string[] dataArray, string key)
        {
            return dataArray.Contains(key, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Determines if any of the items in the specified data array match
        /// the specified key using specified string comparison settings.
        /// </summary>
        /// <param name="dataArray">A list of string values.</param>
        /// <param name="key">The value being searched for in the list of strings.</param>
        /// <param name="comparisonType">Specifies the culture, case, and sort rules to be used during string comparison.</param>
        /// <returns>A boolean indicating if the key value was found in the list of values.</returns>
        public static bool Contains(this string[] dataArray, string key, StringComparison comparisonType)
        {
            bool result = false;
            if (dataArray != null)
            {
                foreach (string item in dataArray)
                {
                    if (item.Equals(key, comparisonType))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

    }
}
