using System;
using Xunit;
using TestHelperExtensions.Test.Helpers;

namespace TestHelperExtensions.Test
{

    public class SingleExtensions_GetRandom_Should
    {
        const int _executionCount = 500;

        #region Rules Tests

        // Rules tests are the preferred types of unit tests since they
        // test those things that the customers care about. However, they
        // can sometimes be incomplete, or extremely difficult to make 
        // comprehensive.  In this example, we can easily check the
        // boundary rules, but would have a very difficult time proving
        // that we actually called the random number generator properly.
        // For example, if we were off by one in our calls to the generator
        // such that we never reached our bounds, but were always
        // at least 1 away, these tests might not identify that situation.

        [Fact]
        public void AlwaysBeAboveOrEqualToTheLowerBound()
        {
            var rnd = Randomizer.Create();

            float lowerBound = Convert.ToSingle(rnd.Next(Int32.MaxValue) + Math.Round(rnd.NextDouble(), 4));
            var delta = Convert.ToSingle(2f * rnd.Next(Int32.MaxValue) + Math.Round(rnd.NextDouble(), 4));
            float upperBound = lowerBound + delta;
            Console.WriteLine("LowerBound={0} UpperBound={1}", lowerBound, upperBound);

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("Actual={0}", actual);
                Assert.True(actual >= lowerBound, message);
            }
        }

