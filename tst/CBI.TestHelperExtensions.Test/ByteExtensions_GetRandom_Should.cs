using System;
using System.Linq;
using Xunit;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{

    public class ByteExtensions_GetRandom_Should
    {

        const int _executionCount = 1000;

        #region Interaction Tests

        // Interaction tests are not required here since the
        // implementation is such a thin wrapper over the
        // base functionality.  If that changes, we'll need
        // to add these tests back. Unfortunately, since
        // they require some knowledge of the underlying
        // implementation they can be more brittle
        // then the rules test (needing to be changed if the
        // implementation details change).

        #endregion

        #region Rules Tests

        [Fact]
        public void AlwaysBeBelowTheUpperBound()
        {
            var maxAllowed = Convert.ToByte(Randomizer.Create().Next(200, 230));
            for (int i = 0; i < _executionCount; i++)
            {
                var value = maxAllowed.GetRandom(0);
                string message = string.Format("value:{0}, max allowed:{1}", value, maxAllowed);
                Assert.True(value < maxAllowed, message);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var minAllowed = Convert.ToByte(Randomizer.Create().Next(10, 30));
            for (int i = 0; i < _executionCount; i++)
            {
                var value = byte.MaxValue.GetRandom(minAllowed);
                string message = string.Format("value:{0} min allowed:{1}", value, minAllowed);
                Assert.True(value >= minAllowed);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            byte upperBound = Convert.ToByte(byte.MaxValue - Convert.ToByte(Randomizer.Create().Next(100)));
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
            var minAllowed = Convert.ToByte(Randomizer.Create().Next(10, 30));
            var maxAllowed = minAllowed - 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => maxAllowed.GetRandom(minAllowed));
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const byte lowerBound = 0;
            const byte upperBound = 255;
            const double tolerance = .1;

            var range = upperBound - lowerBound;
            var expectedMean = (range / 2) + lowerBound;
            var slop = expectedMean * tolerance;
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            double sum = 0.0;
            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                sum += value;
            }

            var actualMean = sum / _executionCount;
            string message = string.Format("mean:{0}, min allowed:{1}, max allowed:{2}", actualMean, minMean, maxMean);
            Assert.True(actualMean > minMean, message);
            Assert.True(actualMean < maxMean, message);

        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .1;
            Random rnd = Randomizer.Create();

            byte upperBound = Convert.ToByte(byte.MaxValue - Convert.ToByte(rnd.Next(100)));
            byte lowerBound = Convert.ToByte(byte.MinValue + Convert.ToByte(rnd.Next(100)));

            double expectedRange = upperBound - lowerBound;
            var slop = expectedRange * tolerance;

            var maxLowValue = lowerBound + slop;
            var minHighValue = upperBound - slop;

            var lowestValue = upperBound;
            var highestValue = lowerBound;

            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                if (value < lowestValue) lowestValue = value;
                if (value > highestValue) highestValue = value;
            }

            string message = string.Format("lowestValue:{0}, highestValue:{1}, maxLowValue:{2}, minHighValue:{3}, lowerBound:{4}, upperBound:{5}", lowestValue, highestValue, maxLowValue, minHighValue, lowerBound, upperBound);
            Assert.True(lowestValue < maxLowValue, message);
            Assert.True(highestValue > minHighValue, message);
        }


        #endregion

    }
}
