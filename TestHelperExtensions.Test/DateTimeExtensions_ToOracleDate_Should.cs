using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class DateTimeExtensions_ToOracleDate_Should
    {
        // TO_DATE('01/01/2010 16:41:57','MM/DD/YYYY HH24:MI:SS')

        [TestMethod]
        public void ReturnAValidToDateStatement()
        {
            var d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');

            Assert.IsTrue(actual.Substring(0, 8).ToUpper() == "TO_DATE(");
            Assert.IsTrue(actual[actual.Length - 1] == ')');
            Assert.AreEqual(4, actual.Count(c => c == '\''));
            Assert.AreEqual(2, actualHalves.Length);
        }

        [TestMethod]
        public void ReturnAValidToDateStatementIfANullableDateTimeIsUsed()
        {
            DateTime? d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');

            Assert.IsTrue(actual.Substring(0, 8).ToUpper() == "TO_DATE(");
            Assert.IsTrue(actual[actual.Length - 1] == ')');
            Assert.AreEqual(4, actual.Count(c => c == '\''));
            Assert.AreEqual(2, actualHalves.Length);
        }

        [TestMethod]
        public void ReturnAStatementContainingAParseableDate()
        {
            var d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);

            DateTime parsedValue;
            Assert.IsTrue(DateTime.TryParse(result, out parsedValue));
        }

        [TestMethod]
        public void ReturnAStatementContainingAParseableDateIfANullableDateTimeIsUsed()
        {
            DateTime? d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);

            DateTime parsedValue;
            Assert.IsTrue(DateTime.TryParse(result, out parsedValue));
        }

        [TestMethod]
        public void ReturnAStatementContainingTheDateSpecified()
        {
            var d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);

            var parsedValue = DateTime.Parse(result);
            Assert.AreEqual(d.ToSecondPrecision(), parsedValue.ToSecondPrecision());
        }

        [TestMethod]
        public void ReturnAStatementContainingTheDateSpecifiedIfANullableDateTimeUsed()
        {
            DateTime? d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);

            var parsedValue = DateTime.Parse(result);
            Assert.AreEqual(d.ToSecondPrecision(), parsedValue.ToSecondPrecision());
        }

        [TestMethod]
        public void ReturnNullIfANullValueIsSpecified()
        {
            DateTime? d = null;
            var actual = d.ToOracleDate().Trim();
            Assert.AreEqual("null".ToUpper(), actual.ToUpper());
        }
    }
}
