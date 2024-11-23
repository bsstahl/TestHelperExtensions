using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class DateTimeExtensions_ToMinutePrecision_Should
    {
        [Fact]
        public void RemoveSecondsAndMillisecondsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddSeconds(-target.Second).AddMilliseconds(-target.Millisecond).AddTicks(-target.Ticks % 10000);
            var actual = target.ToMinutePrecision();
            Assert.Equal(expected.Ticks, actual.Ticks);
        }

        [Fact]
        public void RemoveSecondsAndMillisecondsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddSeconds(-target.Value.Second).AddMilliseconds(-target.Value.Millisecond).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.ToMinutePrecision();
            Assert.Equal(expected.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void KeepADateTimeUnchangedIfItHasNoSecondAndMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, 0);
            var actual = target.ToMinutePrecision();
            Assert.Equal(target.Ticks, actual.Ticks);
        }

        [Fact]
        public void KeepANullableDateTimeUnchangedIfItHasNoSecondAndMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, 0);
            var actual = target.ToMinutePrecision();
            Assert.Equal(target.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.ToMinutePrecision();
            Assert.Null(result);
        }

    }
}
