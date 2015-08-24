using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class StringExtensions_ToStream_Should
    {
        [TestMethod]
        public void ReturnAZeroLenghtBufferIfTheTargetIsEmpty()
        {
            var target = string.Empty;
            var actual = target.ToStream();
            Assert.AreEqual(0, actual.Length);
            actual.Dispose();
        }

        [TestMethod]
        public void ReturnAZeroLenghtBufferIfTheTargetIsNull()
        {
            string target = null;
            var actual = target.ToStream();
            Assert.AreEqual(0, actual.Length);
            actual.Dispose();
        }

        [TestMethod]
        public void ReturnABufferWithTheSameLengthAsTheSource()
        {
            var expectedLength = byte.MaxValue.GetRandom(5);
            var target = string.Empty.GetRandom(expectedLength);
            var actual = target.ToStream();
            Assert.AreEqual(expectedLength, actual.Length);
            actual.Dispose();
        }

        [TestMethod]
        public void ReturnAStreamThatConvertsToTheOriginalString()
        {
            var expectedLength = byte.MaxValue.GetRandom(5);
            var target = string.Empty.GetRandom(expectedLength);
            var actual = target.ToStream();
            actual.Position = 0;
            var reader = new System.IO.StreamReader(actual);
            var result = reader.ReadToEnd();
            Assert.AreEqual(target, result);
            actual.Dispose();
        }

    }
}
