using System;
using Xunit;
using System.Collections.Generic;
using TestHelperExtensions;

namespace TestHelperExtensions.Test
{
    public class EnumerableExtensions_HasSameValues_Should
    {
        [Fact]
        public void ReturnFalseIfTheSourceListIsNull()
        {
            List<Int32> list1 = null;
            List<Int32> list2 = new List<Int32>();
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfTheTargetListIsNull()
        {
            List<Int32> list1 = new List<Int32>();
            List<Int32> list2 = null;
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfBothListsAreNull()
        {
            List<Int32> list1 = null;
            List<Int32> list2 = null;
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnTrueIfBothCollectionsAreEmpty()
        {
            var list1 = new List<Int32>();
            var list2 = new List<Int32>();
            Assert.True(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnTrueIfBothCollectionsHaveTheSameSingleValue()
        {
            int value = Int32.MaxValue.GetRandom();
            var list1 = new List<Int32>() { value };
            var list2 = new List<Int32>() { value };
            Assert.True(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfTheSourceListHasMoreOfTheSameValue()
        {
            int value = Int32.MaxValue.GetRandom();
            var list1 = new List<Int32>() { value, value };
            var list2 = new List<Int32>() { value };
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfTheTargetListHasMoreOfTheSameValue()
        {
            int value = Int32.MaxValue.GetRandom();
            var list1 = new List<Int32>() { value };
            var list2 = new List<Int32>() { value, value };
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfTheTargetListHasASingleDifferentValue()
        {
            int value = Int32.MaxValue.GetRandom();
            var list1 = new List<Int32>() { Int32.MaxValue.GetRandom() };
            var list2 = new List<Int32>() { Int32.MaxValue.GetRandom() };
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfTheTargetListHasThreeDifferentValues()
        {
            int value = Int32.MaxValue.GetRandom();
            var list1 = new List<Int32>() { Int32.MaxValue.GetRandom(), Int32.MaxValue.GetRandom(), Int32.MaxValue.GetRandom() };
            var list2 = new List<Int32>() { Int32.MaxValue.GetRandom(), Int32.MaxValue.GetRandom(), Int32.MaxValue.GetRandom() };
            Assert.False(list1.HasSameValues(list2));
        }

        [Fact]
        public void ReturnFalseIfTheTargetListHasTheSameCountButDifferentValues()
        {
            int listCount = 99.GetRandom();
            var list1 = new List<Int32>();
            var list2 = new List<Int32>();
            for (int i = 0; i < listCount; i++)
            {
                list1.Add(Int32.MaxValue.GetRandom());
                list2.Add(Int32.MaxValue.GetRandom());
            }
            Assert.False(list1.HasSameValues(list2), $"Count = {list1.Count}");
        }

        [Fact]
        public void ReturnTrueIfBothCollectionsHaveTheSameValuesButInDifferentOrders()
        {
            var list1 = new List<Int32>();
            var list2 = new List<Int32>();

            for (int i = 0; i < 100.GetRandom(20); i++)
            {
                int value1 = Int32.MaxValue.GetRandom();
                int value2 = Int32.MaxValue.GetRandom();
                list1.AddRange(new int[]{ value1, value2 });
                list2.AddRange(new int[] { value2, value1 });
            }

            Assert.True(list1.HasSameValues(list2));
        }

    }
}
