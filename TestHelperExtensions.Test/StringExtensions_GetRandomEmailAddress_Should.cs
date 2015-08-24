using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class StringExtensions_GetRandomEmailAddress_Should
    {
        const string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public TestContext TestContext { get; set; }


        [TestMethod]
        public void ReturnAValidEmailAddress()
        {
            for (int i = 0; i < 1000; i++)
            {
                var email = string.Empty.GetRandomEmailAddress();
                Assert.IsTrue(email.RegexMatch(emailPattern));
            }
        }

    }

}
