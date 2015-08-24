using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a random date/time value earlier than the value specified 
        /// </summary>
        /// <param name="maxValue">The date/time that the random value should fall before.</param>
        /// <returns>A DateTime structure representing a random moment in time</returns>
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
        public static DateTime GetRandom(this DateTime maxValue, DateTime minValue)
        {
            long ticks = maxValue.Ticks.GetRandom(minValue.Ticks);
            return new DateTime(ticks);
        }

        public static DateTime ToMinutePrecision(this DateTime myDate)
        {
            return new DateTime(myDate.Year, myDate.Month, myDate.Day, myDate.Hour, myDate.Minute, 0);
        }

        public static DateTime? ToMinutePrecision(this DateTime? myDate)
        {
            if (myDate.HasValue)
                return myDate.Value.ToMinutePrecision();
            else
                return null;
        }

        public static DateTime ToSecondPrecision(this DateTime myDate)
        {
            return new DateTime(myDate.Year, myDate.Month, myDate.Day, myDate.Hour, myDate.Minute, myDate.Second);
        }

        public static DateTime? ToSecondPrecision(this DateTime? myDate)
        {
            if (myDate.HasValue)
                return myDate.Value.ToSecondPrecision();
            else
                return null;
        }

        public static DateTime To10MSPrecision(this DateTime value)
        {
            return DateTime.Parse(value.ToString("MM/dd/yyyy HH:mm:ss.FF", System.Globalization.CultureInfo.InvariantCulture), System.Globalization.CultureInfo.InvariantCulture);
        }

        public static DateTime? To10MSPrecision(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.To10MSPrecision();
            else
                return null;
        }

        public static DateTime To100MSPrecision(this DateTime value)
        {
            return DateTime.Parse(value.ToString("MM/dd/yyyy HH:mm:ss.F", System.Globalization.CultureInfo.InvariantCulture), System.Globalization.CultureInfo.InvariantCulture);
        }

        public static DateTime? To100MSPrecision(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.To100MSPrecision();
            else
                return null;
        }

        public static string ToOracleDate(this DateTime myDate)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "TO_DATE('{0:MM/dd/yyyy HH:mm:ss}','MM/DD/YYYY HH24:MI:SS')", myDate);
        }

        public static string ToOracleDate(this DateTime? myDate)
        {
            if (myDate.HasValue)
                return myDate.Value.ToOracleDate();
            else
                return "null";
        }

        public static bool EqualWithinTolerance(this DateTime objectUnderTest, DateTime comparisonValue, TimeSpan tolerance)
        {
            TimeSpan duration = objectUnderTest.Subtract(comparisonValue).Duration();
            int comparisonResult = duration.CompareTo(tolerance);
            return (comparisonResult <= 0);
        }

    }
}
