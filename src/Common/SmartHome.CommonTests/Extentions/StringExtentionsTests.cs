using FluentAssertions;
using SmartHome.Common.Extentions;
using Xunit;

namespace SmartHome.CommonTests.Extentions.Tests
{
    public class StringExtentionsTests
    {
        [Theory]
        [InlineData("url\\", false)]
        [InlineData("http/", false)]
        [InlineData("any", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        public void IsNullOrEmptyTest(string value, bool expectedResult)
        {
            value.IsNullOrEmpty().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("-", false)]
        [InlineData("\n", true)]
        [InlineData("\r", true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("   ", true)]
        [InlineData("    ", true)]
        public void IsNullOrEmptyOrWhiteSpaceTest(string value, bool expectedResult)
        {
            value.IsNullOrEmptyOrWhiteSpace().Should().Be(expectedResult);
        }
    }
}