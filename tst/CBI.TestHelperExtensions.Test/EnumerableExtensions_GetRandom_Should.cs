using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TestHelperExtensions.Test.Helpers;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class EnumerableExtensions_GetRandom_Should
    {
        [Fact]
        public void SelectEachItemAtLeastOnceIfRunEnoughTimes()
        {
            const int maxExecutionCount = 10000;
            var values = new int[] { 0, 1, 2 };
            var results = new int[] { 0, 0, 0 };

            int i = 0;
            while ((i < maxExecutionCount) && (results.Any(r => r == 0)))
            {
                results[values.GetRandom()]++;
                i++;
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
