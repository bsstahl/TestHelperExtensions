using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the Long (Int64) data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class LongExtensions
    {
        /// <summary>
        /// Returns a random number greater than or equal to 0 and less than the integer
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for the random number</param>
        /// <returns>A random integer</returns>
        /// <example>
        /// <code>
        /// // Get a random long between 0 (inclusive) and 1000000 (exclusive)
        /// long result = 1000000L.GetRandom();
        /// </code>
        /// </example>
        public static long GetRandom(this long maxValue)
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
        /// // Get a random long between 100 (inclusive) and 500 (exclusive)
        /// long result = 500L.GetRandom(100L);
        /// </code>
        /// </example>
        public static long GetRandom(this long maxValue, long minValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");

            double percentage = RandomNumberGenerator.Create().NextDouble();
            double difference = maxValue - minValue;
            return Convert.ToInt64(System.Math.Round(percentage * difference)) + minValue;
        }
    }
}
