using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelperExtensions.Test
{
    public static class Extensions
    {
        public static IEnumerable<int> GetRandomByteValues(this int valueCount)
        {
            return valueCount.GetRandomByteValues(byte.MaxValue);
        }

        public static IEnumerable<int> GetRandomByteValues(this int valueCount, byte maxValue)
        {
            return valueCount.GetRandomByteValues(maxValue, 0);
        }

        public static IEnumerable<int> GetRandomByteValues(this int valueCount, byte maxValue, byte minValue)
        {
            int[] result = new int[valueCount];
            for (int i = 0; i < valueCount; i++)
                result[i] = maxValue.GetRandom(minValue);
            return result;
        }

        public static IEnumerable<int> GetRandomIntegerValues(this int valueCount)
        {
            return valueCount.GetRandomIntegerValues(Int32.MaxValue);
        }

        public static IEnumerable<int> GetRandomIntegerValues(this int valueCount, int maxValue)
        {
            int minValue = 0;
            return valueCount.GetRandomIntegerValues(maxValue, minValue);
        }

        public static IEnumerable<int> GetRandomIntegerValues(this int valueCount, int maxValue, int minValue)
        {
            int[] result = new int[valueCount];
            for (int i = 0; i < valueCount; i++)
                result[i] = maxValue.GetRandom(minValue);
            return result;
        }

        public static IEnumerable<short> GetRandomShortValues(this int valueCount)
        {
            return valueCount.GetRandomShortValues(Int16.MaxValue);
        }

        public static IEnumerable<short> GetRandomShortValues(this int valueCount, Int16 maxValue)
        {
            short minValue = 0;
            return valueCount.GetRandomShortValues(maxValue, minValue);
        }

        public static IEnumerable<short> GetRandomShortValues(this int valueCount, short maxValue, short minValue)
        {
            short[] result = new short[valueCount];
            for (int i = 0; i < valueCount; i++)
                result[i] = maxValue.GetRandom(minValue);
            return result;
        }

        public static IEnumerable<long> GetRandomLongValues(this int valueCount)
        {
            return valueCount.GetRandomLongValues(long.MaxValue);
        }

        public static IEnumerable<long> GetRandomLongValues(this int valueCount, long maxValue)
        {
            long minValue = 0;
            return valueCount.GetRandomLongValues(maxValue, minValue);
        }

        public static IEnumerable<long> GetRandomLongValues(this int valueCount, long maxValue, long minValue)
        {
            long[] result = new long[valueCount];
            for (int i = 0; i < valueCount; i++)
                result[i] = maxValue.GetRandom(minValue);
            return result;
        }

        public static IEnumerable<DateTime> GetRandomDateTimeValues(this int valueCount)
        {
            return valueCount.GetRandomDateTimeValues(DateTime.MaxValue);
        }

        public static IEnumerable<DateTime> GetRandomDateTimeValues(this int valueCount, DateTime maxValue)
        {
            return valueCount.GetRandomDateTimeValues(maxValue, DateTime.MinValue);
        }

        public static IEnumerable<DateTime> GetRandomDateTimeValues(this int valueCount, DateTime maxValue, DateTime minValue)
        {
            DateTime[] result = new DateTime[valueCount];
            for (int i = 0; i < valueCount; i++)
                result[i] = maxValue.GetRandom(minValue);
            return result;
        }

        public static IEnumerable<Double> GetRandomDoubleValues(this int valueCount, Double maxValue, Double minValue)
        {
            double[] result = new double[valueCount];
            for (int i = 0; i < valueCount; i++)
                result[i] = maxValue.GetRandom(minValue);
            return result;
        }

        public static int? FirstIndexOf<T>(this IEnumerable<T> values, T searchItem)
        {
            int? result = null;
            int index = 0;
            bool found = false;
            int count = values.Count();
            var searchList = values.ToArray();
            while ((index < count) && (!found))
            {
                if (searchItem.Equals(searchList.ElementAt(index)))
                {
                    found = true;
                    result = index;
                }
                index++;
            }
            return result;
        }

        public static long[] GetValuesDistribution<T>(this IEnumerable<T> values)
        {
            var minValue = Convert.ToInt32(Math.Floor(Convert.ToDouble(values.Min())));
            var maxValue = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(values.Max())));
            var range = maxValue - minValue + 1;

            long[] result = new long[range + 1];
            foreach (var randomValue in values)
            {
                var thisValue = Convert.ToInt32(Math.Round(Convert.ToDouble(randomValue)));
                result[thisValue - minValue]++;
            }

            return result;
        }

    }
}
