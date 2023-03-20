using System;
using System.Collections.Generic;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the byte (Int8) data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class ByteExtensions
    {
        /// <summary>
        /// Returns a random number greater than or equal to 0 and less 
        /// than the specified maxValue
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum byte value for 
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
        /// <param name="maxValue">The non-inclusive maximum byte value for the random number</param>
        /// <param name="minValue">the inclusive minimum byte value for the random number</param>
        /// <returns>A random byte</returns>
        /// <remarks>255 is not a reachable value here since the maximum value
        /// of a byte is 255 and the specified maxValue is non-inclusive.</remarks>
        public static byte GetRandom(this byte maxValue, byte minValue)
        {
            return Convert.ToByte(RandomNumberGenerator.Create().Next(minValue, maxValue));
        }

        /// <summary>
        /// Returns an IEnumerable of the specified length containing byte 
        /// values greater than or equal to the specified minValue and 
        /// less then the specified maxValue. 0 is used for the minValue 
        /// if it is not specified. If the length is not specified, a random 
        /// length is used.
        /// </summary>
        /// <param name="maxValue">The non-inclusive maximum byte value for the random number</param>
        /// <param name="minValue">the inclusive minimum byte value for the random number</param>
        /// <param name="length">The length of the intended enumerable</param>
        /// <returns>A random IEnumerable of type byte</returns>
        public static IEnumerable<byte> GetRandomEnumerable(this byte maxValue, byte minValue = 0, int length = 0)
        {
            var result = new List<byte>();
            var len = length > 0 ? length : 300.GetRandom(8);
            for (var i = 0; i < len; i++)
                result.Add(Convert.ToByte(maxValue.GetRandom(minValue)));
            return result;
        }
    }
}
