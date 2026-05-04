using System;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the Single (float) data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class SingleExtensions
    {
        /// <summary>
        /// Returns a random number less than the specified Single 
        /// value and greater than or equal to 0
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum value for the random number</param>
        /// <returns>A random Single less than the maxValue and greater than or equal to zero.</returns>
        /// <example>
        /// <code>
        /// // Get a random float between 0.0 (inclusive) and 5.0 (exclusive)
        /// float result = 5.0f.GetRandom();
        /// </code>
        /// </example>
        public static float GetRandom(this float maxValue)
            => maxValue.GetRandom(0.0f);

        /// <summary>
        /// Returns a random number greater than or equal to the 
        /// specified minValue and less than the maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum value for the random number</param>
        /// <param name="minValue">The inclusive minimum value for the random number</param>
        /// <returns>A random Single in the specified range</returns>
        /// <example>
        /// <code>
        /// // Get a random float between 1.0 (inclusive) and 10.0 (exclusive)
        /// float result = 10.0f.GetRandom(1.0f);
        /// </code>
        /// </example>
        public static float GetRandom(this float maxValue, float minValue)
        {
            return Convert
                .ToSingle(Convert.ToDouble(maxValue)
                    .GetRandom(minValue));
        }

        /// <summary>
        /// Checks if the minValue and maxValue differ by more than
        /// the specified maximum range.
        /// </summary>
        /// <param name="maxRange">The greatest difference to allow between the two values</param>
        /// <param name="minValue">The lower bound of the range</param>
        /// <param name="maxValue">The upper bound of the range</param>
        /// <returns>True if the difference between the two values is greater than maxRange, False otherwise.</returns>
        /// <example>
        /// <code>
        /// // Check whether the spread of values exceeds an allowed maximum range
        /// float allowedRange = 50.0f;
        /// float lowerBound = 10.0f;
        /// float upperBound = 40.0f;
        /// bool isTooWide = allowedRange.IsWiderThanRange(lowerBound, upperBound); // false
        /// </code>
        /// </example>
        public static bool IsWiderThanRange(this float maxRange, float minValue, float maxValue)
        {
            throw new NotImplementedException();
        }

    }
}