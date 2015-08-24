using System;
using System.Diagnostics;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the boolean data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class BoolExtensions
    {
        internal static Random _rnd = new Random();

        /// <summary>
        /// Returns a random boolean value (true or false)
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>A boolean containing a random value.</returns>
        public static bool GetRandom(this bool ignored)
        {
            double value = _rnd.NextDouble();
            Debug.Assert(value < 1.0);
            Debug.Assert(value >= 0);

            double roundedValue = Math.Round(value);
            byte intResult = Convert.ToByte(roundedValue);
            return (intResult == 1);
        }
    }
}
