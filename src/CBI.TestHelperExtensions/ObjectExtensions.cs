using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the Object data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the value of the specified object to a 32-bit signed integer, using
        /// the current culture formatting information.
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <returns>A 32-bit signed integer that is equivalent to value, or zero if value is null.</returns>
        public static int ToInt32(this object value)
        {
            return Convert.ToInt32(value, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the value of the specified object to a 64-bit signed integer, using
        /// the current culture formatting information.
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <returns>A 64-bit signed integer that is equivalent to value, or zero if value is null.</returns>
        public static Int64 ToInt64(this object value)
        {
            return Convert.ToInt64(value, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the value of the specified object to a 64-bit signed integer, using
        /// the current culture formatting information. If the object has no value, a 
        /// null is returned.
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <returns>A nullable 64-bit signed integer that is equivalent to value, or null if value is null.</returns>
        /// <remarks>Unfortunately, it appears that DBNull.Value is not available 
        /// in some of the portable frameworks so it cannot be tested for here.</remarks>
        public static Int64? ToNullableInt64(this object value)
        {
            Int64? result = null;
            if (value != null)
                result = value.ToInt64();
            return result;
        }



        /// <summary>
        /// Converts the value of the specified object to a System.DateTime object, using
        /// the current culture formatting information.
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <returns>
        /// The date and time equivalent of the value of value, or the date and time equivalent
        /// of System.DateTime.MinValue if value is null.
        /// </returns>
        public static DateTime ToDateTime(this object value)
        {
            return Convert.ToDateTime(value, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the value of the specified object to a System.DateTime object, using
        /// the current culture formatting information.
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <returns>
        /// The date and time equivalent of the value of value, or null if value is null.
        /// </returns>
        /// <remarks>Unfortunately, DBNull.Value is apparently not available 
        /// in some of the portable frameworks so it cannot be tested for here.</remarks>
        public static DateTime? ToNullableDateTime(this object value)
        {
            DateTime? result = null;
            if (value != null)
                result = value.ToDateTime();
            return result;
        }

    }
}
