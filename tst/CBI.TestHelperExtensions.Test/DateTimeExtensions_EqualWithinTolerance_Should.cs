using System;
using Xunit;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{

    public class DateTimeExtensions_EqualWithinTolerance_Should
    {

        #region Parameterized Test

        [Theory]
        [InlineData("09/24/2015 11:24:05", "09/24/2015 11:25:04", 0, 0, 1, 0, true)] // Matches a date less than 1 minute ahead if a 1 minute tolerance specified
        [InlineData("09/24/2015 12:24:05", "09/24/2015 12:25:06", 0, 0, 1, 0, false)] // Does not match a date more than 1 minute ahead if a 1 minute tolerance specified
        [InlineData("09/24/2015 13:24:05", "09/24/2015 13:23:06", 0, 0, 1, 0, true)] // Matches a date less than 1 minute behind if a 1 minute tolerance specified
        [InlineData("09/24/2015 14:24:05", "09/24/2015 14:23:04", 0, 0, 1, 0, false)] // Does not match a date more than 1 minute behind if a 1 minute tolerance specified
        [InlineData("09/24/2015 15:24:05", "09/24/2015 15:55:06", 0, 1, 0, 0, true)] // Matches a date less than 1 hour ahead if a 1 hour tolerance specified
        [InlineData("09/24/2015 16:24:05", "09/24/2015 15:25:06", 0, 1, 0, 0, true)] // Matches a date less than 1 hour behind if a 1 hour tolerance specified
        public void MatchADateIfAppropriate(string targetDateString, string compareToDateString,
            Int32 days, Int32 hours, Int32 minutes, Int32 seconds, bool expected)
        {
            var targetDate = DateTime.Parse(targetDateString);
            var compareToDate = DateTime.Parse(compareToDateString);
            var tolerance = new TimeSpan(days, hours, minutes, seconds);
            Assert.Equal(expected, targetDate.EqualWithinTolerance(compareToDate, tolerance));
        }

        #endregion

        #region Minute Tolerance

        [Fact]
        public void MatchADateLessThan1MinuteAheadIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(59);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.True(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [Fact]
        public void MatchADateLessThan1MinuteBehindIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(-59);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.True(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [Fact]
        public void NotMatchADateMoreThan1MinuteAheadIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(61);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.False(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [Fact]
        public void NotMatchADateMoreThan1MinuteBehindIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(-61);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.False(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        #endregion

        #region 2 Hour Tolerance

        [Fact]
        public void MatchADateLessThan2HoursAheadIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(119);
            var tolerance = TimeSpan.FromHours(2);

            Assert.True(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [Fact]
        public void MatchADateLessThan2HourBehindIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(-119);
            var tolerance = TimeSpan.FromHours(2);

            Assert.True(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [Fact]
        public void NotMatchADateMoreThan2HourAheadIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(121);
            var tolerance = TimeSpan.FromHours(2);

            Assert.False(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [Fact]
        public void NotMatchADateMoreThan2HourBehindIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(-121);
            var tolerance = TimeSpan.FromHours(2);

            Assert.False(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        #endregion
    }
}
