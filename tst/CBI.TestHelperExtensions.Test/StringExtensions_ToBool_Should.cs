using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace TestHelperExtensions.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensions_ToBool_Should
    {
        [Theory]
        [InlineData(null!)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("mung")]
        [InlineData("maybe")]
        [InlineData("null")]
        [InlineData("undefined")]
        [InlineData("nan")]
        [InlineData("hello")]
        [InlineData("falsey")]
        [InlineData("truthy")]
        [InlineData("yesno")]
        [InlineData("noyes")]
        [InlineData("n/a")]
        [InlineData("na")]
        [InlineData("none")]
        [InlineData("agree")]
        [InlineData("disagree")]
        [InlineData("Mung")]
        [InlineData("Maybe")]
        [InlineData("Null")]
        [InlineData("Undefined")]
        [InlineData("Nan")]
        [InlineData("Hello")]
        [InlineData("Falsey")]
        [InlineData("Truthy")]
        [InlineData("YesNo")]
        [InlineData("NoYes")]
        [InlineData("N/A")]
        [InlineData("NA")]
        [InlineData("None")]
        [InlineData("Agree")]
        [InlineData("Disagree")]
        [InlineData("MUNG")]
        [InlineData("MAYBE")]
        [InlineData("NULL")]
        [InlineData("UNDEFINED")]
        [InlineData("NAN")]
        [InlineData("HELLO")]
        [InlineData("FALSEY")]
        [InlineData("TRUTHY")]
        [InlineData("YESNO")]
        [InlineData("NOYES")]
        [InlineData("NONE")]
        [InlineData("AGREE")]
        [InlineData("DISAGREE")]
        [InlineData("42")]
        [InlineData("0.5")]
        public void ThrowIfTheValueIsNotConvertableToBool(string value)
        {
            Assert.Throws<ArgumentException>(() => value.ToBool());
        }
        
        [Theory]
        [InlineData("0", false)]
        [InlineData("1", true)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("True", true)]
        [InlineData("False", false)]
        [InlineData("TRUE", true)]
        [InlineData("FALSE", false)]
        [InlineData("t", true)]
        [InlineData("f", false)]
        [InlineData("T", true)]
        [InlineData("F", false)]
        [InlineData("yes", true)]
        [InlineData("no", false)]
        [InlineData("Yes", true)]
        [InlineData("No", false)]
        [InlineData("YES", true)]
        [InlineData("NO", false)]
        [InlineData("y", true)]
        [InlineData("n", false)]
        [InlineData("Y", true)]
        [InlineData("N", false)]
        [InlineData("on", true)]
        [InlineData("off", false)]
        [InlineData("On", true)]
        [InlineData("Off", false)]
        [InlineData("ON", true)]
        [InlineData("OFF", false)]
        public void ReturnTheProperBoolIfTheValueIsConvertable(string value, bool expected)
        {
            Assert.Equal(expected, value.ToBool());
        }

    }
}
