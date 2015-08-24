using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class StringExtensions_RegexMatch_Should
    {
        const string _validEmail = "thisemail@domain.com";
        const string _invalidEmail = "NoAnEmail.com";

        const string _validPhone = "555-567-8989";
        const string _invalidPhone = "77-222-7394";

        const string _emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        const string _phonePattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";

        [TestMethod]
        public void ReturnFalseWhenAnEmptyTargetIsPassedRegardlessOfPattern()
        {
            Assert.IsFalse(string.Empty.RegexMatch(_emailPattern));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ThrowExceptionWhenANullTargetIsPassed()
        {
            string target = null;
            target.RegexMatch(_emailPattern);
        }

        [TestMethod]
        public void ReturnTrueWhenAnEmptyPatternIsPassed()
        {
            Assert.IsTrue(_validEmail.RegexMatch(string.Empty));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ThrowExceptionWhenANullPatternIsPassed()
        {
            string pattern = null;
            _validEmail.RegexMatch(pattern);
        }

        [TestMethod]
        public void ReturnTrueWhenAValidEmailAddressTestedWithAnEmailPattern()
        {
            Assert.IsTrue(_validEmail.RegexMatch(_emailPattern));
        }

        [TestMethod]
        public void ReturnFalseWhenAnInvalidEmailAddressTestedWithAnEmailPattern()
        {
            Assert.IsFalse(_invalidEmail.RegexMatch(_emailPattern));
        }

        [TestMethod]
        public void ReturnTrueWhenAValidPhoneTestedWithAPhonePattern()
        {
            Assert.IsTrue(_validPhone.RegexMatch(_phonePattern));
        }

        [TestMethod]
        public void ReturnFalseWhenAnInvalidPhoneTestedWithAPhonePattern()
        {
            Assert.IsFalse(_invalidPhone.RegexMatch(_phonePattern));
        }


    }
}