        [Fact]
        public void NotReachTheUpperBound()
        {
            var rnd = Randomizer.Create();

            float upperBound = Convert.ToSingle(Int32.MaxValue) + Convert.ToSingle(rnd.Next(Int32.MaxValue) + rnd.NextDouble());
            float lowerBound = upperBound - Convert.ToSingle(rnd.Next(Int32.MaxValue) - rnd.NextDouble());

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("LowerBound={0}, UpperBound={1}, Actual={2}", lowerBound, upperBound, actual);
                Assert.True(actual < upperBound, message);
            }
        }

        [Fact]
        public void NotReachTheUpperBoundIfAValueGreaterThanALongIsSpecified()
        {
            var rnd = Randomizer.Create();

            float upperBound = Convert.ToSingle(Math.Round(Int64.MaxValue + Convert.ToSingle(rnd.Next(Int16.MaxValue) + rnd.NextDouble()), 4));
            float lowerBound = 0.0f;

            for (int i = 0; i < _executionCount; i++)
            {
                var actual = upperBound.GetRandom(lowerBound);
                string message = string.Format("LowerBound={0}, UpperBound={1}, Actual={2}", lowerBound, upperBound, actual);
                Assert.True(actual < upperBound, message);
            }
        }

        [Fact]
        public void ThrowExceptionIfLowerBoundIsNotBelowTheUpperBound()
        {
            var rnd = Randomizer.Create();
            float upperBound = Convert.ToSingle(Int32.MaxValue) + Convert.ToSingle(rnd.Next(Int16.MaxValue) + rnd.NextDouble());
            float lowerBound = upperBound + Convert.ToSingle(rnd.Next(Int32.MaxValue) - rnd.NextDouble());
            Assert.Throws<ArgumentOutOfRangeException>(() => upperBound.GetRandom(lowerBound));
        }

        [Fact]
        public void NotFailIfTheLowerBoundIsCloseToTheUpperBoundButStillLower()
        {
            float upperBound = 2.5f;
            float lowerBound = 0.5f;
            var result = upperBound.GetRandom(lowerBound);
        }

        [Fact]
        public void NotFailIfAllInt64ValuesAreInTheRange()
        {
            float lowerBound = Int64.MinValue;
            float upperBound = Int64.MaxValue;
            var result = upperBound.GetRandom(lowerBound);
        }

        [Fact]
        public void NotFailIfAllPositiveInt64ValuesAreInTheRange()
        {
            float lowerBound = 0;
            float upperBound = Int64.MaxValue;
            var result = upperBound.GetRandom(lowerBound);
        }

        [Fact]
        public void SpanTheFullRangeOfValuesIfTheRangeIsLessThanOne()
        {
            float upperBound = 2.9f;
            float lowerBound = 2.1f;

            float minValue = upperBound;
            float maxValue = lowerBound;

            for (int i = 0; i < _executionCount; i++)
            {
                var result = upperBound.GetRandom(lowerBound);
                if (result < minValue)
                    minValue = result;
                if (result > maxValue)
                    maxValue = result;
            }

            Assert.Equal(Math.Round(lowerBound, 2), Math.Round(minValue, 2));
            Assert.Equal(Math.Round(upperBound, 2), Math.Round(maxValue, 2));
        }

        [Fact]
        public void SpanTheFullRangeOfValuesIfAllInt64ValuesAreInRange()
        {
            float tolerance = 0.1f;

            float upperBound = Int64.MaxValue;
            float lowerBound = Int64.MinValue;

            float minValue = upperBound;
            float maxValue = lowerBound;

            float slop = Convert.ToSingle(Int64.MaxValue * tolerance * 2.0);
            float maxLowestValue = lowerBound + slop;
            float minHighestValue = upperBound - slop;

            for (int i = 0; i < _executionCount; i++)
            {
                var result = upperBound.GetRandom(lowerBound);
                if (result < minValue)
                    minValue = result;
                if (result > maxValue)
                    maxValue = result;
            }

            string message = $"minValue:{minValue}, maxValue:{maxValue}, lowerBound:{lowerBound}, upperBound:{upperBound}, maxLowestValue:{maxLowestValue}, minHighestValue:{minHighestValue}";
            Assert.True(minValue < maxLowestValue, message);
            Assert.True(maxValue > minHighestValue, message);
        }

        #endregion

        #region Sanity Tests

        // Not for testing randomness, just to make sure things look reasonable. 
        // I feel no need to test the implementation of Microsoft's random number generator.

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRange()
        {
            const float tolerance = .1f;
            var rnd = Randomizer.Create();

            float upperBound = Convert.ToSingle(Int16.MaxValue + rnd.Next(Int16.MaxValue) + rnd.NextDouble());
            float range = rnd.Next(Int16.MaxValue) + rnd.Next();
            float lowerBound = upperBound - range;

            ValidateAverageResultNearTheMiddleOfTheRange(upperBound, lowerBound, tolerance);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequest()
        {
            const float tolerance = .1f;
            var rnd = Randomizer.Create();

            float upperBound = Convert.ToSingle(Int16.MaxValue) + Convert.ToSingle(rnd.Next(Int16.MaxValue) + rnd.NextDouble());
            float lowerBound = upperBound - Convert.ToSingle(2 * rnd.Next(Int16.MaxValue) - rnd.NextDouble());

            ValidateResultsAcrossTheEntireRangeOfTheRequest(lowerBound, upperBound, tolerance);
        }

        [Fact]
        public void HaveAnAverageResultNearTheMiddleOfTheRangeForASmallRange()
        {
            const float tolerance = .02f;
            var rnd = Randomizer.Create();

            float upperBound = Convert.ToSingle(rnd.NextDouble() * rnd.Next(byte.MaxValue));
            float lowerBound = upperBound - Convert.ToSingle(rnd.NextDouble());

            ValidateAverageResultNearTheMiddleOfTheRange(upperBound, lowerBound, tolerance);
        }

        [Fact]
        public void GetResultsAcrossTheEntireRangeOfTheRequestForASmallRange()
        {
            const float tolerance = .1f;
            var rnd = Randomizer.Create();

            float upperBound = Convert.ToSingle(rnd.NextDouble() * rnd.Next(byte.MaxValue));
            float lowerBound = upperBound - Convert.ToSingle(rnd.NextDouble());

            ValidateResultsAcrossTheEntireRangeOfTheRequest(lowerBound, upperBound, tolerance);
        }

        #endregion


        #region Helper Methods

        private static void ValidateAverageResultNearTheMiddleOfTheRange(float upperBound, float lowerBound, float tolerance)
        {
            var range = upperBound - lowerBound;
            var expectedMean = (range / 2) + lowerBound;
            var slop = range * tolerance;
            var minMean = expectedMean - slop;
            var maxMean = expectedMean + slop;

            float sum = 0.0f;
            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                sum += value;
            }

            var actualMean = sum / _executionCount;

            string message = string.Format("mean:{0} min allowed:{1} max allowed:{2} lower bound:{3} upper bound:{4}", actualMean, minMean, maxMean, lowerBound, upperBound);
            Assert.True(actualMean > minMean, message);
            Assert.True(actualMean < maxMean, message);
        }

        private static void ValidateResultsAcrossTheEntireRangeOfTheRequest(float lowerBound, float upperBound, float tolerance)
        {
            float expectedRange = upperBound - lowerBound;
            var slop = Convert.ToSingle(expectedRange * tolerance);

            var maxLowValue = lowerBound + slop;
            var minHighValue = upperBound - slop;

            var minValue = upperBound;
            var maxValue = lowerBound;

            for (int i = 0; i < _executionCount; i++)
            {
                var value = upperBound.GetRandom(lowerBound);
                if (value < minValue) minValue = value;
                if (value > maxValue) maxValue = value;
            }

            string message = string.Format("minValue:{0}, maxValue:{1}, maxLowValue:{2}, minHighValue:{3}, lowerBound:{4}, upperBound:{5}", minValue, maxValue, maxLowValue, minHighValue, lowerBound, upperBound);
            Assert.True(minValue < maxLowValue, message);
            Assert.True(maxValue > minHighValue, message);
        }

        #endregion
    }
}
