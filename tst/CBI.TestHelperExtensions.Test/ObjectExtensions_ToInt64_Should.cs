using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class ObjectExtensions_ToInt64_Should
    {
        [Fact]
        public void SuccessfullyConvertAnIntegerValueToAnIntegerType()
        {
            object target = Int64.MaxValue;
            var result = target.ToInt64();
            Assert.Equal(Int64.MaxValue, result);
        }

        [Fact]
        public void ReturnZeroIfTheValueIsNull()
        {
            object target = null;
            var result = target.ToInt64();
            Assert.Equal(0, result);
        }

        [Fact]
        public void RoundToTheNearestIntegerIfTheValueIsReal()
        {
            object[] data = { 40000000.66667, 40000000.33333, -40000000.66667, -40000000.33333 };

            foreach (var target in data)
            {
                var result = target.ToInt64();
                var expected = Convert.ToInt64(System.Math.Round(Convert.ToDouble(target)));
                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsTooBigForAnInt()
        {
            object target = Convert.ToDouble(Int64.MaxValue) + 100;
            Assert.Throws<OverflowException>(() => target.ToInt64());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsTooSmallForAnInt()
        {
            object target = Convert.ToDouble(Int64.MinValue) - 10000.0;
            Assert.Throws<OverflowException>(() => target.ToInt64());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsNotNumeric()
        {
            object target = "NaN";
            Assert.Throws<FormatException>(() => target.ToInt64());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            Assert.Throws<InvalidCastException>(() => target.ToInt64());
        }

    }
}
