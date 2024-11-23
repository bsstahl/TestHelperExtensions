using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class ObjectExtensions_ToDateTime_Should
    {
        [Fact]
        public void SuccessfullyConvertAStringRepresentedDateToADateType()
        {
            DateTime expected = DateTime.Now;
            object target = expected.ToString();
            var result = target.ToDateTime();
            Assert.Equal(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [Fact]
        public void ReturnMinDateIfTheValueIsNull()
        {
            DateTime expected = DateTime.MinValue;
            object target = null;
            var result = target.ToDateTime();
            Assert.Equal(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [Fact]
        public void SuccessfullyConvertADateInAnObjectToADateType()
        {
            DateTime expected = DateTime.Now;
            object target = expected;
            var result = target.ToDateTime();
            Assert.Equal(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsNotAValidDate()
        {
            object target = "2015-02-30 18:34:43";
            Assert.Throws<FormatException>(() => target.ToDateTime());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            Assert.Throws<InvalidCastException>(() => target.ToDateTime());
        }

    }
}
