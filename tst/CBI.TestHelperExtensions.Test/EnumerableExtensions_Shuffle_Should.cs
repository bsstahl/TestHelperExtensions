using System;
using Xunit;
using System.Linq;
using TestHelperExtensions;
using TestHelperExtensions.Test.Helpers;
using System.Collections.Generic;

namespace TestHelperExtensions.Test
{
    
    public class EnumerableExtensions_Shuffle_Should
    {
        [Fact]
        public void ThrowAnArgumentNullExceptionIfTheListIsNull()
        {
            List<int> target = null;
            Assert.Throws<ArgumentNullException>(() => target.Shuffle());
        }

        [Fact]
        public void ReturnAnEmptyEnumerableIfTheOriginalListIsEmpty()
        {
            var target = new List<int>();
            var actual = target.Shuffle();
            Assert.Empty(actual);
        }

        [Fact]
        public void ReturnAnEnumerableWithTheSameNumberOfValuesAsTheOriginal()
        {
            int elementCount = Int16.MaxValue.GetRandom();
            var target = Enumerable.Range(1, elementCount);
            var actual = target.Shuffle();
            Assert.Equal(target.Count(), actual.Count());
        }

        [Fact]
        public void ReturnAnEnumerableWithTheCorrectValues()
        {
            var target = new List<int>() { 1, 3, 2, 0, 0, 1, 2, 3 };
            var actual = target.Shuffle();

            var actualCounts = new int[] { 0, 0, 0, 0};
            foreach (var item in actual)
                actualCounts[item]++;

            var failures = actualCounts.Where(c => c != 2);
            Assert.Empty(failures);
        }

        [Fact]
        public void ReturnAnEnumerableWithTheSameValuesAsTheOriginal()
        {
            int elementCount = Byte.MaxValue.GetRandom(10);
            var target = Enumerable.Range(1, elementCount);
            var actual = target.Shuffle();
            Assert.True(target.HasSameValues(actual));
        }

        [Fact]
        public void ReturnAnEnumerableInADifferentOrderForNonTrivialElementCounts()
        {
            int elementCount = Byte.MaxValue.GetRandom(255);
            var target = Enumerable.Range(1, elementCount).ToArray();
            var actual = target.Shuffle().ToArray();

            bool orderIsDifferent = false;
            int i = 0;
            while ((i < elementCount) && !orderIsDifferent)
            {
                orderIsDifferent = (target[i] != actual[i]);
                i++;
            }

            Assert.True(orderIsDifferent);
        }
    }
}
