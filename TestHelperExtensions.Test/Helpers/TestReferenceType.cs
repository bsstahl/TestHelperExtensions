using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelperExtensions.Test.Helpers
{
    public class TestReferenceType
    {
        public Guid Id { get; set; }

        public TestReferenceType()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
