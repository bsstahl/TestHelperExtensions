using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensions_Repeat_Should
    {

        #region Overload with no separator

        [Fact]
        public void ThrowAnArgumentNullExceptionIfTheStringIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Repeat(99.GetRandom()));
        }

        [Fact]
        public void ReturnAnEmptyStringIfAnEmptyStringIsSupplied()
        {
            Assert.Empty(string.Empty.Repeat(99.GetRandom()));
        }

        [Fact]
        public void ReturnAnNCharacterStringIfAOneCharacterStringIsSupplied()
        {
            int expectedLength = 99.GetRandom(10);
            var actual = string.Empty.GetRandom(1).Repeat(expectedLength);
            Assert.True(actual.Length == expectedLength);
        }

        [Fact]
        public void ReturnATwoNCharacterStringIfATwoCharacterStringIsSupplied()
        {
            int repeatCount = 99.GetRandom(10);
            int expectedLength = repeatCount * 2;
            var actual = string.Empty.GetRandom(2).Repeat(repeatCount);
            Assert.True(actual.Length == expectedLength);
        }

        [Fact]
        public void ReturnAnLNCharacterStringIfAnLCharacterStringIsSupplied()
        {
            int sourceLength = 99.GetRandom(10);
            int repeatCount = 99.GetRandom(10);
            int expectedLength = sourceLength * repeatCount;
            var actual = string.Empty.GetRandom(sourceLength).Repeat(repeatCount);
            Assert.True(actual.Length == expectedLength);
        }

        [Fact]
        public void ReturnTheProperValue()
        {
            string source = "abc";
            string expected = "abcabcabcabc";
            var actual = source.Repeat(4);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnTheProperValueForARandomRequest()
        {
            int repeatCount = 25.GetRandom(10);
            int sourceLength = 99.GetRandom(5);
            string source = string.Empty.GetRandom(sourceLength);

            var sb = new StringBuilder();
            for (int i = 0; i < repeatCount; i++)
                sb.Append(source);
            string expected = sb.ToString();

            var actual = source.Repeat(repeatCount);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Overload with separator

        [Fact]
        public void ActAsIfWithoutSeparatorIfSeparatorIsEmpty()
        {
            string separator = string.Empty;

            int repeatCount = 25.GetRandom(10);
            int sourceLength = 99.GetRandom(5);
            string source = string.Empty.GetRandom(sourceLength);

            var expected = source.Repeat(repeatCount);
            var actual = source.Repeat(repeatCount, separator);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ActAsIfWithoutSeparatorIfSeparatorIsNull()
        {
            string separator = null;

            int repeatCount = 25.GetRandom(10);
            int sourceLength = 99.GetRandom(5);
            string source = string.Empty.GetRandom(sourceLength);

            var expected = source.Repeat(repeatCount);
            var actual = source.Repeat(repeatCount, separator);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnAnNMinusOneCharacterStringIfTheValueIsEmptyAndTheSeparatorIsOneCharacter()
        {
            int repeatCount = 99.GetRandom();
            int expectedLength = repeatCount - 1;
            string value = string.Empty;
            string separator = string.Empty.GetRandom(1);

            string actual = value.Repeat(repeatCount, separator);

            string message = $"Expected Len: {expectedLength} - Actual Len: {actual.Length}";
            Assert.True(actual.Length == expectedLength, message);
        }

        [Fact]
        public void ReturnTheProperLengthStringIfAOneCharacterStringAndAOneCharacterSeparatorIsSupplied()
        {
            int repeatCount = 99.GetRandom(10);
            string value = string.Empty.GetRandom(1);
            string separator = string.Empty.GetRandom(1);
            int expectedLength = repeatCount + (repeatCount - 1);

            var actual = value.Repeat(repeatCount, separator);

            string message = $"Expected Len: {expectedLength} - Actual Len: {actual.Length}";
            Assert.True(actual.Length == expectedLength, message);
        }

        [Fact]
        public void ReturnTheProperLengthStringIfATwoCharacterStringAndAOneCharacterSeparatorIsSupplied()
        {
            int repeatCount = 99.GetRandom(10);
            string value = string.Empty.GetRandom(2);
            string separator = string.Empty.GetRandom(1);
            int expectedLength = (repeatCount * 3) - 1;

            var actual = value.Repeat(repeatCount, separator);

            string message = $"Expected Len: {expectedLength} - Actual Len: {actual.Length}";
            Assert.True(actual.Length == expectedLength, message);
        }

        [Fact]
        public void ReturnTheProperLengthStringForARandomRequestWithSeparator()
        {
            int repeatCount = 99.GetRandom(10);
            int valueLength = 99.GetRandom(5);
            int separatorLength = 25.GetRandom(2);

            string value = string.Empty.GetRandom(valueLength);
            string separator = string.Empty.GetRandom(separatorLength);
            int expectedLength = (repeatCount * valueLength) + ((repeatCount - 1) * separatorLength);

            var actual = value.Repeat(repeatCount, separator);

            string message = $"Expected Len: {expectedLength} - Actual Len: {actual.Length}";
            Assert.True(actual.Length == expectedLength, message);
        }

        [Fact]
        public void ReturnTheProperValueWithSeparator()
        {
            string value = "abc";
            string separator = ",";
            string expected = "abc,abc,abc,abc";
            var actual = value.Repeat(4, separator);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnTheProperValueForARandomRequestWithSeparator()
        {
            int repeatCount = 99.GetRandom(10);
            int valueLength = 99.GetRandom(5);
            int separatorLength = 25.GetRandom(2);

            string value = string.Empty.GetRandom(valueLength);
            string separator = string.Empty.GetRandom(separatorLength);

            string expected = string.Empty;
            for (int i = 0; i < repeatCount; i++)
            {
                expected += value;
                if (i < repeatCount - 1)
                    expected += separator;
            }

            var actual = value.Repeat(repeatCount, separator);

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
