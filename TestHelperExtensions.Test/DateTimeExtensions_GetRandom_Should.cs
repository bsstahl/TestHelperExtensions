using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_GetRandom_Should
    {
        public TestContext TestContext { get; set; }
        private Random _random = new Random();

        [TestCleanup]
        public void TestCleanup()
        {
            TestHelperExtensions.LongExtensions._rnd = new Random();
        }

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

        [TestMethod]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            const int executionCount = 10000;

            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-_random.Next(Int32.MaxValue));
            TestContext.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                TestContext.WriteLine("Actual={0}", actual);
                Assert.IsTrue(actual >= lowerBound);
            }
        }

        [TestMethod]
        public void NotReachTheUpperBound()
        {
            const int executionCount = 10000;

            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-_random.Next(Int32.MaxValue));
            TestContext.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                TestContext.WriteLine("Actual={0}", actual);
                Assert.IsTrue(actual < upperBound);
            }
        }

        [TestMethod]
        public void AlwaysBeAboveTheMinimumDateTimeValueIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;

            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            TestContext.WriteLine("UpperBound={0}", upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom();
                TestContext.WriteLine("Actual={0}", actual);
                Assert.IsTrue(actual >= DateTime.MinValue);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            var lowerBound = upperBound.AddMinutes(25);
            var result = upperBound.GetRandom(lowerBound);
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [TestMethod]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const double tolerance = .001;
            const int executionCount = 100000;

            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-_random.Next(Int32.MaxValue));

            var expectedMeanTicks = Convert.ToInt64(((upperBound.Ticks) - lowerBound.Ticks) / 2) + lowerBound.Ticks;
            var slopTicks = Convert.ToInt64(expectedMeanTicks * tolerance);
            var minMeanTicks = expectedMeanTicks - slopTicks;
            var maxMeanTicks = expectedMeanTicks + slopTicks;

            var result = executionCount.GetRandomDateTimeValues(upperBound, lowerBound);
            var actualMean = result.Average(d => Convert.ToDouble(d.Ticks));

            TestContext.WriteLine("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", new DateTime(Convert.ToInt64(Math.Round(actualMean))), new DateTime(minMeanTicks), new DateTime(maxMeanTicks), lowerBound, upperBound);
            Assert.IsTrue(actualMean > minMeanTicks);
            Assert.IsTrue(actualMean < maxMeanTicks);
        }

        [TestMethod]
        public void HaveAMedianResultNearTheMiddleOfTheRange()
        {
            const double tolerance = .001;
            const int executionCount = 100000;

            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-_random.Next(Int32.MaxValue));

            var expectedMedianTicks = Convert.ToInt64(((upperBound.Ticks) - lowerBound.Ticks) / 2) + lowerBound.Ticks;
            var slopTicks = Convert.ToInt64(expectedMedianTicks * tolerance);
            var minMedianTicks = expectedMedianTicks - slopTicks;
            var maxMedianTicks = expectedMedianTicks + slopTicks;

            var result = executionCount.GetRandomDateTimeValues(upperBound, lowerBound);
            var actualMedian = result.Select(d => Convert.ToDouble(d.Ticks)).Median();

            TestContext.WriteLine("Median:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", new DateTime(Convert.ToInt64(Math.Round(actualMedian))), new DateTime(minMedianTicks), new DateTime(maxMedianTicks), lowerBound, upperBound);
            Assert.IsTrue(actualMedian > minMedianTicks);
            Assert.IsTrue(actualMedian < maxMedianTicks);
        }

        [TestMethod]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .001;
            const int executionCount = 100000;

            var upperBound = DateTime.UtcNow.AddSeconds(_random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-_random.Next(Int32.MaxValue));

            double expectedRangeTicks = upperBound.Ticks - lowerBound.Ticks;
            var slop = Convert.ToDouble(expectedRangeTicks * tolerance);
            var minRangeTicks = Convert.ToInt64(Math.Round(expectedRangeTicks - slop));
            var maxRangeTicks = Convert.ToInt64(Math.Round(expectedRangeTicks + slop));

            var result = executionCount.GetRandomDateTimeValues(upperBound, lowerBound);
            var actualRange = result.Select(d => Convert.ToDouble(d.Ticks)).Range();
            var actualRangeInDays = (new TimeSpan(Convert.ToInt64(actualRange))).TotalDays;
            var minRangeInDays = (new TimeSpan(minRangeTicks)).TotalDays;
            var maxRangeInDays = (new TimeSpan(maxRangeTicks)).TotalDays;

            TestContext.WriteLine("Range:{0} days  min allowed:{1} days  max allowed:{2} days  lower bound:{3} upper bound:{4}", actualRangeInDays, minRangeInDays, maxRangeInDays, lowerBound, upperBound);
            Assert.IsTrue(actualRange > minRangeTicks);
            Assert.IsTrue(actualRange < maxRangeTicks);

        }

        #endregion

    }
}
