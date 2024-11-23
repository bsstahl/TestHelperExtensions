using System;
using System.Diagnostics.CodeAnalysis;
using TestHelperExtensions.Test.Helpers;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class ExceptionExtensions_ThrowIf_Should
    {
        [Fact]
        public void ThrowAnArgumentNullExceptionIfTheExceptionIsNullAndThePredicateTrue()
        {
            TestException ex = null;
            Assert.Throws<ArgumentNullException>(() => ex.ThrowIf<object>(o => true, null));
        }

        [Fact]
        public void NotThrowAnExceptionIfTheExceptionIsNullButThePredicateFalse()
        {
            TestException ex = null;
            ex.ThrowIf<object>(o => false, null);
        }

        #region Object Type Tests

        [Fact]
        public void ThrowAnExceptionIfThePredicateReturnsTrueForAnObjType()
        {
            var ex = new TestException();
            Assert.Throws<TestException>(() => ex.ThrowIf<object>(o => true, null));
        }

        [Fact]
        public void NotThrowIfThePredicateReturnsFalseForAnObjType()
        {
            var ex = new TestException();
            ex.ThrowIf<object>(o => false, null);
        }

        [Fact]
        public void ThrowTheExceptionIfThePredicateResultsInATrueValueForAnObjType()
        {
            object testVal = new object();
            var ex = new TestException();
            Func<object, bool> predicate = i => i != null;
            Assert.Throws<TestException>(() => ex.ThrowIf(predicate, testVal));
        }

        [Fact]
        public void NotThrowIfThePredicateResultsInAFalseValueForAnObjType()
        {
            object testVal = new object();
            var ex = new TestException();
            Func<object, bool> predicate = i => i == null;
            ex.ThrowIf(predicate, testVal);
        }

        #endregion

        #region Integer Type Tests

        [Fact]
        public void ThrowAnExceptionIfThePredicateReturnsTrueForAnIntType()
        {
            var ex = new TestException();
            Assert.Throws<TestException>(() => ex.ThrowIf<int>(o => true, Int32.MaxValue.GetRandom()));
        }

        [Fact]
        public void NotThrowIfThePredicateReturnsFalseForAnIntType()
        {
            var ex = new TestException();
            ex.ThrowIf<int>(o => false, Int32.MaxValue.GetRandom());
        }

        [Fact]
        public void ThrowTheExceptionIfThePredicateResultsInATrueValueForAnIntType()
        {
            int switchVal = 255.GetRandom(10);
            int testVal = switchVal + 99.GetRandom(1);

            var ex = new TestException();
            Func<int, bool> predicate = i => i > switchVal;
            Assert.Throws<TestException>(() => ex.ThrowIf(predicate, testVal));
        }

        [Fact]
        public void NotThrowIfThePredicateResultsInAFalseValueForAnIntType()
        {
            int switchVal = 255.GetRandom(10);
            int testVal = switchVal - 99.GetRandom(1);

            var ex = new TestException();
            Func<int, bool> predicate = i => i > switchVal;
            ex.ThrowIf(predicate, testVal);
        }

        #endregion

        #region Double Type Tests

        [Fact]
        public void ThrowAnExceptionIfThePredicateReturnsTrueForADoubleType()
        {
            var ex = new TestException();
            Assert.Throws<TestException>(() => ex.ThrowIf<double>(o => true, Double.MaxValue.GetRandom()));
        }

        [Fact]
        public void NotThrowIfThePredicateReturnsFalseForADoubleType()
        {
            var ex = new TestException();
            double maxValue = Convert.ToDouble(Single.MaxValue);
            ex.ThrowIf<double>(o => false, maxValue.GetRandom());
        }

        [Fact]
        public void ThrowTheExceptionIfThePredicateResultsInATrueValueForADoubleType()
        {
            double switchVal = (5225.0).GetRandom(10.0);
            double testVal = switchVal + (99.9).GetRandom(1.0);

            var ex = new TestException();
            Func<double, bool> predicate = i => i > switchVal;
            Assert.Throws<TestException>(() => ex.ThrowIf(predicate, testVal));
        }

        [Fact]
        public void NotThrowIfThePredicateResultsInAFalseValueForADoubleType()
        {
            double switchVal = (5225.0).GetRandom(10.0);
            double testVal = switchVal + (99.9).GetRandom(1.0);

            var ex = new TestException();
            Func<double, bool> predicate = i => i < switchVal;
            ex.ThrowIf(predicate, testVal);
        }

        #endregion
    }
}
