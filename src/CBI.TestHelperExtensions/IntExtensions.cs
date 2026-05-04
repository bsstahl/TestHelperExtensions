using System;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the Integer (Int32) data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class IntExtensions
    {
        /// <summary>
        /// Returns a random number greater than or equal to 0 and less than the integer
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for the random number</param>
        /// <returns>A random integer</returns>
        /// <example>
        /// <code>
        /// // Get a random integer between 0 (inclusive) and 100 (exclusive)
        /// int result = 100.GetRandom();
        /// </code>
        /// </example>
        public static int GetRandom(this int maxValue)
        {
            return maxValue.GetRandom(0);
        }

        /// <summary>
        /// Returns a random number greater than or equal to the specified minValue and less than the integer
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for the random number</param>
        /// <param name="minValue">the inclusive minimum integer value for the random number</param>
        /// <returns>A random integer</returns>
        /// <example>
        /// <code>
        /// // Get a random integer between 10 (inclusive) and 50 (exclusive)
        /// int result = 50.GetRandom(10);
        /// </code>
        /// </example>
        public static int GetRandom(this int maxValue, int minValue)
        {
            return RandomNumberGenerator.Create().Next(minValue, maxValue);
        }

    }
}
