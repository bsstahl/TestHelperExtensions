using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class EnumerableExtensions_GetRandom_Should
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void SelectEachItemAtLeastOnceIfRunEnoughTimes()
        {
            const int executionCount = 1000;
            var values = new int[] { 0, 1, 2 };
            var results = new int[] { 0, 0, 0 };
            for (int i = 0; i < executionCount; i++)
            {
                results[values.GetRandom()]++;
            }

            Assert.IsFalse(results.Any(r => r == 0));
        }

        [TestMethod]
        public void SelectAValueType()
        {
            var values = new double[] { 0.5, 1.4, 2.5, 3.1, 4.8, 5.3, 6.0, 7.7 };
            var actual = values.GetRandom();

            Assert.IsTrue(actual > 0.0);
            Assert.IsTrue(actual < 8.0);
        }

        [TestMethod]
        public void SelectAReferenceType()
        {
            var values = new TestReferenceType[] { new TestReferenceType(), new TestReferenceType(), new TestReferenceType() };
            var actual = values.GetRandom();
            Assert.IsInstanceOfType(actual, typeof(TestReferenceType));
        }
    }
}
