using System;
using System.Linq;
using Xunit;
using TestHelperExtensions;
using System.Collections.Generic;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{
    
    public class ShortExtensions_GetRandom_Should
    {
        const int _executionCount = 5000;

        #region Rules Tests

        [Fact]
        public void AlwaysBeBelowTheUpperBound()
        {
            var maxAllowed = Convert.ToInt16(Randomizer.Create().Next(byte.MaxValue + 1, byte.MaxValue + 230));
            for (int i = 0; i < _executionCount; i++)
            {
                var value = maxAllowed.GetRandom(0);
                string message = string.Format("max value:{0} max allowed:{1}", value, maxAllowed);
                Assert.True(value < maxAllowed);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var minAllowed = Convert.ToInt16(Randomizer.Create().Next(byte.MaxValue + 1, byte.MaxValue + 230));
            var maxAllowed = Convert.ToInt16(minAllowed + 500);

            for (int i = 0; i < _executionCount; i++)
            {
                var value = maxAllowed.GetRandom(minAllowed);
                string message = string.Format("value:{0} min allowed:{1}", value, minAllowed);
                Assert.True(value >= minAllowed, message);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            int upperBound = Int32.MaxValue - Randomizer.Create().Next(100);
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
            var rnd = Randomizer.Create();
            int upperBound = checked(Int16.MaxValue + rnd.Next(100, 1000));
            int lowerBound = checked(upperBound + rnd.Next(100));
            if (upperBound >= lowerBound)
                Assert.False(true, $"Invalid test conditions - lowerBound:{lowerBound}, upperBound:{upperBound}");
            Assert.Throws<ArgumentOutOfRangeException>(() => upperBound.GetRandom(lowerBound));
        }


        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const int lowerBound = 0;
            const int upperBound = Int32.MaxValue;
            const double tolerance = .1;

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
            const double tolerance = .1;
            var rnd = Randomizer.Create();

            int upperBound = rnd.Next(100);
            int lowerBound = -rnd.Next(100);

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
