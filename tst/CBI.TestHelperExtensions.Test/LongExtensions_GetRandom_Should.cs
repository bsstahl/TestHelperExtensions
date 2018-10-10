using System;
using System.Linq;
using Xunit;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{

    public class LongExtensions_GetRandom_Should
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

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(Int32.MaxValue));
            long lowerBound = upperBound - Convert.ToInt64(random.Next(Int32.MaxValue));
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

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(Int32.MaxValue));
            long lowerBound = upperBound - Convert.ToInt64(random.Next(Int32.MaxValue));
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

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(Int32.MaxValue));
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
            long upperBound = Int32.MaxValue + random.Next(100, 1000);
            long lowerBound = upperBound + random.Next(100);
            Assert.Throws<ArgumentOutOfRangeException>(() => upperBound.GetRandom(lowerBound));
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const int valueRadius = 2000;
            const double tolerance = .02;
            var random = new Random();

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(valueRadius));
            long lowerBound = Convert.ToInt64(Int32.MaxValue) - Convert.ToInt64(random.Next(valueRadius));

            var expectedMean = Convert.ToInt64(((upperBound - 1) - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToInt64(expectedMean * tolerance);
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            var result = 100000.GetRandomLongValues(upperBound, lowerBound);
            var actualMean = result.Average();

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", actualMean, minMean, maxMean, lowerBound, upperBound);
            Assert.True(actualMean > minMean);
            Assert.True(actualMean < maxMean);
        }

        [Fact]
        public void HaveAMedianResultNearTheMiddleOfTheRange()
        {
            const int valueRadius = 2000;
            const double tolerance = .02;
            var random = new Random();

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(valueRadius));
            long lowerBound = Convert.ToInt64(Int32.MaxValue) - Convert.ToInt64(random.Next(valueRadius));

            var expectedMedian = Convert.ToInt64(((upperBound - 1) - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToInt64(expectedMedian * tolerance);
            var minMedian = expectedMedian - slop;
            var maxMedian = expectedMedian + slop;

            var result = 100000.GetRandomLongValues(upperBound, lowerBound);
            var actualMedian = result.Median();

            Console.WriteLine("median:{0} min allowed:{1} max allowed:{2}", actualMedian, minMedian, maxMedian);
            Assert.True(actualMedian > minMedian);
            Assert.True(actualMedian < maxMedian);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const int valueRadius = 2000;
            const double tolerance = .02;
            var random = new Random();

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(valueRadius));
            long lowerBound = Convert.ToInt64(Int32.MaxValue) - Convert.ToInt64(random.Next(valueRadius));

            double expectedRange = upperBound - lowerBound;
            var slop = Convert.ToByte(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;


            var result = 100000.GetRandomLongValues(upperBound, lowerBound);
            var actualRange = result.Range();

            Console.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.True(actualRange > minRange);
            Assert.True(actualRange < maxRange);

        }

        #endregion

    }
}
