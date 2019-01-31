using System;
using Xunit;
using System.Linq;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{

    public class DateTimeExtensions_GetRandom_Should
    {
        // Note: Interaction tests are even less valuable here then
        // they are in some of the other contexts since this implementation
        // leverages the GetRandom methods for the Int64 data type. As a result
        // only rules type tests are supplied for the DateTime data type
        // random data features.

        const int _executionCount = 5000;

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
            var random = Randomizer.Create();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual >= lowerBound, message);
            }
        }

        [Fact]
        public void NotReachTheUpperBound()
        {
            var random = Randomizer.Create();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual < upperBound, message);
            }
        }

        [Fact]
        public void AlwaysBeAboveTheMinimumDateTimeValueIfNoLowerBoundSpecified()
        {
            var random = Randomizer.Create();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var minAllowed = DateTime.MinValue;

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom();
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual >= minAllowed, message);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var random = Randomizer.Create();
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
            const double tolerance = .1;
            var random = Randomizer.Create();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));

            var range = upperBound.Ticks - lowerBound.Ticks;
            var expectedMeanTicks = (range / 2) + lowerBound.Ticks;
            var slopTicks = range * tolerance;
            var minMeanTicks = Convert.ToInt64(expectedMeanTicks - slopTicks);
            var maxMeanTicks = Convert.ToInt64(expectedMeanTicks + slopTicks);

            double sum = 0.0;
            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                sum += value.Ticks;
            }

            var actualMean = sum / _executionCount;

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", new DateTime(Convert.ToInt64(Math.Round(actualMean))), new DateTime(minMeanTicks), new DateTime(maxMeanTicks), lowerBound, upperBound);
            Assert.True(actualMean > minMeanTicks);
            Assert.True(actualMean < maxMeanTicks);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .1;
            var random = Randomizer.Create();

            var upperBound = DateTime.UtcNow.AddSeconds(random.Next(Int32.MaxValue));
            var lowerBound = DateTime.UtcNow.AddSeconds(-random.Next(Int32.MaxValue));

            double expectedRangeTicks = upperBound.Ticks - lowerBound.Ticks;
            var slop = Convert.ToDouble(expectedRangeTicks * tolerance);
            var maxLowTicks = Convert.ToInt64(Math.Round(lowerBound.Ticks + slop));
            var minHighTicks = Convert.ToInt64(Math.Round(upperBound.Ticks - slop));

            Int64 minTicks = upperBound.Ticks;
            Int64 maxTicks = lowerBound.Ticks;
            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                if (value.Ticks < minTicks) minTicks = value.Ticks;
                if (value.Ticks > maxTicks) maxTicks = value.Ticks;
            }

            string message = string.Format("minTicks:{0}, maxTicks:{1}, maxLowTicks:{2}, minHighTicks:{3}, lowerBound:{4}, upperBound:{5}", minTicks, maxTicks, maxLowTicks, minHighTicks, lowerBound, upperBound);
            Assert.True(minTicks < maxLowTicks, message);
            Assert.True(maxTicks > minHighTicks, message);
        }

        #endregion

    }
}
