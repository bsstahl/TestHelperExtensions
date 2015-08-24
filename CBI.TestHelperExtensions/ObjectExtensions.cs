using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    public static class ObjectExtensions
    {
        public static int ToInt32(this object value)
        {
            return Convert.ToInt32(value, System.Globalization.CultureInfo.CurrentCulture);
        }

        public static Int64 ToInt64(this object value)
        {
            return Convert.ToInt64(value, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <remarks>Unfortunately, DBNull.Value is apparently not available 
        /// in some of the portable frameworks so it cannot be tested for here.</remarks>
        public static Int64? ToNullableInt64(this object value)
        {
            Int64? result = null;
            if (value != null)
                result = value.ToInt64();
            return result;
        }

        public static DateTime ToDateTime(this object value)
        {
            return Convert.ToDateTime(value, System.Globalization.CultureInfo.CurrentCulture);
        }

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
