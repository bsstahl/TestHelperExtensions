using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;
using Microsoft.QualityTools.Testing.Fakes;

namespace TestHelperExtensions.Test
{
    // We do not need to test the randomness of the Microsoft
    // random number generator. We just need to test our interaction
    // with that service.  Some possible failure vectors include
    // failure to call the service, failure to use the value
    // returned properly, and one-off errors such as failing
    // to include a value on the inclusive side or exclude a
    // value on the non-inclusive side.

    [TestClass]
    public class BoolExtensions_GetRandom_Should
    {
        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void TestCleanup()
        {
            TestHelperExtensions.BoolExtensions._rnd = new Random();
        }

        [TestMethod]
        public void ProduceAFalseIfTheRandomValueIsZero()
        {
            var r = new System.Fakes.StubRandom();
            r.NextDouble01 = () => 0.49;
            TestHelperExtensions.BoolExtensions._rnd = r;

            var actual = true.GetRandom();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProduceAFalseIfTheRandomValueIsLessThanZeroPointFive()
        {
            var r = new System.Fakes.StubRandom();
            r.NextDouble01 = () => 0.49;
            TestHelperExtensions.BoolExtensions._rnd = r;

            var actual = true.GetRandom();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProduceATrueIfTheRandomValueIsMaximized()
        {
            var r = new System.Fakes.StubRandom();
            r.NextDouble01 = () => 0.999999999999;
            TestHelperExtensions.BoolExtensions._rnd = r;

            var actual = true.GetRandom();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProduceATrueIfTheRandomValueIsGreaterThanZeroPointFive()
        {
            var r = new System.Fakes.StubRandom();
            r.NextDouble01 = () => 0.51;
            TestHelperExtensions.BoolExtensions._rnd = r;

            var actual = true.GetRandom();
            Assert.IsTrue(actual);
        }


        // The odds of randomly picking 100 consecutive identical
        // values are approximately 1 in 6.3 x 10^29th
        // For perspective, the estimated number of stars in
        // the universe is 2 x 10^22nd.
        // This test verifies that we will actually get some
        // true values. In combination with the test that
        // guarantees we actually get some false values, it
        // makes sure we are actually calling the random
        // number generator.
        [TestMethod]
        public void ReturnTrueWithinTheFirstHundredCalls()
        {
            int i = 0;
            bool actual = false;
            while (i < 100 && !actual)
            {
                actual = actual.GetRandom();
                TestContext.WriteLine("Result: {0}", actual.ToString());
            }
            Assert.IsTrue(actual);
        }

        // The odds of randomly picking 100 consecutive identical
        // values are approximately 1 in 6.3 x 10^29th
        // For perspective, the estimated number of stars in
        // the universe is 2 x 10^22nd.
        // This test verifies that we will actually get some
        // false values. In combination with the test that
        // guarantees we actually get some true values, it
        // makes sure we are actually calling the random
        // number generator.
        [TestMethod]
        public void ReturnFalseWithinTheFirstHundredCalls()
        {
            int i = 0;
            bool actual = true;
            while (i < 100 && actual)
            {
                actual = actual.GetRandom();
                TestContext.WriteLine("Result: {0}", actual.ToString());
            }
            Assert.IsFalse(actual);
        }

        // This test is a sanity-check. We are not
        // testing true randomness here, just making
        // sure our results are not crazily skewed.
        [TestMethod]
        public void ReturnRoughlyFiftyPercentTrue()
        {
            const int totalExecutions = 10000;
            const double tolerance = 0.20;

            int trueCount = 0;
            bool actual = false;
            for (int i = 0; i < totalExecutions; i++)
            {
                actual = actual.GetRandom();
                if (actual)
                    trueCount++;
            }

            int deltaCount = Convert.ToInt32(totalExecutions * tolerance);
            int halfCount = Convert.ToInt32(totalExecutions / 2);
            int minCount = halfCount - deltaCount;
            int maxCount = halfCount + deltaCount;

            TestContext.WriteLine("True: {0}  -  Min: {1}  - Max: {2}", trueCount, minCount, maxCount);
            Assert.IsTrue(trueCount > minCount);
            Assert.IsTrue(trueCount < maxCount);
        }

    }
}
