using System;
using Xunit;

namespace TestHelperExtensions.Test
{
    
    public class StringExtensions_GetRandom_Should
    {
        [Fact]
        public void ReturnAStringOfLength8IfNoLengthSpecified()
        {
            var actual = string.Empty.GetRandom();
            Assert.Equal(8, actual.Length);
        }

        [Fact]
        public void ThrowArgumentExceptionIfSpecifiedLengthIsZero()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.GetRandom(0));
        }

        [Fact]
        public void ThrowArgumentExceptionIfSpecifiedLengthIsNegative()
        {
            var length = -50.GetRandom(1);
            Assert.Throws<ArgumentException>(() => string.Empty.GetRandom(length));
        }

        [Fact]
        public void ReturnAStringOfTheSpecifiedLength()
        {
            for (int i = 0; i < 1000; i++)
            {
                var expectedLength = 50.GetRandom(1);
                var actual = string.Empty.GetRandom(expectedLength);
                Assert.Equal(expectedLength, actual.Length);
            }
        }

        /// <remarks>
        /// This is a sanity-check only, not a test for randomness
        /// </remarks>
        [Fact]
        public void NotReturnConsecutiveIdenticalStrings()
        {
            string lastValue = string.Empty;
            for (int i = 0; i < 1000; i++)
            {
                var actual = string.Empty.GetRandom(50);
                Assert.NotEqual(actual, lastValue);
                lastValue = actual;
            }
        }

    }

}
