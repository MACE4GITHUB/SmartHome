using FluentAssertions;
using SmartHome.Data.Infrastructure.MongoDB.Configuration;
using Xunit;

namespace SmartHome.Data.Infrastructure.MongoDB.Validators.Tests
{
    public class MongoDbConfigurationValidationTests
    {
        [Theory]
        [InlineData("", "", false)]
        [InlineData(" ", "", false)]
        [InlineData("", " ", false)]
        [InlineData(null, " ", false)]
        [InlineData(null, null, false)]
        [InlineData("null", null, false)]
        [InlineData("connectionString", "dataBase", true)]
        public void MongoDbConfigurationTest(string connectionString, string dataBase, bool expectedResult)
        {
            var config = new MongoDbConfiguration
            {
                ConnectionString = connectionString,
                Database = dataBase
            };

            var rule = new MongoDbConfigurationValidation();
            rule.Validate(null, config).Succeeded.Should().Be(expectedResult);
        }
    }
}