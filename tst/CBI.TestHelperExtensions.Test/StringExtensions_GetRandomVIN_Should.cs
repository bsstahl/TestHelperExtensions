using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensions_GetRandomVIN_Should
    {
        const string vinPattern = @"^[A-HJ-NPR-Z0-9]{8}[0-9X][A-HJ-NPR-Z0-9]{8}$";

        [Fact]
        public void ReturnAVINThatMatchesThePattern()
        {
            for (int i = 0; i < 1000; i++)
            {
                var target = string.Empty.GetRandomVIN();
                Assert.True(target.RegexMatch(vinPattern));
            }
        }

    }

}
