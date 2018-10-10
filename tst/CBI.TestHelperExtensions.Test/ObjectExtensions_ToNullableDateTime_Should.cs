using System;
using Xunit;

namespace TestHelperExtensions.Test
{
    
    public class ObjectExtensions_ToNullableDateTime_Should
    {
        [Fact]
        public void SuccessfullyConvertAStringRepresentedDateToADateType()
        {
            DateTime expected = DateTime.Now;
            object target = expected.ToString();
            var result = target.ToNullableDateTime();
            Assert.Equal(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [Fact]
        public void SuccessfullyConvertADateInAnObjectToADateType()
        {
            DateTime expected = DateTime.Now;
            object target = expected;
            var result = target.ToNullableDateTime();
            Assert.Equal(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [Fact]
        public void ConvertANullObjectToANullDate()
        {
            object target = null;
            var result = target.ToNullableDateTime();
            Assert.False(result.HasValue);
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsNotAValidDate()
        {
            object target = "2015-02-30 18:34:43";
            Assert.Throws<FormatException>(() => target.ToNullableDateTime());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            Assert.Throws<InvalidCastException>(() => target.ToNullableDateTime());
        }

    }
}
