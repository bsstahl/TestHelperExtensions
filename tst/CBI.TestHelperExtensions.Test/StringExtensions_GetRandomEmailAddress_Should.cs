﻿using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensions_GetRandomEmailAddress_Should
    {
        const string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";




        [Fact]
        public void ReturnAValidEmailAddress()
        {
            for (int i = 0; i < 1000; i++)
            {
                var email = string.Empty.GetRandomEmailAddress();
                Assert.True(email.RegexMatch(emailPattern));
            }
        }

    }

}
