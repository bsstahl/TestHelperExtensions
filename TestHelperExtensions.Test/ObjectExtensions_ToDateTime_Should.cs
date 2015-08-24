using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class ObjectExtensions_ToDateTime_Should
    {
        [TestMethod]
        public void SuccessfullyConvertAStringRepresentedDateToADateType()
        {
            DateTime expected = DateTime.Now;
            object target = expected.ToString();
            var result = target.ToDateTime();
            Assert.AreEqual(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [TestMethod]
        public void ReturnMinDateIfTheValueIsNull()
        {
            DateTime expected = DateTime.MinValue;
            object target = null;
            var result = target.ToDateTime();
            Assert.AreEqual(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [TestMethod]
        public void SuccessfullyConvertADateInAnObjectToADateType()
        {
            DateTime expected = DateTime.Now;
            object target = expected;
            var result = target.ToDateTime();
            Assert.AreEqual(expected.ToSecondPrecision(), result.ToSecondPrecision());
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void ThrowExceptionIfTheValueIsNotAValidDate()
        {
            object target = "2015-02-30 18:34:43";
            var result = target.ToDateTime();
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void ThrowExceptionIfTheValueIsAClassInstance()
        {
            object target = this;
            var result = target.ToDateTime();
        }

    }
}
