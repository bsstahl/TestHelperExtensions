using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_ToSecondPrecision_Should
    {
        [TestMethod]
        public void RemoveMillisecondsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddMilliseconds(-target.Millisecond).AddTicks(-target.Ticks % 10000);
            var actual = target.ToSecondPrecision();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void RemoveMillisecondsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddMilliseconds(-target.Value.Millisecond).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.ToSecondPrecision();
            Assert.AreEqual(expected.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void KeepADateTimeUnchangedIfItHasNoMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);
            var actual = target.ToSecondPrecision();
            Assert.AreEqual(target.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void KeepANullableDateTimeUnchangedIfItHasNoMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);
            var actual = target.ToSecondPrecision();
            Assert.AreEqual(target.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.ToSecondPrecision();
            Assert.IsNull(result);
        }

    }
}
