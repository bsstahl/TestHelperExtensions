using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to all IEnumerables of numeric data types
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Returns a random item from the objects in the collection
        /// </summary>
        /// <typeparam name="T">Any collectable type</typeparam>
        /// <param name="values">A list of items from which a random one is to be selected.</param>
        /// <returns>A random object from within the list of values</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "values")]
        public static T GetRandom<T>(this IEnumerable<T> values)
        {
            T result;
            int count = values.Count();
            if (count < 2)
                result = values.FirstOrDefault();
            else
            {
                var i = count.GetRandom();
                result = values.Skip(i).First();
            }

            return result;
        }

        /// <summary>
        /// Calculates the population standard deviation of a list of at least 2 values.
        /// </summary>
        /// <typeparam name="T">Any numeric data type.</typeparam>
        /// <param name="values">A list of numeric values for which the standard deviation is to be determined.</param>
        /// <returns>A double containing the population standard deviation of the list of values.</returns>
        public static double StdDev<T>(this IEnumerable<T> values)
        {
            int count = values.Count();
            if (count < 2)
                throw new InvalidOperationException("Cannot determine the deviation of a list of fewer than 2 items.");

            var avg = values.Average<T>(v => Convert.ToDouble(v, CultureInfo.CurrentCulture));
            var sum = values.Sum<T>(d => (Convert.ToDouble(d, CultureInfo.CurrentCulture) - avg) * (Convert.ToDouble(d, CultureInfo.CurrentCulture) - avg));
            return Math.Sqrt(sum / count);
        }

        /// <summary>
        /// Calculates the median of a list of at least 2 values.
        /// </summary>
        /// <typeparam name="T">Any numeric data type.</typeparam>
        /// <param name="values">A list of numeric values for which the median is to be determined.</param>
        /// <returns>A double containing the median value of the list of values.</returns>
        /// <remarks>The return type of this method has to be double rather than T because
        /// the median has to be interpolated if the list contains an even number of values.</remarks>
        public static double Median<T>(this IEnumerable<T> values)
        {
            int count = values.Count();
            if (count < 2)
                throw new InvalidOperationException("Cannot determine the median of a list of fewer than 2 items.");

            var sortedValues = values.OrderBy(v => v).ToArray();
            double result;
            if (count % 2 == 0)
            {
                // even # of values
                int upperValue = Convert.ToInt32(count / 2);
                result = ((Convert.ToDouble(sortedValues[upperValue], CultureInfo.CurrentCulture) + Convert.ToDouble(sortedValues[upperValue - 1], CultureInfo.CurrentCulture)) / 2);
            }
            else
            {
                // Odd # of values
                int middleValue = Convert.ToInt32(Math.Floor(Convert.ToDouble(count) / 2));
                result = Convert.ToDouble(sortedValues[middleValue], CultureInfo.CurrentCulture);
            }

            return result;
        }


        /// <summary>
        /// Calculates the range of values of a list of at least 2 values.
        /// </summary>
        /// <typeparam name="T">Any numeric data type.</typeparam>
        /// <param name="values">A list of numeric values for which the range is to be determined.</param>
        /// <returns>A double containing the total range of the list of values.</returns>
        /// <remarks>The return type of this method has to be double rather than T because
        /// the range may cross the maximum positive value for the specified data type. For example,
        /// a list of Integers, that goes from -Int32.MaxValue to Int32.MaxValue would have a range
        /// of 2*Int32.MaxValue, which is clearly greater then the maximum allowable value of an Int32.</remarks>
        public static double Range<T>(this IEnumerable<T> values)
        {
            int count = values.Count();
            if (count < 2)
                throw new InvalidOperationException("Cannot determine the deviation of a list of fewer than 2 items.");

            var min = Convert.ToDouble(values.Min(), CultureInfo.CurrentCulture);
            var max = Convert.ToDouble(values.Max(), CultureInfo.CurrentCulture);
            return max - min;
        }


    }
}
