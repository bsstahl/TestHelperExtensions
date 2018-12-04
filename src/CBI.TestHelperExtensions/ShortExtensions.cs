using System;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the Short (Int16) data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class ShortExtensions
    {
        internal static Random _rnd = new Random();

        /// <summary>
        /// Returns a random number greater than or equal to 0 and less than the maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum short value for the random number</param>
        /// <returns>A random short</returns>
        public static Int16 GetRandom(this Int16 maxValue)
        {
            return maxValue.GetRandom(0);
        }

        /// <summary>
        /// Returns a random number greater than or equal to the specified minValue and less than the maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum short value for the random number</param>
        /// <param name="minValue">the inclusive minimum short value for the random number</param>
        /// <returns>A random short</returns>
        public static Int16 GetRandom(this Int16 maxValue, Int16 minValue)
        {
            return (Int16)_rnd.Next(minValue, maxValue);
        }

    }
}
