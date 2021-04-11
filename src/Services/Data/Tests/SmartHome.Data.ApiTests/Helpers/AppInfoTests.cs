using FluentAssertions;
using Xunit;

namespace SmartHome.Data.Api.Helpers.Tests
{
    public class AppInfoTests
    {
        [Fact]
        public void AppInfoNameAndVersionTest()
        {
            var name = AppInfo.NameAndVersion;
            name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void AppInfoNameTest()
        {
            var name = AppInfo.Name;
            name.Should().NotBeNullOrEmpty();
        }
    }
}
