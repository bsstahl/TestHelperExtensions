using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class StringExtensions_GetRandom_Should
    {
        [TestMethod]
        public void ReturnAStringOfLength8IfNoLengthSpecified()
        {
            var actual = string.Empty.GetRandom();
            Assert.AreEqual(8, actual.Length);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionIfSpecifiedLengthIsZero()
        {
            var actual = string.Empty.GetRandom(0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionIfSpecifiedLengthIsNegative()
        {
            var length = -50.GetRandom(1);
            var actual = string.Empty.GetRandom(length);
        }

        [TestMethod]
        public void ReturnAStringOfTheSpecifiedLength()
        {
            for (int i = 0; i < 1000; i++)
            {
                var expectedLength = 50.GetRandom(1);
                var actual = string.Empty.GetRandom(expectedLength);
                Assert.AreEqual(expectedLength, actual.Length);
            }
        }

        /// <remarks>
        /// This is a sanity-check only, not a test for randomness
        /// </remarks>
        [TestMethod]
        public void NotReturnConsecutiveIdenticalStrings()
        {
            string lastValue = string.Empty;
            for (int i = 0; i < 1000; i++)
            {
                var actual = string.Empty.GetRandom(50);
                Assert.IsFalse(actual.Equals(lastValue));
                lastValue = actual;
            }
        }

    }

}
