using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;
using System.Collections.Generic;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class ByteExtensions_GetRandom_Should
    {
        public TestContext TestContext { get; set; }
        private Random _random = new Random();

        [TestCleanup]
        public void TestCleanup()
        {
            TestHelperExtensions.ByteExtensions._rnd = new Random();
        }

        #region Interaction Tests

        // These tests require some knowledge of the underlying
        // implementation.  As a result, they can be more brittle
        // then the rules test (needing to be changed if the
        // implementation details change).

        [TestMethod]
        public void ReturnTheValueReturnedFromTheRandomNumberGenerator()
        {
            byte lowerBound = 10;
            byte upperBound = 50;
            var expected = _random.Next(lowerBound, upperBound);

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => expected;
            TestHelperExtensions.ByteExtensions._rnd = r;

            var actual = upperBound.GetRandom(lowerBound);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PassTheCorrectLowerBoundToTheRandomNumberGenerator()
        {
            byte lowerBound = Convert.ToByte(_random.Next(3, 250));
            byte upperBound = Convert.ToByte(lowerBound + 1);
            var expected = lowerBound;

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => i;
            TestHelperExtensions.ByteExtensions._rnd = r;

            var actual = upperBound.GetRandom(lowerBound);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PassTheCorrectUpperBoundToTheRandomNumberGenerator()
        {
            byte lowerBound = Convert.ToByte(_random.Next(3, 250));
            byte upperBound = Convert.ToByte(lowerBound + 1);
            var expected = upperBound;

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => j;
            TestHelperExtensions.ByteExtensions._rnd = r;

            var actual = upperBound.GetRandom(lowerBound);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PassALowerBoundOfZeroToTheRandomNumberGeneratorIfNoLowerBoundSpecified()
        {
            byte upperBound = Convert.ToByte(_random.Next(3, 250));

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => i;
            TestHelperExtensions.ByteExtensions._rnd = r;

            var actual = upperBound.GetRandom();
            Assert.AreEqual(0, actual);
        }

        #endregion

        #region Rules Tests

        [TestMethod]
        public void AlwaysBeBelowTheUpperBound()
        {
            var maxAllowed = Convert.ToByte(_random.Next(200, 230));

            var result = 100000.GetRandomByteValues(maxAllowed, 0);
            var maxValue = result.Max();

            TestContext.WriteLine("max value:{0} max allowed:{1}", maxValue, maxAllowed);
            Assert.IsTrue(maxValue < maxAllowed);
        }

        [TestMethod]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var minAllowed = Convert.ToByte(_random.Next(10, 30));

            var result = 100000.GetRandomByteValues(byte.MaxValue, minAllowed);
            var minValue = result.Min();

            TestContext.WriteLine("min value:{0} min allowed:{1}", minValue, minAllowed);
            Assert.IsTrue(minValue >= minAllowed);
        }

        [TestMethod]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;

            byte upperBound = Convert.ToByte(byte.MaxValue - Convert.ToByte(_random.Next(100)));
            TestContext.WriteLine("UpperBound={0}", upperBound);

            for (int i = 0; i < executionCount; i++)
            {
                var actual = upperBound.GetRandom();
                TestContext.WriteLine("Actual={0}", actual);
                Assert.IsTrue(actual >= 0);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var minAllowed = Convert.ToByte(_random.Next(10, 30));
            var maxAllowed = minAllowed - 5;
            var result = maxAllowed.GetRandom(minAllowed);
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [TestMethod]
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

            TestContext.WriteLine("mean:{0} min allowed:{1} max allowed:{2}", actualMean, minMean, maxMean);
            Assert.IsTrue(actualMean > minMean);
            Assert.IsTrue(actualMean < maxMean);
        }

        [TestMethod]
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

            TestContext.WriteLine("median:{0} min allowed:{1} max allowed:{2}", actualMedian, minMedian, maxMedian);
            Assert.IsTrue(actualMedian > minMedian);
            Assert.IsTrue(actualMedian < maxMedian);
        }

        [TestMethod]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .02;

            byte upperBound = Convert.ToByte(byte.MaxValue - Convert.ToByte(_random.Next(100)));
            byte lowerBound = Convert.ToByte(byte.MinValue + Convert.ToByte(_random.Next(100)));

            double expectedRange = upperBound - lowerBound;
            var slop = Convert.ToByte(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;


            var result = 100000.GetRandomByteValues(upperBound, lowerBound);
            var actualRange = result.Range();

            TestContext.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.IsTrue(actualRange > minRange);
            Assert.IsTrue(actualRange < maxRange);

        }


        #endregion

    }
}
