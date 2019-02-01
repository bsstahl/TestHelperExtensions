using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the Double data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Returns a random number less than the double value
        /// and greater than or equal to 0
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum value for the random number</param>
        /// <returns>A random double less than the maxValue and greater than or equal to zero.</returns>
        public static double GetRandom(this double maxValue)
        {
            return maxValue.GetRandom(0);
        }

        /// <summary>
        /// Returns a random number greater than or equal to the specified minValue and less than the maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum value for the random number</param>
        /// <param name="minValue">The inclusive minimum value for the random number</param>
        /// <returns>A random double in the specified range</returns>
        public static double GetRandom(this double maxValue, double minValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), $"minValue {minValue} must be less than maxValue {maxValue}");

            var range = maxValue - minValue;
            return (RandomNumberGenerator.NextDouble() * range) + minValue;
        }

        /// <summary>
        /// Checks if the minValue and maxValue differ by more than
        /// the specified maximum range.
        /// </summary>
        /// <param name="maxRange">The greatest difference to allow between the two values</param>
        /// <param name="minValue">The lower bound of the range</param>
        /// <param name="maxValue">The upper bound of the range</param>
        /// <returns>True if the difference between the two values is greater than maxRange, False otherwise.</returns>
        public static bool IsWiderThanRange(this double maxRange, double minValue, double maxValue)
        {
            double range = Math.Abs(checked(maxValue - minValue));
            return (range > maxRange);
        }

    }
}
