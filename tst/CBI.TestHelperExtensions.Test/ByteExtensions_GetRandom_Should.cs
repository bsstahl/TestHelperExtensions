using System;
using System.Linq;
using Xunit;

namespace TestHelperExtensions.Test
{
    
    public class ByteExtensions_GetRandom_Should
    {

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
            var random = new Random();
            var maxAllowed = Convert.ToByte(random.Next(200, 230));

            var result = 100000.GetRandomByteValues(maxAllowed, 0);
            var maxValue = result.Max();

            Console.WriteLine("max value:{0} max allowed:{1}", maxValue, maxAllowed);
            Assert.True(maxValue < maxAllowed);
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var random = new Random();
            var minAllowed = Convert.ToByte(random.Next(10, 30));

            var result = 100000.GetRandomByteValues(byte.MaxValue, minAllowed);
            var minValue = result.Min();

            Console.WriteLine("min value:{0} min allowed:{1}", minValue, minAllowed);
            Assert.True(minValue >= minAllowed);
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;
            var random = new Random();

            byte upperBound = Convert.ToByte(byte.MaxValue - Convert.ToByte(random.Next(100)));
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
            var minAllowed = Convert.ToByte(random.Next(10, 30));
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
            const double tolerance = .02;

            var expectedMean = Convert.ToByte(((upperBound - 1) - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToByte(expectedMean * tolerance);
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            var result = 100000.GetRandomByteValues(upperBound, lowerBound); //.GetValuesDistribution();
            var actualMean = result.Average(v => Convert.ToInt32(v));

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2}", actualMean, minMean, maxMean);
            Assert.True(actualMean > minMean);
            Assert.True(actualMean < maxMean);
        }

        [Fact]
        public void HaveAMedianResultNearTheMiddleOfTheRange()
        {
            const byte lowerBound = 0;
            const byte upperBound = 255;
            const double tolerance = .02;

            var expectedMedian = Convert.ToByte(((upperBound - 1) - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToByte(expectedMedian * tolerance);
            var minMedian = expectedMedian - slop;
            var maxMedian = expectedMedian + slop;

            var result = 100000.GetRandomByteValues(upperBound, lowerBound);
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

            byte upperBound = Convert.ToByte(byte.MaxValue - Convert.ToByte(random.Next(100)));
            byte lowerBound = Convert.ToByte(byte.MinValue + Convert.ToByte(random.Next(100)));

            double expectedRange = upperBound - lowerBound;
            var slop = Convert.ToByte(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;


            var result = 100000.GetRandomByteValues(upperBound, lowerBound);
            var actualRange = result.Range();

            Console.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.True(actualRange > minRange);
            Assert.True(actualRange < maxRange);

        }


        #endregion

    }
}
