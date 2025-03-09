using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestHelperExtensions
{
    /// <summary>
    /// Adds functionality that is often used for 
    /// unit testing to the string data type
    /// </summary>
    /// <remarks>This library is not intended for use as production code,
    /// but instead is intended to provide functionality in the test
    /// libraries for that production code.</remarks>
    public static class StringExtensions
    {
        const int _defaultLength = 8;
        private static string[] _streetNames = ["Main", "Elm", "Broadway", "Maple", "Oak", "Pine", "Cedar", "Birch", "Walnut", 
            "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "Washington", "Jefferson", "Lincoln", "Madison", 
            "Jackson", "Franklin", "Adams", "Kennedy", "Hamilton", "Grant", "Cleveland" ];
        private static string[] _streetTypes = ["St", "Ave", "Blvd", "Cir", "Ct", "Dr", "Ln", "Pkwy", "Pl", "Rd", "St", "Ter", "Way"];
        private static string[] _unitTypes = ["Apt", "Apartment", "Suite", "Ste", "Unit", "#"];

        private static string[] _alternativeTrue = { "1", "true", "t", "yes", "y", "on" };
        private static string[] _alternativeFalse = { "0", "false", "f", "no", "n", "off" };


        /// <summary>
        /// Returns a random string of 8 characters
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random string value.</returns>
        public static string GetRandom(this string ignored)
        {
            return string.Empty.GetRandom(_defaultLength);
        }

        /// <summary>
        /// Returns a random string of the specified length
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <param name="length">The length of the desired random string.</param>
        /// <returns>The resulting random string value.</returns>
        public static string GetRandom(this string ignored, int length)
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

        /// <summary>
        /// Returns a random string in a format representing a US phone number
        /// </summary>
        /// <param name="ignored">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random US phone number.</returns>
        public static string GetRandomUSPhoneNumber(this string ignored)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}-{1}-{2}", 999.GetRandom(211), 999.GetRandom(111), 9999.GetRandom(1111));
        }

        /// <summary>
        /// Returns a random string in a format representing an email address.
        /// </summary>
        /// <param name="_">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random email address.</returns>
        public static string GetRandomEmailAddress(this string _)
        {
            string[] tlds = new string[] { "com", "net", "org", "int", "edu", "mil", "gov", "me", "ru", "au", "cz", "gb", "hk", "it", "jp" };
            string part1 = string.Empty.GetRandom(8.GetRandom(3));
            string domain = string.Empty.GetRandom(12.GetRandom(3));
            string tld = tlds[15.GetRandom()];
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}@{1}.{2}", part1, domain, tld);
        }

        /// <summary>
        /// Returns a random string in a format representing a Vehicle Identification Number.
        /// </summary>
        /// <param name="_">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random VIN.</returns>
        /// <remarks>Vehicle Identification Numbers (VINs) are 17 characters long and are used to uniquely identify motor vehicles.
        /// This method returns a well-formed VIN, but one that is probably not valid for any particular vehicle type.
        /// </remarks>
        public static string GetRandomVIN(this string _)
        {
            var (prefix, suffix) = GenerateRandomVIN();
            return CalculateFullVin(prefix, suffix);
        }

        /// <summary>
        /// Returns a random string the a format representing a US street address
        /// </summary>
        /// <param name="_">This parameter is used only to determine variable type. By
        /// the time we have reached this method, the type has already been determined by
        /// the runtime and the parameter is no longer needed.</param>
        /// <returns>The resulting random US street address.</returns>
        public static string GetRandomUSAddress(this string _)
        {
            string number = 99999.GetRandom(10).ToString();
            string streetName = _streetNames.GetRandom();
            string streetType = _streetTypes.GetRandom();
            string unitType = _unitTypes.GetRandom();
            string unitNumber = 99.GetRandom(4).ToString();
            return true.GetRandom()
                ? $"{number} {streetName} {streetType}" 
                : $"{number} {streetName} {streetType} {unitType} {unitNumber}";
        }

        /// <summary>
        /// Converts the specified string into a System.IO.Stream type.
        /// </summary>
        /// <param name="value">The string value to be converted into a Stream.</param>
        /// <returns>The resulting System.IO.Stream data type.</returns>
        /// <remarks>System.IO.Stream objects implement IDisposable and therefore must
        /// be disposed of once they are no longer needed to avoid memory leaks.</remarks>
        public static System.IO.Stream ToStream(this string value)
        {
            var buffer = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(buffer);
            writer.Write(value);
            writer.Flush();
            buffer.Position = 0;
            return buffer;
        }

        /// <summary>
        ///  Indicates whether the specified regular expression pattern 
        /// finds a match in the specified input string.
        /// </summary>
        /// <param name="value">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>Returns true if the regular expression finds a match, False if no match is found.</returns>
        public static bool RegexMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// Repeats the specified string a number of times
        /// </summary>
        /// <param name="value">The string to be repeated</param>
        /// <param name="numberOfRepetitions">The number of times to repeat the string</param>
        /// <returns>A string that represents the original string repeated the proper number of times</returns>
        public static string Repeat(this string value, int numberOfRepetitions)
        {
            return value.Repeat(numberOfRepetitions, null);
        }

        /// <summary>
        /// Repeats the specified string a number of times, each
        /// instance separated by the separator string
        /// </summary>
        /// <param name="value">The string to be repeated</param>
        /// <param name="numberOfRepetitions">The number of times to repeat the string</param>
        /// <param name="separator">The string to be placed in between instances of the 
        /// original value in the output</param>
        /// <returns>A string that represents the original string repeated the 
        /// proper number of times, separated by the separator string</returns>
        public static string Repeat(this string value, int numberOfRepetitions, string separator)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var sb = new StringBuilder();
            for (int i = 0; i < numberOfRepetitions; i++)
            {
                sb.Append(value);
                if (i < (numberOfRepetitions - 1))
                    sb.Append(separator);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts the specified string to a boolean value
        /// </summary>
        /// <param name="value">The string to be converted</param>
        /// <returns>True if the value is recognizable as a boolean true value, 
        /// false if it is recognized as a boolean false value.</returns>
        /// <exception cref="ArgumentException">Thrown if the value is not recognizable as a boolean true or false</exception>
        public static bool ToBool(this string value)
        {
            return value.IsValidBool()
                ? value.GetBool() 
                : throw new ArgumentException($"Unable to parse '{value}' to a valid boolean value");
        }

        /// <summary>
        /// Determines if the specified string is a valid boolean value
        /// </summary>
        /// <param name="value">The value being tested</param>
        /// <returns>True if the value is recognizable as a boolean, otherwise false</returns>
        public static bool IsValidBool(this string value)
        {
            var result = !string.IsNullOrWhiteSpace(value) && 
                (bool.TryParse(value, out _) || value.IsAlternativeBool());
            return result;
        }

        private static bool GetBool(this string value)
        {
            return value.IsAlternativeBool() 
                ? value.GetAlternativeBoolValue() 
                : bool.Parse(value);
        }

        private static bool IsAlternativeBool(this string value)
        {
            return _alternativeTrue.Contains(value.ToLower())
                || _alternativeFalse.Contains(value.ToLower());
        }

        private static bool GetAlternativeBoolValue(this string value)
        {
            return value.IsAlternativeBool()
                ? _alternativeTrue.Contains(value.ToLower())
                : throw new ArgumentException($"'{value}' is not recognizable as a boolean value");
        }

        private static (string Prefix, string Suffix) GenerateRandomVIN()
        {
            const string chars = "ABCDEFGHJKLMNPRSTUVWXYZ0123456789";
            Random random = new Random();

            StringBuilder prefix = new StringBuilder(8);

            // Generate WMI (3 characters)
            prefix.Append(chars[random.Next(0, 26)]); // First letter
            prefix.Append(chars[random.Next(0, 26)]); // Second letter
            prefix.Append(chars[random.Next(0, 10)]); // Number

            // Generate VDS (5 characters)
            for (int i = 0; i < 5; i++)
            {
                prefix.Append(chars[random.Next(chars.Length)]);
            }

            // Generate VIS (8 characters)
            StringBuilder suffix = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                suffix.Append(chars[random.Next(chars.Length)]);
            }

            return (prefix.ToString(), suffix.ToString());
        }

        private static string CalculateFullVin(string prefix, string suffix)
        {
            if (prefix.Length != 8 || suffix.Length != 8)
                throw new ArgumentException("Total VIN without check digit must be 16 characters long.");

            string vin = prefix + suffix;

            int[] weights = { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };
            Dictionary<char, int> transliterations = new Dictionary<char, int>
        {
            {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6},
            {'G', 7}, {'H', 8}, {'J', 1}, {'K', 2}, {'L', 3}, {'M', 4},
            {'N', 5}, {'P', 7}, {'R', 9}, {'S', 2}, {'T', 3}, {'U', 4},
            {'V', 5}, {'W', 6}, {'X', 7}, {'Y', 8}, {'Z', 9},
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
            {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9}
        };

            int sum = 0;
            for (int i = 0; i < vin.Length; i++)
            {
                char c = vin[i];
                if (!transliterations.ContainsKey(c))
                    throw new ArgumentException($"Invalid character in VIN: {c}");

                int value = transliterations[c];
                sum += value * weights[i];
            }

            int remainder = sum % 11;
            var checkChar = remainder == 10 ? 'X' : remainder.ToString()[0];

            return $"{prefix}{checkChar}{suffix}";
        }
    }

}