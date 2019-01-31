using System;
using System.Linq;
using Xunit;
using TestHelperExtensions;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{

    public class LongExtensions_GetRandom_Should
    {

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

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(Int32.MaxValue));
            long lowerBound = upperBound - Convert.ToInt64(random.Next(Int32.MaxValue));
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

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(Int32.MaxValue));
            long lowerBound = upperBound - Convert.ToInt64(random.Next(Int32.MaxValue));
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual < upperBound, message);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            var random = Randomizer.Create();

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(Int32.MaxValue));
            Console.WriteLine("UpperBound={0}", upperBound);

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom();
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual >= 0, message);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var random = Randomizer.Create();
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
            const double tolerance = .1;
            var random = Randomizer.Create();

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(valueRadius));
            long lowerBound = Convert.ToInt64(Int32.MaxValue) - Convert.ToInt64(random.Next(valueRadius));

            var range = upperBound - lowerBound;
            var expectedMean = (range / 2) + lowerBound;
            var slop = range * tolerance;
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            double sum = 0.0;
            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                sum += value;
            }

            var actualMean = sum / _executionCount;

            string message = string.Format("mean:{0} min allowed:{1} max allowed:{2}", actualMean, minMean, maxMean);
            Assert.True(actualMean > minMean, message);
            Assert.True(actualMean < maxMean, message);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const int valueRadius = 2000;
            const double tolerance = .1;
            var random = Randomizer.Create();

            long upperBound = Convert.ToInt64(Int32.MaxValue) + Convert.ToInt64(random.Next(valueRadius));
            long lowerBound = Convert.ToInt64(Int32.MaxValue) - Convert.ToInt64(random.Next(valueRadius));

            double expectedRange = upperBound - lowerBound;
            var slop = Convert.ToDouble(expectedRange * tolerance);

            var maxLowValue = lowerBound + slop;
            var minHighValue = upperBound - slop;

            var minValue = upperBound;
            var maxValue = lowerBound;

            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                if (value < minValue) minValue = value;
                if (value > maxValue) maxValue = value;
            }

            string message = string.Format("minValue:{0}, maxValue:{1}, maxLowValue:{2}, minHighValue:{3}, lowerBound:{4}, upperBound:{5}", minValue, maxValue, maxLowValue, minHighValue, lowerBound, upperBound);
            Assert.True(minValue < maxLowValue, message);
            Assert.True(maxValue > minHighValue, message);
        }

        #endregion

    }
}
