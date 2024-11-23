using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensions_GetRandomUSAddress_Should
    {
        const string addressPattern = @"^\d+\s[A-Za-z0-9\s.,'-]+(?:\s(?:Apt|Apartment|Suite|Ste|Unit|#)\s?[A-Za-z0-9-]*)?$";

        [Fact]
        public void ReturnAnAddressThatMatchesThePattern()
        {
            for (int i = 0; i < 1000; i++)
            {
                var target = string.Empty.GetRandomUSAddress();
                Assert.True(target.RegexMatch(addressPattern));
            }
        }

    }

}
