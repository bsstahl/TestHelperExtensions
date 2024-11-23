using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class DateTimeExtensions_To10MSPrecision_Should
    {
        [Fact]
        public void RemoveIndividualMillisecondsFromADateTimeValue()
        {
            var target = DateTime.UtcNow;
            var expected = target.AddMilliseconds(-target.Millisecond % 10).AddTicks(-target.Ticks % 10000);
            var actual = target.To10MSPrecision();
            Assert.Equal(expected.Ticks, actual.Ticks);
        }

        [Fact]
        public void RemoveIndividualMillisecondsFromANullableDateTimeValue()
        {
            var n = DateTime.UtcNow;
            DateTime? target = n;
            DateTime? expected = target.Value.AddMilliseconds(-target.Value.Millisecond % 10).AddTicks(-target.Value.Ticks % 10000);
            var actual = target.To10MSPrecision();
            Assert.Equal(expected.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void KeepADateTimeUnchangedIfItHasNoIndividualMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(790);
            var actual = target.To10MSPrecision();
            Assert.Equal(target.Ticks, actual.Ticks);
        }

        [Fact]
        public void KeepANullableDateTimeUnchangedIfItHasNoIndividualMillisecondComponents()
        {
            var n = DateTime.UtcNow;
            DateTime? target = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second).AddMilliseconds(970);
            var actual = target.To10MSPrecision();
            Assert.Equal(target.Value.Ticks, actual.Value.Ticks);
        }

        [Fact]
        public void ReturnANullIfANullableDateTimeHasNoValue()
        {
            DateTime? target = null;
            var result = target.To10MSPrecision();
            Assert.Null(result);
        }

    }
}
