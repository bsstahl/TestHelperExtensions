using System;
using Xunit;

namespace TestHelperExtensions.Test
{
    // We do not need to test the randomness of the Microsoft
    // random number generator. We just need to test our interaction
    // with that service.  Some possible failure vectors include
    // failure to call the service, failure to use the value
    // returned properly, and one-off errors such as failing
    // to include a value on the inclusive side or exclude a
    // value on the non-inclusive side.
    
    public class BoolExtensions_GetRandom_Should
    {

        [Fact]
        public void ProduceAFalseIfTheRandomValueIsZero()
        {
            var actual = true.GetRandom(0.0);
            Assert.False(actual);
        }

        [Fact]
        public void ProduceAFalseIfTheRandomValueIsLessThanZeroPointFive()
        {
            var actual = true.GetRandom(0.49);
            Assert.False(actual);
        }

        [Fact]
        public void ProduceATrueIfTheRandomValueIsMaximized()
        {
            var actual = true.GetRandom(0.999999999999);
            Assert.True(actual);
        }

        [Fact]
        public void ProduceATrueIfTheRandomValueIsGreaterThanZeroPointFive()
        {
            var actual = true.GetRandom(0.51);
            Assert.True(actual);
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
        [Fact]
        public void ReturnTrueWithinTheFirstHundredCalls()
        {
            int i = 0;
            bool actual = false;
            while (i < 100 && !actual)
            {
                actual = actual.GetRandom();
                Console.WriteLine("Result: {0}", actual.ToString());
            }
            Assert.True(actual);
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
        [Fact]
        public void ReturnFalseWithinTheFirstHundredCalls()
        {
            int i = 0;
            bool actual = true;
            while (i < 100 && actual)
            {
                actual = actual.GetRandom();
                Console.WriteLine("Result: {0}", actual.ToString());
            }
            Assert.False(actual);
        }

        // This test is a sanity-check. We are not
        // testing true randomness here, just making
        // sure our results are not crazily skewed.
        [Fact]
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

            Console.WriteLine("True: {0}  -  Min: {1}  - Max: {2}", trueCount, minCount, maxCount);
            Assert.True(trueCount > minCount);
            Assert.True(trueCount < maxCount);
        }

    }
}
