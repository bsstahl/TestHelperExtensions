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
        /// <returns>A random double</returns>
        public static double GetRandom(this double maxValue)
        {
            return maxValue.GetRandom(0);
        }

        /// <summary>
        /// Returns a random number greater than or equal to the specified minValue and less than the integer
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for the random number</param>
        /// <param name="minValue">the inclusive minimum integer value for the random number</param>
        /// <returns>A random integer</returns>
        public static double GetRandom(this double maxValue, double minValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");

            long maxBase;
            if (maxValue > Int64.MaxValue)
                maxBase = Int64.MaxValue;
            else
                maxBase = Convert.ToInt64(maxValue) - 1;
            long minBase = Convert.ToInt64(System.Math.Round(minValue + 0.5));

            double resultMantissa = _rnd.NextDouble();
            long resultBase = maxBase.GetRandom(minBase);

            return Convert.ToDouble(resultBase) + Convert.ToDouble(resultMantissa);
        }
    }
}
