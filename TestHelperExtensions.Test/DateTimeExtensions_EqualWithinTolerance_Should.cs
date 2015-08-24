using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_EqualWithinTolerance_Should
    {
        #region Minute Tolerance

        [TestMethod]
        public void MatchADateLessThan1MinuteAheadIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(59);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.IsTrue(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [TestMethod]
        public void MatchADateLessThan1MinuteBehindIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(-59);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.IsTrue(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [TestMethod]
        public void NotMatchADateMoreThan1MinuteAheadIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(61);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.IsFalse(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [TestMethod]
        public void NotMatchADateMoreThan1MinuteBehindIfA1MinuteToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddSeconds(-61);
            var tolerance = TimeSpan.FromMinutes(1);

            Assert.IsFalse(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        #endregion

        #region 2 Hour Tolerance

        [TestMethod]
        public void MatchADateLessThan2HoursAheadIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(119);
            var tolerance = TimeSpan.FromHours(2);

            Assert.IsTrue(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [TestMethod]
        public void MatchADateLessThan2HourBehindIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(-119);
            var tolerance = TimeSpan.FromHours(2);

            Assert.IsTrue(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [TestMethod]
        public void NotMatchADateMoreThan2HourAheadIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(121);
            var tolerance = TimeSpan.FromHours(2);

            Assert.IsFalse(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        [TestMethod]
        public void NotMatchADateMoreThan2HourBehindIfA2HourToleranceSpecified()
        {
            var target = DateTime.UtcNow;
            var compareToDate = target.AddMinutes(-121);
            var tolerance = TimeSpan.FromHours(2);

            Assert.IsFalse(target.EqualWithinTolerance(compareToDate, tolerance));
        }

        #endregion
    }
}
