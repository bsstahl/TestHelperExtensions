using System;
using System.Linq;
using Xunit;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{

    public class DoubleExtensions_GetRandom_Should
    {

        #region Rules Tests

        // Rules tests are the preferred types of unit tests since they
        // test those things that the customers care about. However, they
        // can sometimes be incomplete, or extremely difficult to make 
        // comprehensive.  In this example, we can easily check the
        // boundary rules, but would have a very difficult time proving
        // that we actually called the random number generator properly.
        // For example, if we were off by one in our calls to the generator
        // such that we never reached our bounds, but were always
        // at least 1 away, these tests might not identify that situation.

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            const int executionCount = 10000;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(random.Next(Int32.MaxValue))) - random.NextDouble();
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                Console.WriteLine("Actual={0}", actual);
                Assert.True(actual >= lowerBound);
            }
        }

        [Fact]
        public void NotReachTheUpperBound()
        {
            const int executionCount = 10000;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - Convert.ToDouble(random.Next(Int32.MaxValue)) - random.NextDouble();
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                Console.WriteLine("Actual={0}", actual);
                Assert.True(actual < upperBound);
            }
        }

        [Fact]
        public void NotReachTheUpperBoundIfAValueGreaterThanALongIsSpecified()
        {
            const int executionCount = 10000;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int64.MaxValue) + Convert.ToDouble(random.Next(Int16.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - Convert.ToDouble(random.Next(Int32.MaxValue)) - random.NextDouble();
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                Console.WriteLine("Actual={0}", actual);
                Assert.True(actual < upperBound);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            Console.WriteLine("UpperBound={0}", upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom();
                Console.WriteLine("Actual={0}", actual);
                Assert.True(actual >= 0);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var random = new Random();
            double upperBound = Convert.ToDouble(Int64.MaxValue) + Convert.ToDouble(random.Next(Int16.MaxValue) + random.NextDouble());
            double lowerBound = upperBound + Convert.ToDouble(random.Next(Int32.MaxValue)) - random.NextDouble();
            Assert.Throws<ArgumentOutOfRangeException>(() => upperBound.GetRandom(lowerBound));
        }

        [Fact]
        public void NotFailIfTheLowerBoundIsCloseToTheUpperBoundButStillLower()
        {
            double upperBound = 2.5;
            double lowerBound = 0.5;
            var result = upperBound.GetRandom(lowerBound);
        }

        [Fact]
        public void SpanTheFullRangeOfValuesIfTheRangeIsLessThanOne()
        {
            const int executionCount = 10000;

            double upperBound = 2.9;
            double lowerBound = 2.1;

            double minValue = upperBound;
            double maxValue = lowerBound;

            for (int i = 0; i < executionCount; i++)
            {
                var result = upperBound.GetRandom(lowerBound);
                if (result < minValue)
                    minValue = result;
                if (result > maxValue)
                    maxValue = result;
            }

            Assert.Equal(Math.Round(lowerBound, 2), Math.Round(minValue, 2));
            Assert.Equal(Math.Round(upperBound, 2), Math.Round(maxValue, 2));
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const double tolerance = .02;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int16.MaxValue) + Convert.ToDouble(random.Next(Int16.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(random.Next(Int16.MaxValue))) - random.NextDouble();

            var expectedMean = Convert.ToDouble((upperBound - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToInt64(expectedMean * tolerance);
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            var result = 100000.GetRandomDoubleValues(upperBound, lowerBound);
            var actualMean = result.Average();

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", actualMean, minMean, maxMean, lowerBound, upperBound);
            Assert.True(actualMean > minMean);
            Assert.True(actualMean < maxMean);
        }

        [Fact]
        public void HaveAMedianResultNearTheMiddleOfTheRange()
        {
            const double tolerance = .02;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int16.MaxValue) + Convert.ToDouble(random.Next(Int16.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(random.Next(Int16.MaxValue))) - random.NextDouble();

            var expectedMedian = Convert.ToDouble((upperBound - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToDouble(expectedMedian * tolerance);
            var minMedian = expectedMedian - Math.Abs(slop);
            var maxMedian = expectedMedian + Math.Abs(slop);

            var result = 100000.GetRandomDoubleValues(upperBound, lowerBound);
            var actualMedian = result.Median();

            Console.WriteLine("median:{0} min allowed:{1} max allowed:{2}", actualMedian, minMedian, maxMedian);
            Assert.True(actualMedian > minMedian);
            Assert.True(actualMedian < maxMedian);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .02;
            var random = new Random();

            double upperBound = Convert.ToDouble(Int16.MaxValue) + Convert.ToDouble(random.Next(Int16.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(random.Next(Int16.MaxValue))) - random.NextDouble();

            double expectedRange = upperBound - lowerBound;
            var slop = Convert.ToDouble(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;


            var result = 100000.GetRandomDoubleValues(upperBound, lowerBound);
            var actualRange = result.Range();

            Console.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.True(actualRange > minRange);
            Assert.True(actualRange < maxRange);

        }

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRangeForASmallRange()
        {
            const double tolerance = .02;
            var random = new Random();

            double upperBound = random.NextDouble() * Convert.ToDouble(random.Next(byte.MaxValue));
            double lowerBound = upperBound - random.NextDouble();

            var expectedMean = Convert.ToDouble((upperBound - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToInt64(expectedMean * tolerance);
            var minMean = Math.Round(expectedMean - slop, 2);
            var maxMean = Math.Round(expectedMean + slop, 2);

            var result = 100000.GetRandomDoubleValues(upperBound, lowerBound);
            var actualMean = Math.Round(result.Average(), 2);

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", actualMean, minMean, maxMean, lowerBound, upperBound);
            Assert.True(actualMean >= minMean);
            Assert.True(actualMean <= maxMean);
        }

        [Fact]
        public void HaveAMedianResultNearTheMiddleOfTheRangeForASmallRange()
        {
            const double tolerance = .001;
            var random = new Random();

            double upperBound = random.NextDouble() * Convert.ToDouble(random.Next(byte.MaxValue));
            double lowerBound = upperBound - random.NextDouble();

            var expectedMedian = Convert.ToDouble((upperBound - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToDouble(expectedMedian * tolerance);
            var minMedian = expectedMedian - Math.Abs(slop);
            var maxMedian = expectedMedian + Math.Abs(slop);

            var result = 100000.GetRandomDoubleValues(upperBound, lowerBound);
            var actualMedian = result.Median();

            Console.WriteLine("median:{0} min allowed:{1} max allowed:{2}", actualMedian, minMedian, maxMedian);
            Assert.True(actualMedian > minMedian);
            Assert.True(actualMedian < maxMedian);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequestForASmallRange()
        {
            const double tolerance = .001;
            var random = new Random();

            double upperBound = random.NextDouble() * Convert.ToDouble(random.Next(byte.MaxValue));
            double lowerBound = upperBound - random.NextDouble();

            double expectedRange = upperBound - lowerBound;
            var slop = Convert.ToDouble(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;

            var result = 100000.GetRandomDoubleValues(upperBound, lowerBound);
            var actualRange = result.Range();

            Console.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.True(actualRange > minRange);
            Assert.True(actualRange < maxRange);

        }

        #endregion

    }
}
