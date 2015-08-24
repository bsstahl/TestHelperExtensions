using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestHelperExtensions
{
    public static class StringExtensions
    {
        const int _defaultLength = 8;

        public static string GetRandom(this string value)
        {
            return value.GetRandom(_defaultLength);
        }

        public static string GetRandom(this string value, int length)
        {
            if (length < 1)
                throw new System.ArgumentException("length must be a positive integer", "length");

            string fullString = System.Guid.NewGuid().ToString();
            while (fullString.Length < length)
            {
                fullString += System.Guid.NewGuid().ToString();
            }

            return fullString.Substring(0, length);
        }

        public static string GetRandomUSPhoneNumber(this string value)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}-{1}-{2}", 999.GetRandom(211), 999.GetRandom(111), 9999.GetRandom(1111));
        }

        public static string GetRandomEmailAddress(this string value)
        {
            string[] tlds = new string[] { "com", "net", "org", "int", "edu", "mil", "gov", "me", "ru", "au", "cz", "gb", "hk", "it", "jp" };
            string part1 = string.Empty.GetRandom(8.GetRandom(3));
            string domain = string.Empty.GetRandom(12.GetRandom(3));
            string tld = tlds[15.GetRandom()];
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}@{1}.{2}", part1, domain, tld);
        }

        public static System.IO.Stream ToStream(this string value)
        {
            var buffer = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(buffer);
            writer.Write(value);
            writer.Flush();
            return buffer;
        }


        public static bool RegexMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }



    }
}
