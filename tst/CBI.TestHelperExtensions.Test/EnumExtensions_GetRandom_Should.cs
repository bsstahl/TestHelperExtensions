using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class EnumExtensions_GetRandom_Should
    {
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3,
            Value4,
            Value5
        }

        [Fact]
        public void ReturnAValidEnumValue()
        {
            var validValues = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>();
            var actual = TestEnum.Value1.GetRandom();
            Assert.Contains(actual, validValues);
        }

        [Fact]
        public void SelectEachValueAtLeastOnceIfRunEnoughTimes()
        {
            const int maxExecutionCount = 10000;
            var validValues = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>().ToArray();
            var results = new int[validValues.Length];

            int i = 0;
            while (i < maxExecutionCount && results.Any(r => r == 0))
            {
                var value = TestEnum.Value1.GetRandom();
                results[Array.IndexOf(validValues, value)]++;
                i++;
            }

            Assert.DoesNotContain(results, r => r == 0);
        }

        [Fact]
        public void WorkFromAnyStartingEnumValue()
        {
            var validValues = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>().ToArray();

            foreach (var startValue in validValues)
            {
                var actual = startValue.GetRandom();
                Assert.Contains(actual, validValues);
            }
        }
    }
}
