using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{

    [ExcludeFromCodeCoverage]
    public class DateTimeExtensions_To100MSPrecision_Should
    {
        [Fact]
        public void RemoveTenMillisecondsIncrementsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddMilliseconds(-target.Millisecond % 100).AddTicks(-target.Ticks % 10000);
            var actual = target.To100MSPrecision();
            Assert.Equal(expected.Ticks, actual.Ticks);
        }

        [Fact]
        public void RemoveTenMillisecondsIncrementsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddMilliseconds(-target.Value.Millisecond % 100).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.To100MSPrecision();
            Assert.Equal(expected.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void KeepADateTimeUnchangedIfItHasNoTenMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(600);
            var actual = target.To100MSPrecision();
            Assert.Equal(target.Ticks, actual.Ticks);
        }

        [Fact]
        public void KeepANullableDateTimeUnchangedIfItHasNoTenthsOfMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(400);
            var actual = target.To100MSPrecision();
            Assert.Equal(target.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.To100MSPrecision();
            Assert.Null(result);
        }

    }
}
