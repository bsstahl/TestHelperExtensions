using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_To100MSPrecision_Should
    {
        [TestMethod]
        public void RemoveTenMillisecondsIncrementsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddMilliseconds(-target.Millisecond % 100).AddTicks(-target.Ticks % 10000);
            var actual = target.To100MSPrecision();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void RemoveTenMillisecondsIncrementsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddMilliseconds(-target.Value.Millisecond % 100).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.To100MSPrecision();
            Assert.AreEqual(expected.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void KeepADateTimeUnchangedIfItHasNoTenMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(600);
            var actual = target.To100MSPrecision();
            Assert.AreEqual(target.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void KeepANullableDateTimeUnchangedIfItHasNoTenthsOfMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(400);
            var actual = target.To100MSPrecision();
            Assert.AreEqual(target.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.To100MSPrecision();
            Assert.IsNull(result);
        }

    }
}
