using System;
using System.Linq;
using Xunit;
using TestHelperExtensions;
using System.Collections.Generic;

namespace TestHelperExtensions.Test
{

    public class IntExtensions_GetRandom_Should
    {

        #region Rules Tests

        [Fact]
        public void AlwaysBeBelowTheUpperBound()
        {
            var random = new Random();
            var maxAllowed = random.Next(Int16.MaxValue + 1, Int16.MaxValue + 230);

            var result = 100000.GetRandomIntegerValues(maxAllowed, 0);
            var maxValue = result.Max();

            Console.WriteLine("max value:{0} max allowed:{1}", maxValue, maxAllowed);
            Assert.True(maxValue < maxAllowed);
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var random = new Random();
            var minAllowed = random.Next(Int16.MaxValue + 1, Int16.MaxValue + 230);
            var maxAllowed = minAllowed + 500;

            var result = 100000.GetRandomIntegerValues(maxAllowed, minAllowed);
            var minValue = result.Min();

            Console.WriteLine("min value:{0} min allowed:{1}", minValue, minAllowed);
            Assert.True(minValue >= minAllowed);
        }

        [Fact]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;
            var random = new Random();

            int upperBound = Int32.MaxValue - random.Next(100);
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
            int upperBound = Int16.MaxValue + random.Next(100, 1000);
            int lowerBound = upperBound + random.Next(100);
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
            const double tolerance = .02;

            var expectedMean = Convert.ToInt32(((upperBound - 1) - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToInt32(expectedMean * tolerance);
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            var result = 100000.GetRandomIntegerValues(upperBound, lowerBound); //.GetValuesDistribution();
            var actualMean = result.Average(v => v);

            Console.WriteLine("mean:{0} min allowed:{1} max allowed:{2}", actualMean, minMean, maxMean);
            Assert.True(actualMean > minMean);
            Assert.True(actualMean < maxMean);
        }

        [Fact]
        public void HaveAMedianResultNearTheMiddleOfTheRange()
        {
            const int lowerBound = 0;
            const int upperBound = Int32.MaxValue;
            const double tolerance = .02;

            var expectedMedian = Convert.ToInt32(((upperBound - 1) - lowerBound) / 2) + lowerBound;
            var slop = Convert.ToInt32(expectedMedian * tolerance);
            var minMedian = expectedMedian - slop;
            var maxMedian = expectedMedian + slop;

            var result = 100000.GetRandomIntegerValues(upperBound, lowerBound);
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

            int upperBound = random.Next(10000);
            int lowerBound = -random.Next(10000);

            int expectedRange = upperBound - lowerBound;
            var slop = Convert.ToInt32(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;


            var result = 100000.GetRandomIntegerValues(upperBound, lowerBound);
            var actualRange = result.Range();

            Console.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.True(actualRange >= minRange);
            Assert.True(actualRange <= maxRange);

        }


        #endregion

    }
}
