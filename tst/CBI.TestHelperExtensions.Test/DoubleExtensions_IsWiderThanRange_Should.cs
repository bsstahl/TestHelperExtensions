using System;
using System.Linq;
using Xunit;
using TestHelperExtensions.Test.Helpers;
using System.Text;

namespace TestHelperExtensions.Test
{

    public class DoubleExtensions_IsWiderThanRange_Should
    {
        [Fact]
        public void ReturnsTrueForAVerySmallRangeAndAWiderSetOfValues()
        {
            var rnd = Randomizer.Create();

            var range = rnd.NextDouble();
            double upperBound = ((double)(Int16.MaxValue)).GetRandom();
            double lowerBound = upperBound - (5.GetRandom(2) * range);

            var actual = range.IsWiderThanRange(lowerBound, upperBound);
            string message = $"Range: {range}, LowerBound: {lowerBound}, UpperBound: {upperBound}";

            Assert.True(actual, message);
        }

        [Fact]
        public void ReturnsFalseForASmallRangeButANarrowerSetOfValues()
        {
            var rnd = Randomizer.Create();

            var range = rnd.NextDouble() * 10.GetRandom(5);
            double upperBound = ((double)(Int16.MaxValue)).GetRandom();
            double lowerBound = upperBound - (rnd.NextDouble() * range);

            var actual = range.IsWiderThanRange(lowerBound, upperBound);
            string message = $"Range: {range}, LowerBound: {lowerBound}, UpperBound: {upperBound}";

            Assert.False(actual, message);
        }

        [Fact]
        public void ReturnsTrueForAVeryLargeRangeButAWiderSetOfValues()
        {
            var rnd = Randomizer.Create();

            var range = ((double)(Int16.MaxValue)).GetRandom();
            double upperBound = ((double)(Int16.MaxValue)).GetRandom();
            double lowerBound = upperBound - (5.GetRandom(2) * range);

            var actual = range.IsWiderThanRange(lowerBound, upperBound);
            string message = $"Range: {range}, LowerBound: {lowerBound}, UpperBound: {upperBound}";

            Assert.True(actual, message);
        }

        [Fact]
        public void ReturnsFalseForAVeryLargeRangeAndANarrowerSetOfValues()
        {
            var rnd = Randomizer.Create();

            var range = ((double)(Int16.MaxValue)).GetRandom();
            double upperBound = ((double)(Int16.MaxValue)).GetRandom();
            double lowerBound = upperBound - (rnd.NextDouble() * range);

            var actual = range.IsWiderThanRange(lowerBound, upperBound);
            string message = $"Range: {range}, LowerBound: {lowerBound}, UpperBound: {upperBound}";

            Assert.False(actual, message);
        }

    }
}
