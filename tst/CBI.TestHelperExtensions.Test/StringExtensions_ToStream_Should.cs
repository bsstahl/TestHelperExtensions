using System;
using Xunit;

namespace TestHelperExtensions.Test
{
    
    public class StringExtensions_ToStream_Should
    {
        [Fact]
        public void ReturnAZeroLenghtBufferIfTheTargetIsEmpty()
        {
            var target = string.Empty;
            var actual = target.ToStream();
            Assert.Equal(0, actual.Length);
            actual.Dispose();
        }

        [Fact]
        public void ReturnAZeroLenghtBufferIfTheTargetIsNull()
        {
            string target = null;
            var actual = target.ToStream();
            Assert.Equal(0, actual.Length);
            actual.Dispose();
        }

        [Fact]
        public void ReturnABufferWithTheSameLengthAsTheSource()
        {
            var expectedLength = byte.MaxValue.GetRandom(5);
            var target = string.Empty.GetRandom(expectedLength);
            var actual = target.ToStream();
            Assert.Equal(expectedLength, actual.Length);
            actual.Dispose();
        }

        [Fact]
        public void ReturnAStreamThatConvertsToTheOriginalString()
        {
            var expectedLength = byte.MaxValue.GetRandom(5);
            var target = string.Empty.GetRandom(expectedLength);
            var actual = target.ToStream();
            var reader = new System.IO.StreamReader(actual);
            var result = reader.ReadToEnd();
            Assert.Equal(target, result);
            actual.Dispose();
        }

    }
}
