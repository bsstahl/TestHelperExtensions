using System;
using System.Linq;
using Xunit;
using TestHelperExtensions;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{
    public class DoubleExtensions_GetRandomRange_Should
    {
        const int _executionCount = 1000;

        [Fact]
        public void ReturnAFirstValueAlwaysAboveOrEqualToTheLowerBound()
        {
            var random = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(random.Next(Int32.MaxValue))) - random.NextDouble();

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandomRange(lowerBound);
                string message = string.Format("Actual={0}", actual.upperBound);
                Assert.True(actual.lowerBound >= lowerBound, message);
            }
        }

        [Fact]
        public void ReturnASecondValueAlwaysAboveOrEqualToTheLowerBound()
        {
            var random = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - (2 * Convert.ToDouble(random.Next(Int32.MaxValue))) - random.NextDouble();
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandomRange(lowerBound);
                string message = string.Format("Actual={0}", actual.upperBound);
                Assert.True(actual.upperBound >= lowerBound, message);
            }
        }

        [Fact]
        public void ReturnAFirstValueThatDoesNotReachTheUpperBound()
        {
            var random = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - Convert.ToDouble(random.Next(Int32.MaxValue)) - random.NextDouble();

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandomRange(lowerBound);
                string message = string.Format("Actual={0}", actual.lowerBound);
                Assert.True(actual.lowerBound < upperBound, message);
            }
        }

        [Fact]
        public void ReturnASecondValueThatDoesNotReachTheUpperBound()
        {
            var random = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            double lowerBound = upperBound - Convert.ToDouble(random.Next(Int32.MaxValue)) - random.NextDouble();

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandomRange(lowerBound);
                string message = string.Format("Actual={0}", actual.upperBound);
                Assert.True(actual.upperBound < upperBound, message);
            }
        }

        [Fact]
        public void ReturnAFirstValueWithinRangeOfTheUpperBoundIfNoLowerBoundSpecified()
        {
            var random = Randomizer.Create();

            double upperBound = Convert.ToDouble(Int32.MaxValue) + Convert.ToDouble(random.Next(Int32.MaxValue) + random.NextDouble());
            Console.WriteLine("UpperBound={0}", upperBound);
            var minLowerBound = upperBound - DoubleExtensions.RealMaxInt64;

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandomRange();
                string message = string.Format("Actual={0}", actual.lowerBound);
                Assert.True(actual.lowerBound >= minLowerBound, message);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var random = Randomizer.Create();
            double upperBound = Convert.ToDouble(Int64.MaxValue) + Convert.ToDouble(random.Next(Int16.MaxValue) + random.NextDouble());
            double lowerBound = upperBound + Convert.ToDouble(random.Next(Int32.MaxValue)) - random.NextDouble();
            Assert.Throws<ArgumentOutOfRangeException>(() => upperBound.GetRandomRange(lowerBound));
        }

        [Fact]
        public void NotFailIfTheLowerBoundIsCloseToTheUpperBoundButStillLower()
        {
            double upperBound = 2.5;
            double lowerBound = 0.5;
            var result = upperBound.GetRandomRange(lowerBound);
        }

        [Fact]
        public void SpanTheFullRangeOfValuesIfTheRangeIsLessThanOne()
        {
            double upperBound = 2.9;
            double lowerBound = 2.1;

            double maxLowResult = 2.2;
            double minHighResult = 2.8;

            double minValue = upperBound;
            double maxValue = lowerBound;

            for (int i = 0; i < _executionCount; i++)
            {
                var result = upperBound.GetRandomRange(lowerBound);
                if (result.lowerBound < minValue)
                    minValue = result.lowerBound;
                if (result.upperBound > maxValue)
                    maxValue = result.upperBound;
            }

            Assert.True(minValue < maxLowResult);
            Assert.True(maxValue > minHighResult);
        }
    }
}
