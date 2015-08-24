using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelperExtensions
{
    public static class StringArrayExtensions
    {

        public static bool Contains(this string[] dataArray, string key)
        {
            return dataArray.Contains(key, StringComparison.CurrentCulture);
        }

        public static bool Contains(this string[] dataArray, string key, StringComparison comparisonType)
        {
            bool result = false;
            if (dataArray != null)
            {
                foreach (string item in dataArray)
                {
                    if (item.Equals(key, comparisonType))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

    }
}
