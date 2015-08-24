using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_To10MSPrecision_Should
    {
        [TestMethod]
        public void RemoveIndividualMillisecondsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddMilliseconds(-target.Millisecond % 10).AddTicks(-target.Ticks % 10000);
            var actual = target.To10MSPrecision();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void RemoveIndividualMillisecondsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddMilliseconds(-target.Value.Millisecond % 10).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.To10MSPrecision();
            Assert.AreEqual(expected.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void KeepADateTimeUnchangedIfItHasNoIndividualMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(790);
            var actual = target.To10MSPrecision();
            Assert.AreEqual(target.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void KeepANullableDateTimeUnchangedIfItHasNoIndividualMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(970);
            var actual = target.To10MSPrecision();
            Assert.AreEqual(target.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.To10MSPrecision();
            Assert.IsNull(result);
        }

    }
}
