using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringArrayExtensions_Contains_Should
    {
        const string _targetValue = "16FFAC08-676A-4737-B4D9-13971B10752A";
        string[] _dataWithTargetAtStart = new string[] { _targetValue, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
        string[] _dataWithTargetInMiddle = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), _targetValue, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
        string[] _dataWithTargetAtEnd = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), _targetValue };

        [Fact]
        public void ReturnFalseIfArrayIsNull()
        {
            string[] target = null;
            var actual = target.Contains(_targetValue);
            Assert.False(actual);
        }

        [Fact]
        public void ReturnFalseIFArrayIsEmpty()
        {
            var actual = (new string[] { }).Contains(_targetValue);
            Assert.False(actual);
        }

        [Fact]
        public void ReturnFalseIfSearchStringIsEmpty()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(string.Empty);
            Assert.False(actual);
        }

        [Fact]
        public void ReturnFalseIfSearchStringIsNull()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(null);
            Assert.False(actual);
        }

        [Fact]
        public void ReturnTrueIfTestedValueIsOnlyValue()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(_targetValue);
            Assert.True(actual);
        }

        [Fact]
        public void ReturnFalseIfOnlyValueIsNotTheValueSearchedFor()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(Guid.NewGuid().ToString());
            Assert.False(actual);
        }

        [Fact]
        public void ReturnFalseIfValueIsNotInTheList()
        {
            var actual = _dataWithTargetInMiddle.Contains(Guid.NewGuid().ToString());
            Assert.False(actual);
        }

        [Fact]
        public void ReturnTrueIfTestedValueIsFoundAtTheFrontOfTheList()
        {
            var actual = _dataWithTargetAtStart.Contains(_targetValue);
            Assert.True(actual);
        }

        [Fact]
        public void ReturnTrueIfTestedValueIsFoundInTheMiddleOfTheList()
        {
            var actual = _dataWithTargetInMiddle.Contains(_targetValue);
            Assert.True(actual);
        }

        [Fact]
        public void ReturnTrueIfTestedValueIsFoundAtTheEndOfTheList()
        {
            var actual = _dataWithTargetAtEnd.Contains(_targetValue);
            Assert.True(actual);
        }

        [Fact]
        public void CorrectlyExecutesACaseSensitiveComparisonType()
        {
            var actual = _dataWithTargetAtEnd.Contains(_targetValue.ToLower(), StringComparison.InvariantCulture);
            Assert.False(actual);
        }

        [Fact]
        public void CorrectlyExecutesACaseInsensitiveComparisonType()
        {
            var actual = _dataWithTargetAtEnd.Contains(_targetValue.ToLower(), StringComparison.InvariantCultureIgnoreCase);
            Assert.True(actual);
        }
    }
}
