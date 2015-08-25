using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class ObjectExtensions_ToInt64_Should
    {
        [TestMethod]
        public void SuccessfullyConvertAnIntegerValueToAnIntegerType()
        {
            object target = Int64.MaxValue;
            var result = target.ToInt64();
            Assert.AreEqual(Int64.MaxValue, result);
        }

        [TestMethod]
        public void ReturnZeroIfTheValueIsNull()
        {
            object target = null;
            var result = target.ToInt64();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RoundToTheNearestIntegerIfTheValueIsReal()
        {
            object[] data = { 40000000.66667, 40000000.33333, -40000000.66667, -40000000.33333 };

            foreach (var target in data)
            {
                var result = target.ToInt64();
                var expected = Convert.ToInt64(System.Math.Round(Convert.ToDouble(target)));
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void ThrowExceptionIfTheValueIsTooBigForAnInt()
        {
            object target = Convert.ToDouble(Int64.MaxValue) + 100;
            var result = target.ToInt64();
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void ThrowExceptionIfTheValueIsTooSmallForAnInt()
        {
            object target = Convert.ToDouble(Int64.MinValue) - 10000.0;
            var result = target.ToInt64();
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void ThrowExceptionIfTheValueIsNotNumeric()
        {
            object target = "NaN";
            var result = target.ToInt64();
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            var result = target.ToInt64();
        }

    }
}
