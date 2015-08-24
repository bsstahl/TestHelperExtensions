using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class StringExtensions_GetRandomUSPhoneNumber_Should
    {
        const string phonePattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";

        public TestContext TestContext { get; set; }


        [TestMethod]
        public void ReturnAValidEmailAddress()
        {
            for (int i = 0; i < 1000; i++)
            {
                var target = string.Empty.GetRandomUSPhoneNumber();
                Assert.IsTrue(target.RegexMatch(phonePattern));
            }
        }

    }

}
