using System;
using Xunit;
using System.Linq;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{
    
    public class EnumerableExtensions_GetRandom_Should
    {
        [Fact]
        public void SelectEachItemAtLeastOnceIfRunEnoughTimes()
        {
            const int executionCount = 1000;
            var values = new int[] { 0, 1, 2 };
            var results = new int[] { 0, 0, 0 };
            for (int i = 0; i < executionCount; i++)
            {
                results[values.GetRandom()]++;
            }

            Assert.DoesNotContain(results, r => r == 0);
        }

        [Fact]
        public void SelectAValueType()
        {
            var values = new double[] { 0.5, 1.4, 2.5, 3.1, 4.8, 5.3, 6.0, 7.7 };
            var actual = values.GetRandom();

            Assert.True(actual > 0.0);
            Assert.True(actual < 8.0);
        }

        [Fact]
        public void SelectAReferenceType()
        {
            var values = new TestReferenceType[] { new TestReferenceType(), new TestReferenceType(), new TestReferenceType() };
            var actual = values.GetRandom();
            Assert.IsType<TestReferenceType>(actual);
        }
    }
}
