using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_ToMinutePrecision_Should
    {
        [TestMethod]
        public void RemoveSecondsAndMillisecondsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddSeconds(-target.Second).AddMilliseconds(-target.Millisecond).AddTicks(-target.Ticks % 10000);
            var actual = target.ToMinutePrecision();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void RemoveSecondsAndMillisecondsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddSeconds(-target.Value.Second).AddMilliseconds(-target.Value.Millisecond).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.ToMinutePrecision();
            Assert.AreEqual(expected.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void KeepADateTimeUnchangedIfItHasNoSecondAndMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, 0);
            var actual = target.ToMinutePrecision();
            Assert.AreEqual(target.Ticks, actual.Ticks);
        }

        [TestMethod]
        public void KeepANullableDateTimeUnchangedIfItHasNoSecondAndMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, 0);
            var actual = target.ToMinutePrecision();
            Assert.AreEqual(target.Value.Ticks, actual.Value.Ticks);
        }

        [TestMethod]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.ToMinutePrecision();
            Assert.IsNull(result);
        }

    }
}
