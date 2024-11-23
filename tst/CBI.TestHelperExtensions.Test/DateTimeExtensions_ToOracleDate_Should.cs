using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class DateTimeExtensions_ToOracleDate_Should
    {
        // TO_DATE('01/01/2010 16:41:57','MM/DD/YYYY HH24:MI:SS')

        [Fact]
        public void ReturnAValidToDateStatement()
        {
            var d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');

            Assert.True(actual.Substring(0, 8).ToUpper() == "TO_DATE(");
            Assert.True(actual[actual.Length - 1] == ')');
            Assert.Equal(4, actual.Count(c => c == '\''));
            Assert.Equal(2, actualHalves.Length);
        }

        [Fact]
        public void ReturnAValidToDateStatementIfANullableDateTimeIsUsed()
        {
            DateTime? d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');

            Assert.True(actual.Substring(0, 8).ToUpper() == "TO_DATE(");
            Assert.True(actual[actual.Length - 1] == ')');
            Assert.Equal(4, actual.Count(c => c == '\''));
            Assert.Equal(2, actualHalves.Length);
        }

        [Fact]
        public void ReturnAStatementContainingAParseableDate()
        {
            var d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);
            Assert.True(DateTime.TryParse(result, out DateTime parsedValue));
        }

        [Fact]
        public void ReturnAStatementContainingAParseableDateIfANullableDateTimeIsUsed()
        {
            DateTime? d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);
            Assert.True(DateTime.TryParse(result, out DateTime parsedValue));
        }

        [Fact]
        public void ReturnAStatementContainingTheDateSpecified()
        {
            var d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);

            var parsedValue = DateTime.Parse(result);
            Assert.Equal(d.ToSecondPrecision(), parsedValue.ToSecondPrecision());
        }

        [Fact]
        public void ReturnAStatementContainingTheDateSpecifiedIfANullableDateTimeUsed()
        {
            DateTime? d = DateTime.UtcNow;
            var actual = d.ToOracleDate().Trim();
            var actualHalves = actual.Split(',');
            var firstHalf = actualHalves[0].Trim();
            var result = firstHalf.Substring(9, firstHalf.Length - 10);

            var parsedValue = DateTime.Parse(result);
            Assert.Equal(d.ToSecondPrecision(), parsedValue.ToSecondPrecision());
        }

        [Fact]
        public void ReturnNullIfANullValueIsSpecified()
        {
            DateTime? d = null;
            var actual = d.ToOracleDate().Trim();
            Assert.Equal("null".ToUpper(), actual.ToUpper());
        }
    }
}
