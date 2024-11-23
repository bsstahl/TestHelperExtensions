using System;
using System.Diagnostics.CodeAnalysis;

namespace TestHelperExtensions.Test.Helpers
{
    [ExcludeFromCodeCoverage]
    public class TestException:Exception
    {
        public TestException():base("A TestException was thrown")
        { }

        public TestException(string message) : base(message)
        { }
    }
}
