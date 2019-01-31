using System;
using System.Linq;
using Xunit;
using TestHelperExtensions.Test.Helpers;
using System.Text;

namespace TestHelperExtensions.Test
{

    public class DoubleExtensions_GetRandom_Should
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
            var rnd = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(rnd.Next(Int32.MaxValue) + rnd.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(rnd.Next(Int32.MaxValue))) - rnd.NextDouble();
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
            var rnd = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(rnd.Next(Int32.MaxValue) + rnd.NextDouble());
            double lowerBound = upperBound - Convert.ToDouble(rnd.Next(Int32.MaxValue)) - rnd.NextDouble();

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("LowerBound={0}, UpperBound={1}, Actual={2}", lowerBound, upperBound, actual);
                Assert.True(actual < upperBound, message);
            }
        }

        [Fact]
        public void NotReachTheUpperBoundIfAValueGreaterThanALongIsSpecified()
        {
            var rnd = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int64.MaxValue) + Convert.ToDouble(rnd.Next(Int16.MaxValue) + rnd.NextDouble());
            double lowerBound = upperBound - Convert.ToDouble(rnd.Next(Int32.MaxValue)) - rnd.NextDouble();

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("LowerBound={0}, UpperBound={1}, Actual={2}", lowerBound, upperBound, actual);
                Assert.True(actual < upperBound, message);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var rnd = Randomizer.Create();
            double upperBound = Convert.ToDouble(Int64.MaxValue) + Convert.ToDouble(rnd.Next(Int16.MaxValue) + rnd.NextDouble());
            double lowerBound = upperBound + Convert.ToDouble(rnd.Next(Int32.MaxValue)) - rnd.NextDouble();
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
            double upperBound = 2.9;
            double lowerBound = 2.1;

            double minValue = upperBound;
            double maxValue = lowerBound;

            for (int i = 0; i < _executionCount; i++)
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
            const double tolerance = .1;
            var rnd = Randomizer.Create();

            double upperBound = Int16.MaxValue + rnd.Next(Int16.MaxValue) + rnd.NextDouble();
            double range = rnd.Next(Int16.MaxValue) + rnd.Next();
            double lowerBound = upperBound - range;

            ValidateAverageResultNearTheMiddleOfTheRange(upperBound, lowerBound, tolerance);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .1;
            var rnd = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int16.MaxValue) + Convert.ToDouble(rnd.Next(Int16.MaxValue) + rnd.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(rnd.Next(Int16.MaxValue))) - rnd.NextDouble();

            ValidateResultsAcrossTheEntireRangeOfTheRequest(lowerBound, upperBound, tolerance);
        }

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRangeForASmallRange()
        {
            const double tolerance = .02;
            var rnd = Randomizer.Create();

            double upperBound = rnd.NextDouble() * Convert.ToDouble(rnd.Next(byte.MaxValue));
            double lowerBound = upperBound - rnd.NextDouble();

            ValidateAverageResultNearTheMiddleOfTheRange(upperBound, lowerBound, tolerance);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequestForASmallRange()
        {
            const double tolerance = .1;
            var rnd = Randomizer.Create();

            double upperBound = rnd.NextDouble() * Convert.ToDouble(rnd.Next(byte.MaxValue));
            double lowerBound = upperBound - rnd.NextDouble();

            ValidateResultsAcrossTheEntireRangeOfTheRequest(lowerBound, upperBound, tolerance);
        }

        #endregion


        #region Helper Methods

        private static void ValidateAverageResultNearTheMiddleOfTheRange(double upperBound, double lowerBound, double tolerance)
        {
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

            string message = string.Format("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", actualMean, minMean, maxMean, lowerBound, upperBound);
            Assert.True(actualMean > minMean, message);
            Assert.True(actualMean < maxMean, message);
        }

        private static void ValidateResultsAcrossTheEntireRangeOfTheRequest(double lowerBound, double upperBound, double tolerance)
        {
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
