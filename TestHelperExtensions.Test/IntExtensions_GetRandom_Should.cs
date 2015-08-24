using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;
using System.Collections.Generic;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class IntExtensions_GetRandom_Should
    {
        public TestContext TestContext { get; set; }
        private Random _random = new Random();

        [TestCleanup]
        public void TestCleanup()
        {
            TestHelperExtensions.IntExtensions._rnd = new Random();
        }

        #region Interaction Tests

        // These tests require some knowledge of the underlying
        // implementation.  As a result, they can be more brittle
        // then the rules test (needing to be changed if the
        // implementation details change).

        [TestMethod]
        public void ReturnTheValueReturnedFromTheRandomNumberGenerator()
        {
            int lowerBound = Int16.MaxValue + 1;
            int upperBound = Int32.MaxValue;
            var expected = _random.Next(lowerBound, upperBound);

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => expected;
            TestHelperExtensions.IntExtensions._rnd = r;

            var actual = upperBound.GetRandom(lowerBound);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PassTheCorrectLowerBoundToTheRandomNumberGenerator()
        {
            int lowerBound = _random.Next(3, 250) + Int16.MaxValue;
            int upperBound = lowerBound + 1;
            var expected = lowerBound;

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => i;
            TestHelperExtensions.IntExtensions._rnd = r;

            var actual = upperBound.GetRandom(lowerBound);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PassTheCorrectUpperBoundToTheRandomNumberGenerator()
        {
            int lowerBound = _random.Next(3, 250) + Int16.MaxValue;
            int upperBound = lowerBound + 1;

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => j;
            TestHelperExtensions.IntExtensions._rnd = r;

            var actual = upperBound.GetRandom(lowerBound);
            Assert.AreEqual(upperBound, actual);
        }

        [TestMethod]
        public void PassALowerBoundOfZeroToTheRandomNumberGeneratorIfNoLowerBoundSpecified()
        {
            int upperBound = _random.Next(3, 250) + Int16.MaxValue;

            var r = new System.Fakes.StubRandom();
            r.NextInt32Int32 = (i, j) => i;
            TestHelperExtensions.IntExtensions._rnd = r;

            var actual = upperBound.GetRandom();
            Assert.AreEqual(0, actual);
        }

        #endregion

        #region Rules Tests

        [TestMethod]
        public void AlwaysBeBelowTheUpperBound()
        {
            var maxAllowed = _random.Next(Int16.MaxValue + 1, Int16.MaxValue + 230);

            var result = 100000.GetRandomIntegerValues(maxAllowed, 0);
            var maxValue = result.Max();

            TestContext.WriteLine("max value:{0} max allowed:{1}", maxValue, maxAllowed);
            Assert.IsTrue(maxValue < maxAllowed);
        }

        [TestMethod]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var minAllowed = _random.Next(Int16.MaxValue + 1, Int16.MaxValue + 230);
            var maxAllowed = minAllowed + 500;

            var result = 100000.GetRandomIntegerValues(maxAllowed, minAllowed);
            var minValue = result.Min();

            TestContext.WriteLine("min value:{0} min allowed:{1}", minValue, minAllowed);
            Assert.IsTrue(minValue >= minAllowed);
        }

        [TestMethod]
        public void AlwaysBeAboveOrEqualToZeroIfNoLowerBoundSpecified()
        {
            const int executionCount = 10000;

            int upperBound = Int32.MaxValue - _random.Next(100);
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
            int upperBound = Int16.MaxValue + _random.Next(100, 1000);
            int lowerBound = upperBound + _random.Next(100);
            var result = upperBound.GetRandom(lowerBound);
        }


        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [TestMethod]
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

            TestContext.WriteLine("mean:{0} min allowed:{1} max allowed:{2}", actualMean, minMean, maxMean);
            Assert.IsTrue(actualMean > minMean);
            Assert.IsTrue(actualMean < maxMean);
        }

        [TestMethod]
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

            TestContext.WriteLine("median:{0} min allowed:{1} max allowed:{2}", actualMedian, minMedian, maxMedian);
            Assert.IsTrue(actualMedian > minMedian);
            Assert.IsTrue(actualMedian < maxMedian);
        }

        [TestMethod]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const double tolerance = .02;

            int upperBound = _random.Next(10000);
            int lowerBound = -_random.Next(10000);

            int expectedRange = upperBound - lowerBound;
            var slop = Convert.ToInt32(expectedRange * tolerance);
            var minRange = expectedRange - slop;
            var maxRange = expectedRange + slop;


            var result = 100000.GetRandomIntegerValues(upperBound, lowerBound);
            var actualRange = result.Range();

            TestContext.WriteLine("range:{0} min allowed:{1} max allowed:{2}", actualRange, minRange, maxRange);
            Assert.IsTrue(actualRange >= minRange);
            Assert.IsTrue(actualRange <= maxRange);

        }


        #endregion

    }
}
