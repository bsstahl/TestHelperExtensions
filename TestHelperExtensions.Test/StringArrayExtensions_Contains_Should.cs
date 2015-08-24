using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelperExtensions.Test
{
    [TestClass]
    public class StringArrayExtensions_Contains_Should
    {
        const string _targetValue = "16FFAC08-676A-4737-B4D9-13971B10752A";
        string[] _dataWithTargetAtStart = new string[] { _targetValue, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
        string[] _dataWithTargetInMiddle = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), _targetValue, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
        string[] _dataWithTargetAtEnd = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), _targetValue };

        [TestMethod]
        public void ReturnFalseIfArrayIsNull()
        {
            string[] target = null;
            var actual = target.Contains(_targetValue);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReturnFalseIFArrayIsEmpty()
        {
            var actual = (new string[] { }).Contains(_targetValue);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReturnFalseIfSearchStringIsEmpty()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(string.Empty);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReturnFalseIfSearchStringIsNull()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(null);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReturnTrueIfTestedValueIsOnlyValue()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(_targetValue);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ReturnFalseIfOnlyValueIsNotTheValueSearchedFor()
        {
            string[] target = new string[] { _targetValue };
            var actual = target.Contains(Guid.NewGuid().ToString());
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReturnFalseIfValueIsNotInTheList()
        {
            var actual = _dataWithTargetInMiddle.Contains(Guid.NewGuid().ToString());
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReturnTrueIfTestedValueIsFoundAtTheFrontOfTheList()
        {
            var actual = _dataWithTargetAtStart.Contains(_targetValue);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ReturnTrueIfTestedValueIsFoundInTheMiddleOfTheList()
        {
            var actual = _dataWithTargetInMiddle.Contains(_targetValue);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ReturnTrueIfTestedValueIsFoundAtTheEndOfTheList()
        {
            var actual = _dataWithTargetAtEnd.Contains(_targetValue);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CorrectlyExecutesACaseSensitiveComparisonType()
        {
            var actual = _dataWithTargetAtEnd.Contains(_targetValue.ToLower(), StringComparison.InvariantCulture);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CorrectlyExecutesACaseInsensitiveComparisonType()
        {
            var actual = _dataWithTargetAtEnd.Contains(_targetValue.ToLower(), StringComparison.InvariantCultureIgnoreCase);
            Assert.IsTrue(actual);
        }
    }
}
