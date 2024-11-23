using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensions_RegexMatch_Should
    {
        const string _validEmail = "thisemail@domain.com";
        const string _invalidEmail = "NoAnEmail.com";

        const string _validPhone = "555-567-8989";
        const string _invalidPhone = "77-222-7394";

        const string _emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        const string _phonePattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";

        [Fact]
        public void ReturnFalseWhenAnEmptyTargetIsPassedRegardlessOfPattern()
        {
            Assert.False(string.Empty.RegexMatch(_emailPattern));
        }

        [Fact]
        public void ThrowExceptionWhenANullTargetIsPassed()
        {
            string target = null;
            Assert.Throws<ArgumentNullException>(() => target.RegexMatch(_emailPattern));
        }

        [Fact]
        public void ReturnTrueWhenAnEmptyPatternIsPassed()
        {
            Assert.True(_validEmail.RegexMatch(string.Empty));
        }

        [Fact]
        public void ThrowExceptionWhenANullPatternIsPassed()
        {
            string pattern = null;
            Assert.Throws<ArgumentNullException>(() => _validEmail.RegexMatch(pattern));
        }

        [Fact]
        public void ReturnTrueWhenAValidEmailAddressTestedWithAnEmailPattern()
        {
            Assert.True(_validEmail.RegexMatch(_emailPattern));
        }

        [Fact]
        public void ReturnFalseWhenAnInvalidEmailAddressTestedWithAnEmailPattern()
        {
            Assert.False(_invalidEmail.RegexMatch(_emailPattern));
        }

        [Fact]
        public void ReturnTrueWhenAValidPhoneTestedWithAPhonePattern()
        {
            Assert.True(_validPhone.RegexMatch(_phonePattern));
        }

        [Fact]
        public void ReturnFalseWhenAnInvalidPhoneTestedWithAPhonePattern()
        {
            Assert.False(_invalidPhone.RegexMatch(_phonePattern));
        }


    }
}
