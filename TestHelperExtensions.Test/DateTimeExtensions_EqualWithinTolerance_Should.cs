using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_EqualWithinTolerance_Should
    {
        public TestContext TestContext { get; set; }

        #region Parameterized Test

        [TestMethod]
        [DeploymentItem("TestHelperExtensions.Test\\DateMatchingFromMinutes.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", 
            "|DataDirectory|\\DateMatchingFromMinutes.xml", 
            "Row", DataAccessMethod.Sequential)]
        public void MatchADateIfAppropriate()
        {
            var row = TestContext.DataRow;
            var target = row["TargetDate"].ToDateTime();
            var compareToDate = row["CompareToDate"].ToDateTime();
            var days = row["TimespanDays"].ToInt32();
            var hours = row["TimespanHours"].ToInt32();
            var minutes = row["TimespanMinutes"].ToInt32();
            var seconds = row["TimespanSeconds"].ToInt32();
            var expected = Convert.ToBoolean(row["Result"]);
            var description = row["Description"].ToString();

            var tolerance = new TimeSpan(days, hours, minutes, seconds);
            var failureMessage = string.Format("{0} for {1} compared to {2} and tolerance of {3}.", description, target, compareToDate, tolerance);
            Assert.AreEqual(expected, target.EqualWithinTolerance(compareToDate, tolerance), failureMessage);
        }

        #endregion

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
