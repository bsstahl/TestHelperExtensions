using System;
using Xunit;

namespace TestHelperExtensions.Test
{

    public class ObjectExtensions_ToInt32_Should
    {
        [Fact]
        public void SuccessfullyConvertAnIntegerValueToAnIntegerType()
        {
            object target = Int32.MaxValue;
            var result = target.ToInt32();
            Assert.Equal(Int32.MaxValue, result);
        }

        [Fact]
        public void ReturnZeroIfTheValueIsNull()
        {
            object target = null;
            var result = target.ToInt32();
            Assert.Equal(0, result);
        }

        [Fact]
        public void RoundToTheNearestIntegerIfTheValueIsReal()
        {
            object[] data = { 40000.66667, 40000.33333, -40000.66667, -40000.33333 };

            foreach (var target in data)
            {
                var result = target.ToInt32();
                var expected = Convert.ToInt32(System.Math.Round(Convert.ToDouble(target)));
                Assert.Equal(expected, result);
            }
        }



        [Fact]
        public void ThrowExceptionIfTheValueIsTooBigForAnInt()
        {
            object target = Convert.ToInt64(Int32.MaxValue) + 10;
            Assert.Throws<OverflowException>(() => target.ToInt32());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsTooSmallForAnInt()
        {
            object target = Convert.ToInt64(Int32.MinValue) - 10;
            Assert.Throws<OverflowException>(() => target.ToInt32());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsNotNumeric()
        {
            object target = "NaN";
            Assert.Throws<FormatException>(() => target.ToInt32());
        }

        [Fact]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            Assert.Throws<InvalidCastException>(() => target.ToInt32());
        }

    }
}
