using System;
using System.Linq;
using Xunit;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{

    public class ByteExtensions_GetRandomEnumerable_Should
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
            var length = 25.GetRandom(5);
            var maxAllowed = Convert.ToByte(Randomizer.Create().Next(200, 230));
            for (int i = 0; i < _executionCount; i++)
            {
                var value = maxAllowed.GetRandomEnumerable(0, length);
                string message = string.Format("value:{0}, max allowed:{1}", value, maxAllowed);
                Assert.DoesNotContain(value, v => v > maxAllowed);
            }
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var length = 25.GetRandom(5);
            var minAllowed = Convert.ToByte(Randomizer.Create().Next(10, 30));
            for (int i = 0; i < _executionCount; i++)
            {
                var value = Convert.ToByte(255).GetRandomEnumerable(minAllowed, length);
                string message = string.Format("value:{0} min allowed:{1}", value, minAllowed);
                Assert.DoesNotContain(value, v => v < minAllowed);
            }
        }

        [Fact]
        public void AlwaysBeTheSpecifiedLength()
        {
            var length = 500.GetRandom(25);
            for (int i = 0; i < _executionCount; i++)
            {
                var actual = Convert.ToByte(255).GetRandomEnumerable(0, length);
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual.Count() == length, message);
            }
        }

        [Fact]
        public void AlwaysBeAtLeastOneElementLongIfNoLengthSpecified()
        {
            for (int i = 0; i < _executionCount; i++)
            {
                var actual = Convert.ToByte(255).GetRandomEnumerable();
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual.Count() > 0, message);
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
            int elementCount = 0;
            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandomEnumerable(lowerBound);
                sum += value.Sum(v => v);
                elementCount += value.Count();
            }

            var actualMean = sum / elementCount;
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
                var value = upperBound.GetRandomEnumerable(lowerBound);
                if (value.Min() < lowestValue) lowestValue = value.Min();
                if (value.Max() > highestValue) highestValue = value.Max();
            }

            string message = string.Format("lowestValue:{0}, highestValue:{1}, maxLowValue:{2}, minHighValue:{3}, lowerBound:{4}, upperBound:{5}", lowestValue, highestValue, maxLowValue, minHighValue, lowerBound, upperBound);
            Assert.True(lowestValue < maxLowValue, message);
            Assert.True(highestValue > minHighValue, message);
        }


        #endregion

    }
}
