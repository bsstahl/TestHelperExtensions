using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class DateTimeExtensions_ToSecondPrecision_Should
    {
        [Fact]
        public void RemoveMillisecondsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddMilliseconds(-target.Millisecond).AddTicks(-target.Ticks % 10000);
            var actual = target.ToSecondPrecision();
            Assert.Equal(expected.Ticks, actual.Ticks);
        }

        [Fact]
        public void RemoveMillisecondsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddMilliseconds(-target.Value.Millisecond).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.ToSecondPrecision();
            Assert.Equal(expected.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void KeepADateTimeUnchangedIfItHasNoMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);
            var actual = target.ToSecondPrecision();
            Assert.Equal(target.Ticks, actual.Ticks);
        }

        [Fact]
        public void KeepANullableDateTimeUnchangedIfItHasNoMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);
            var actual = target.ToSecondPrecision();
            Assert.Equal(target.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.ToSecondPrecision();
            Assert.Null(result);
        }

    }
}
