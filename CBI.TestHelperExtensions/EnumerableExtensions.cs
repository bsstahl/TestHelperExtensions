using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelperExtensions
{
    public static class EnumerableExtensions
    {

        public static double StdDev<T>(this IEnumerable<T> values)
        {
            int count = values.Count();
            if (count < 2)
                throw new InvalidOperationException("Cannot determine the deviation of a list of fewer than 2 items.");

            var avg = values.Average<T>(v => Convert.ToDouble(v, CultureInfo.CurrentCulture));
            var sum = values.Sum<T>(d => (Convert.ToDouble(d, CultureInfo.CurrentCulture) - avg) * (Convert.ToDouble(d, CultureInfo.CurrentCulture) - avg));
            return Math.Sqrt(sum / count);
        }

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
