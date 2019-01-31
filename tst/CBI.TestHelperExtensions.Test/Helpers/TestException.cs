using System;

namespace TestHelperExtensions.Test.Helpers
{
    public class TestException:Exception
    {
        public TestException():base("A TestException was thrown")
        { }

        public TestException(string message) : base(message)
        { }
    }
}
