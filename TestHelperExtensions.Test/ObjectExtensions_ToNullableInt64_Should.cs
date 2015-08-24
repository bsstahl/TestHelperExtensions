using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class ObjectExtensions_ToNullableInt64_Should
    {
        [TestMethod]
        public void SuccessfullyConvertAnIntegerValueToAnIntegerType()
        {
            object target = Int64.MaxValue;
            var result = target.ToNullableInt64();
            Assert.AreEqual(Int64.MaxValue, result);
        }

        [TestMethod]
        public void ConvertAnNullObjectToANullInteger()
        {
            object target = null;
            var result = target.ToNullableInt64();
            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void RoundToTheNearestIntegerIfTheValueIsReal()
        {
            object[] data = { 40000000.66667, 40000000.33333, -40000000.66667, -40000000.33333 };

            foreach (var target in data)
            {
                var result = target.ToNullableInt64();
                var expected = Convert.ToInt64(System.Math.Round(Convert.ToDouble(target)));
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void ThrowExceptionIfTheValueIsTooBigForAnInt()
        {
            object target = Convert.ToDouble(Int64.MaxValue) + 100;
            var result = target.ToNullableInt64();
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void ThrowExceptionIfTheValueIsTooSmallForAnInt()
        {
            object target = Convert.ToDouble(Int64.MinValue) - 10000.0;
            var result = target.ToNullableInt64();
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void ThrowExceptionIfTheValueIsNotNumeric()
        {
            object target = "NaN";
            var result = target.ToNullableInt64();
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            var result = target.ToNullableInt64();
        }

    }
}
