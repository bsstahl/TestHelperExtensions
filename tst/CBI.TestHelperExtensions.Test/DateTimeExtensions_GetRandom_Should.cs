using System;
using Xunit;
using System.Linq;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{

    public class DateTimeExtensions_GetRandom_Should
    {
        // Note: Interaction tests are even less valuable here then
        // they are in some of the other contexts since this implementation
        // leverages the GetRandom methods for the Int64 data type. As a result
        // only rules type tests are supplied for the DateTime data type
        // random data features.

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

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));
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

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                Console.WriteLine("Actual={0}", actual);
                Assert.True(actual < upperBound);
            }
        }

        [Fact]
        public void AlwaysBeAboveTheMinimumDateTimeValueIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;
            var random = new Random();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            Console.WriteLine("UpperBound={0}", upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom();
                Console.WriteLine("Actual={0}", actual);
                Assert.True(actual >= DateTime.MinValue);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var random = new Random();
            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = upperBound.AddMinutes(25);
            Assert.Throws<ArgumentOutOfRangeException>(() => upperBound.GetRandom(lowerBound));
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const double tolerance = .001;
            const int executionCount = 100000;
            var random = new Random();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));

            var expectedMeanTicks = Convert.ToInt64(((upperBound.Ticks) - lowerBound.Ticks) / 2) + lowerBound.Ticks;
            var slopTicks = Convert.ToInt64(expectedMeanTicks * tolerance);
            var minMeanTicks = expectedMeanTicks - slopTicks;
            var maxMeanTicks = expectedMeanTicks + slopTicks;

            var result = executionCount.GetRandomDateTimeValues(upperBound, lowerBound);
            var actualMean = result.Average(d => Convert.ToDouble(d.Ticks));

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", new DateTime(Convert.ToInt64(Math.Round(actualMean))), new DateTime(minMeanTicks), new DateTime(maxMeanTicks), lowerBound, upperBound);
            Assert.True(actualMean > minMeanTicks);
            Assert.True(actualMean < maxMeanTicks);
        }

        [Fact]
        public void HaveAMedianResultNearTheMiddleOfTheRange()
        {
            const double tolerance = .001;
            const int executionCount = 100000;
            var random = new Random();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));

            var expectedMedianTicks = Convert.ToInt64(((upperBound.Ticks) - lowerBound.Ticks) / 2) + lowerBound.Ticks;
            var slopTicks = Convert.ToInt64(expectedMedianTicks * tolerance);
            var minMedianTicks = expectedMedianTicks - slopTicks;
            var maxMedianTicks = expectedMedianTicks + slopTicks;

            var result = executionCount.GetRandomDateTimeValues(upperBound, lowerBound);
            var actualMedian = result.Select(d => Convert.ToDouble(d.Ticks)).Median();

            Console.WriteLine("Median:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", new DateTime(Convert.ToInt64(Math.Round(actualMedian))), new DateTime(minMedianTicks), new DateTime(maxMedianTicks), lowerBound, upperBound);
            Assert.True(actualMedian > minMedianTicks);
            Assert.True(actualMedian < maxMedianTicks);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .001;
            const int executionCount = 100000;
            var random = new Random();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));

            double expectedRangeTicks = upperBound.Ticks - lowerBound.Ticks;
            var slop = Convert.ToDouble(expectedRangeTicks * tolerance);
            var minRangeTicks = Convert.ToInt64(Math.Round(expectedRangeTicks - slop));
            var maxRangeTicks = Convert.ToInt64(Math.Round(expectedRangeTicks + slop));

            var result = executionCount.GetRandomDateTimeValues(upperBound, lowerBound);
            var actualRange = result.Select(d => Convert.ToDouble(d.Ticks)).Range();
            var actualRangeInDays = (new TimeSpan(Convert.ToInt64(actualRange))).TotalDays;
            var minRangeInDays = (new TimeSpan(minRangeTicks)).TotalDays;
            var maxRangeInDays = (new TimeSpan(maxRangeTicks)).TotalDays;

            Console.WriteLine("Range:{0} days  min allowed:{1} days  max allowed:{2} days  lower bound:{3} upper bound:{4}", actualRangeInDays, minRangeInDays, maxRangeInDays, lowerBound, upperBound);
            Assert.True(actualRange > minRangeTicks);
            Assert.True(actualRange < maxRangeTicks);

        }

        #endregion

    }
}
