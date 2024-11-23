using System;
using System.Diagnostics.CodeAnalysis;

namespace TestHelperExtensions.Test.Helpers
{
    [ExcludeFromCodeCoverage]
    public class TestReferenceType
    {
        public Guid Id { get; set; }

        public TestReferenceType()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
