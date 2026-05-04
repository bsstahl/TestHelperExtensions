using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the DateTime data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a random date/time value earlier than the value specified 
        /// </summary>
        /// <param name="maxValue">The date/time that the random value should fall before.</param>
        /// <returns>A DateTime structure representing a random moment in time</returns>
        /// <example>
        /// <code>
        /// // Get a random DateTime before January 1, 2025
        /// DateTime result = new DateTime(2025, 1, 1).GetRandom();
        /// </code>
        /// </example>
        public static DateTime GetRandom(this DateTime maxValue)
        {
            return maxValue.GetRandom(DateTime.MinValue);
        }

        /// <summary>
        /// Returns a random date/time value between the values specified 
        /// </summary>
        /// <param name="maxValue">The date/time that the random value should fall before.</param>
        /// <param name="minValue">The date/time that the random value should fall after</param>
        /// <returns>A DateTime structure representing a random moment in time</returns>
        /// <example>
        /// <code>
        /// // Get a random DateTime between January 1, 2020 and January 1, 2025
        /// DateTime minDate = new DateTime(2020, 1, 1);
        /// DateTime maxDate = new DateTime(2025, 1, 1);
        /// DateTime result = maxDate.GetRandom(minDate);
        /// </code>
        /// </example>
        public static DateTime GetRandom(this DateTime maxValue, DateTime minValue)
        {
            long ticks = maxValue.Ticks.GetRandom(minValue.Ticks);
            return new DateTime(ticks);
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous minute.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A DateTime value representing the target value modified to be precise only to the minute.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a DateTime to minute precision for comparison
        /// DateTime actual = GetSomeDateTimeFromSystem();
        /// DateTime expected = new DateTime(2024, 6, 15, 10, 30, 0);
        /// Assert.AreEqual(expected, actual.ToMinutePrecision());
        /// </code>
        /// </example>
        public static DateTime ToMinutePrecision(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous minute.  If the nullable value has no value,
        /// a null is returned.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A Nullable DateTime value representing the target value modified to be precise only to the minute.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a nullable DateTime to minute precision; null is passed through unchanged
        /// DateTime? actual = GetNullableDateTimeFromSystem();
        /// DateTime? expected = new DateTime(2024, 6, 15, 10, 30, 0);
        /// Assert.AreEqual(expected, actual.ToMinutePrecision());
        /// </code>
        /// </example>
        public static DateTime? ToMinutePrecision(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.ToMinutePrecision();
            else
                return null;
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous second.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A DateTime value representing the target value modified to be precise only to the second.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a DateTime to second precision for comparison
        /// DateTime actual = GetSomeDateTimeFromSystem();
        /// DateTime expected = new DateTime(2024, 6, 15, 10, 30, 45);
        /// Assert.AreEqual(expected, actual.ToSecondPrecision());
        /// </code>
        /// </example>
        public static DateTime ToSecondPrecision(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous second.  If the nullable value has no value,
        /// a null is returned.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A Nullable DateTime value representing the target value modified to be precise only to the second.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a nullable DateTime to second precision; null is passed through unchanged
        /// DateTime? actual = GetNullableDateTimeFromSystem();
        /// DateTime? expected = new DateTime(2024, 6, 15, 10, 30, 45);
        /// Assert.AreEqual(expected, actual.ToSecondPrecision());
        /// </code>
        /// </example>
        public static DateTime? ToSecondPrecision(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.ToSecondPrecision();
            else
                return null;
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous 10 millisecond increment.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A DateTime value representing the target value modified to be precise only to 10ms.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a DateTime to 10-millisecond precision for comparison
        /// DateTime actual = GetSomeDateTimeFromSystem();
        /// Assert.AreEqual(actual.To10MSPrecision(), storedValue.To10MSPrecision());
        /// </code>
        /// </example>
        public static DateTime To10MSPrecision(this DateTime value)
        {
            return DateTime.Parse(value.ToString("MM/dd/yyyy HH:mm:ss.FF", System.Globalization.CultureInfo.InvariantCulture), System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous 10 millisecond increment.  If the nullable value 
        /// has no value, a null is returned.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A Nullable DateTime value representing the target value modified to be precise only to 10ms.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a nullable DateTime to 10-millisecond precision; null is passed through unchanged
        /// DateTime? actual = GetNullableDateTimeFromSystem();
        /// Assert.AreEqual(actual.To10MSPrecision(), storedValue.To10MSPrecision());
        /// </code>
        /// </example>
        public static DateTime? To10MSPrecision(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.To10MSPrecision();
            else
                return null;
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous 100 millisecond increment.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A DateTime value representing the target value modified to be precise only to 100ms.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a DateTime to 100-millisecond precision for comparison
        /// DateTime actual = GetSomeDateTimeFromSystem();
        /// Assert.AreEqual(actual.To100MSPrecision(), storedValue.To100MSPrecision());
        /// </code>
        /// </example>
        public static DateTime To100MSPrecision(this DateTime value)
        {
            return DateTime.Parse(value.ToString("MM/dd/yyyy HH:mm:ss.F", System.Globalization.CultureInfo.InvariantCulture), System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the specified DateTime value truncating the precision
        /// to the previous 100 millisecond increment.  If the nullable value 
        /// has no value, a null is returned.
        /// </summary>
        /// <param name="value">The DateTime value for which precision should be adjusted.</param>
        /// <returns>A Nullable DateTime value representing the target value modified to be precise only to 100ms.</returns>
        /// <remarks>Is often used to make DateTime comparisons work better
        /// since a difference in precision between 2 DateTime values will 
        /// cause comparisons to fail that should otherwise match.</remarks>
        /// <example>
        /// <code>
        /// // Truncate a nullable DateTime to 100-millisecond precision; null is passed through unchanged
        /// DateTime? actual = GetNullableDateTimeFromSystem();
        /// Assert.AreEqual(actual.To100MSPrecision(), storedValue.To100MSPrecision());
        /// </code>
        /// </example>
        public static DateTime? To100MSPrecision(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.To100MSPrecision();
            else
                return null;
        }

        /// <summary>
        /// Converts the specified DateTime value into a string that PL/SQL interprets
        /// as the DateTime value. This method is often used to create text-based 
        /// sql statements for data tier tests against an Oracle database.
        /// </summary>
        /// <param name="value">The DateTime value for which an Oracle date value should be created.</param>
        /// <returns>A string that can be concatenated into a PL/SQL statement representing the specified date.</returns>
        /// <remarks>Reminder: these methods are not recommended for use in production code, 
        /// just the test of production code.</remarks>
        /// <example>
        /// <code>
        /// // Build a PL/SQL WHERE clause using a DateTime value
        /// DateTime testDate = new DateTime(2024, 3, 15, 8, 0, 0);
        /// string sql = $"SELECT * FROM Orders WHERE OrderDate = {testDate.ToOracleDate()}";
        /// // sql == "SELECT * FROM Orders WHERE OrderDate = TO_DATE('03/15/2024 08:00:00','MM/DD/YYYY HH24:MI:SS')"
        /// </code>
        /// </example>
        public static string ToOracleDate(this DateTime value)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "TO_DATE('{0:MM/dd/yyyy HH:mm:ss}','MM/DD/YYYY HH24:MI:SS')", value);
        }

        /// <summary>
        /// Converts the specified DateTime value into a string that PL/SQL interprets
        /// as the DateTime value. If the nullable type has no value, a string containing
        /// the text "null" is returned. This method is often used to create text-based 
        /// sql statements for data tier tests against an Oracle database.
        /// </summary>
        /// <param name="value">The DateTime value for which an Oracle date value should be created.</param>
        /// <returns>A string that can be concatenated into a PL/SQL statement representing the specified date.</returns>
        /// <remarks>Reminder: these methods are not recommended for use in production code, 
        /// just the test of production code.</remarks>
        /// <example>
        /// <code>
        /// // Build a PL/SQL WHERE clause from a nullable DateTime; outputs "null" when the value is not set
        /// DateTime? testDate = GetNullableDateFromSystem();
        /// string sql = $"SELECT * FROM Orders WHERE ShippedDate = {testDate.ToOracleDate()}";
        /// </code>
        /// </example>
        public static string ToOracleDate(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.ToOracleDate();
            else
                return "null";
        }

        /// <summary>
        /// Determines if 2 DateTime values contain the same Date and Time to within
        /// the specified tolerance. That is, if the tolerance specified is 1 second,
        /// the DateTime values would have to be within 1 second of each other to match.
        /// </summary>
        /// <param name="objectUnderTest">A DateTime representing the primary value being tested.</param>
        /// <param name="comparisonValue">A DateTime representing the value being compared to the primary value.</param>
        /// <param name="tolerance">A TimeSpan representing the maximum discrepancy between 
        /// the 2 values for the DateTime values to be considered a match.</param>
        /// <returns>A boolean value indicating if the 2 values are equal within the specified tolerance.</returns>
        /// <example>
        /// <code>
        /// // Verify that two DateTimes are within 1 second of each other
        /// DateTime actual = GetSomeDateTimeFromSystem();
        /// DateTime expected = DateTime.UtcNow;
        /// Assert.IsTrue(actual.EqualWithinTolerance(expected, TimeSpan.FromSeconds(1)));
        /// </code>
        /// </example>
        public static bool EqualWithinTolerance(this DateTime objectUnderTest, DateTime comparisonValue, TimeSpan tolerance)
        {
            TimeSpan duration = objectUnderTest.Subtract(comparisonValue).Duration();
            int comparisonResult = duration.CompareTo(tolerance);
            return (comparisonResult <= 0);
        }

    }
}
