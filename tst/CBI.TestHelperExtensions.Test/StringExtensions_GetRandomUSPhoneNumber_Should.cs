using System;
using Xunit;

namespace TestHelperExtensions.Test
{
    
    public class StringExtensions_GetRandomUSPhoneNumber_Should
    {
        const string phonePattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";




        [Fact]
        public void ReturnAValidEmailAddress()
        {
            for (int i = 0; i < 1000; i++)
            {
                var target = string.Empty.GetRandomUSPhoneNumber();
                Assert.True(target.RegexMatch(phonePattern));
            }
        }

    }

}
