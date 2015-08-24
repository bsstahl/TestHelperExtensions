using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class ObjectExtensions_ToInt32_Should
    {
        [TestMethod]
        public void SuccessfullyConvertAnIntegerValueToAnIntegerType()
        {
            object target = Int32.MaxValue;
            var result = target.ToInt32();
            Assert.AreEqual(Int32.MaxValue, result);
        }

        [TestMethod]
        public void ReturnZeroIfTheValueIsNull()
        {
            object target = null;
            var result = target.ToInt32();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RoundToTheNearestIntegerIfTheValueIsReal()
        {
            object[] data = { 40000.66667, 40000.33333, -40000.66667, -40000.33333 };

            foreach (var target in data)
            {
                var result = target.ToInt32();
                var expected = Convert.ToInt32(System.Math.Round(Convert.ToDouble(target)));
                Assert.AreEqual(expected, result);
            }
        }



        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void ThrowExceptionIfTheValueIsTooBigForAnInt()
        {
            object target = Convert.ToInt64(Int32.MaxValue) + 10;
            var result = target.ToInt32();
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void ThrowExceptionIfTheValueIsTooSmallForAnInt()
        {
            object target = Convert.ToInt64(Int32.MinValue) - 10;
            var result = target.ToInt32();
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void ThrowExceptionIfTheValueIsNotNumeric()
        {
            object target = "NaN";
            var result = target.ToInt32();
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            var result = target.ToInt32();
        }

    }
}
