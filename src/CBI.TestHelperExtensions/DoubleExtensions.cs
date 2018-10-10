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
        const byte _defaultDecimalPlaces = 5;

        static Random _rnd = new Random();

        /// <summary>
        /// Returns a random number greater than or equal to 0 and less than the double value
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum value for the random number</param>
        /// <returns>A random double less than the maxValue and greater than zero.</returns>
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
            if (minValue >= maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue must be less than maxValue");

            double range = maxValue - minValue;

            if (range > Int64.MaxValue)
                throw new OverflowException("The range of values cannot be greater than Int64.MaxValue");

            long rangeBase = Convert.ToInt64(System.Math.Floor(range));
            double rangeMantissa = range - rangeBase;

            double resultMantissa = _rnd.NextDouble() * rangeMantissa;
            long resultBase = (rangeBase > 0) ? rangeBase.GetRandom() : 0;

            return Convert.ToDouble(resultBase) + Convert.ToDouble(resultMantissa) + minValue;
        }
    }
}
