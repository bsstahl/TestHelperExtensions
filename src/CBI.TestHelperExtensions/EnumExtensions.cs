using System;
using System.Linq;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to Enum types
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns a random legal value of the enumeration
        /// </summary>
        /// <typeparam name="T">An Enum type</typeparam>
        /// <param name="value">Any value of the enumeration type (used only to identify the type)</param>
        /// <returns>A random legal value of the enumeration</returns>
        public static T GetRandom<T>(this T value) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
            return values[values.Length.GetRandom()];
        }
    }
}
