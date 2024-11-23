using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class ObjectExtensions_ToNullableInt64_Should
    {
        [Fact]
        public void SuccessfullyConvertAnIntegerValueToAnIntegerType()
        {
            object target = Int64.MaxValue;
            var result = target.ToNullableInt64();
            Assert.Equal(Int64.MaxValue, result);
        }

        [Fact]
        public void ConvertAnNullObjectToANullInteger()
        {
            object target = null;
            var result = target.ToNullableInt64();
            Assert.False(result.HasValue);
        }

        [Fact]
        public void RoundToTheNearestIntegerIfTheValueIsReal()
        {
            object[] data = { 40000000.66667, 40000000.33333, -40000000.66667, -40000000.33333 };

            foreach (var target in data)
            {
                var result = target.ToNullableInt64();
                var expected = Convert.ToInt64(System.Math.Round(Convert.ToDouble(target)));
                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsTooBigForAnInt()
        {
            object target = Convert.ToDouble(Int64.MaxValue) + 100;
            Assert.Throws<OverflowException>(() => target.ToNullableInt64());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsTooSmallForAnInt()
        {
            object target = Convert.ToDouble(Int64.MinValue) - 10000.0;
            Assert.Throws<OverflowException>(() => target.ToNullableInt64());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsNotNumeric()
        {
            object target = "NaN";
            Assert.Throws<FormatException>(() => target.ToNullableInt64());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            Assert.Throws<InvalidCastException>(() => target.ToNullableInt64());
        }

    }
}
