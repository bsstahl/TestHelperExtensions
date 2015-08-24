using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    public static class LongExtensions
    {
        internal static Random _rnd = new Random();

        /// <summary>
        /// Returns a random number greater than or equal to 0 and less than the integer
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for the random number</param>
        /// <returns>A random integer</returns>
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
        public static long GetRandom(this long maxValue, long minValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");

            double percentage = _rnd.NextDouble();
            double difference = maxValue - minValue;
            return Convert.ToInt64(System.Math.Round(percentage * difference)) + minValue;
        }
    }
}
