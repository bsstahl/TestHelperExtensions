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

        /// <summary>
        /// The maximum value convertable from a double to Int64
        /// using the standard conversion tools. Anything higher
        /// throws an OverflowException.
        /// </summary>
        public const double RealMaxInt64 = 9223372036854775295.0;

        /// <summary>
        /// Returns a random number less than the double value
        /// and greater than or equal to the larger of 0 or a value
        /// roughly Int64.MaxValue less than the upper bound
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum value for the random number</param>
        /// <returns>A random double less than the maxValue and greater than zero.</returns>
        /// <remarks>The Int64.MaxValue range requirement is as a result of how
        /// random numbers are generated in .NET.</remarks>
        public static double GetRandom(this double maxValue)
        {
            return maxValue.GetRandom(Math.Max(0, maxValue - RealMaxInt64));
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

            if (Convert.ToDouble(Int64.MaxValue).IsWiderThanRange(minValue, maxValue))
                throw new OverflowException($"The range of values cannot be greater than {RealMaxInt64}. Currently {minValue} - {maxValue}, range = {maxValue - minValue}");

            var range = maxValue - minValue;
            double rangeFloor = System.Math.Floor(range);
            long rangeBase = Convert.ToInt64(rangeFloor);
            double rangeMantissa = range - rangeBase;

            double resultMantissa = RandomNumberGenerator.NextDouble() * rangeMantissa;
            long resultBase = (rangeBase > 0) ? rangeBase.GetRandom() : 0;

            return Convert.ToDouble(resultBase) + Convert.ToDouble(resultMantissa) + minValue;
        }

        /// <summary>
        /// Gets a random range of values that can be used in the GetRandom
        /// method for generating a random double value
        /// </summary>
        /// <param name="maxValue">The highest possible value for the upperBound of the range</param>
        /// <returns>Two doubles that are not separated by more than
        /// the value of Int64.MaxValue</returns>
        /// <remarks>The Int64.MaxValue limit on the range is due to 
        /// the way random numbers are generated in the GetRandom method</remarks>
        public static (double lowerBound, double upperBound) GetRandomRange(this double maxValue)
        {
            double minValue = maxValue - RealMaxInt64;
            return maxValue.GetRandomRange(minValue);
        }

        /// <summary>
        /// Gets a random range of values that can be used in the GetRandom
        /// method for generating a random double value
        /// </summary>
        /// <param name="maxValue">The highest possible value for the upperBound of the range</param>
        /// <param name="minValue">The lowest possible value for the lowerBound of the range</param>
        /// <returns>Two doubles that are not separated by more than
        /// the value of Int64.MaxValue</returns>
        /// <remarks>The Int64.MaxValue limit on the range is due to 
        /// the way random numbers are generated in the GetRandom method</remarks>
        public static (double lowerBound, double upperBound) GetRandomRange(this double maxValue, double minValue)
        {
            if (RealMaxInt64.IsWiderThanRange(minValue, maxValue))
                throw new OverflowException($"The range of values cannot be greater than {RealMaxInt64}. Currently {minValue} - {maxValue}, range = {maxValue - minValue}");
            
            double value1 = maxValue.GetRandom(minValue);
            double value2 = maxValue.GetRandom(minValue);
            return (Math.Min(value1, value2), Math.Max(value1, value2));
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
