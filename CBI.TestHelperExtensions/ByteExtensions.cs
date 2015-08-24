using System;

namespace TestHelperExtensions
{
    public static class ByteExtensions
    {
        internal static Random _rnd = new Random();

        /// <summary>
        /// Returns a random number greater than or equal to 0 and less 
        /// than the specified maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for 
        /// the random number</param>
        /// <returns>A random byte</returns>
        /// <remarks>255 is not a reachable value here since the maximum value
        /// of a byte is 255 and the specified maxValue is non-inclusive.</remarks>
        public static byte GetRandom(this byte maxValue)
        {
            return maxValue.GetRandom(Convert.ToByte(0));
        }

        /// <summary>
        /// Returns a random number greater than or equal to the 
        /// specified minValue and less than the specified maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum integer value for the random number</param>
        /// <param name="minValue">the inclusive minimum integer value for the random number</param>
        /// <returns>A random byte</returns>
        /// <remarks>255 is not a reachable value here since the maximum value
        /// of a byte is 255 and the specified maxValue is non-inclusive.</remarks>
        public static byte GetRandom(this byte maxValue, byte minValue)
        {
            return Convert.ToByte(_rnd.Next(minValue, maxValue));
        }

    }
}
