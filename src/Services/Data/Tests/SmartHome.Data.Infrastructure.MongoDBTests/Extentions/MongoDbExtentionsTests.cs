using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartHome.Data.Infrastructure.MongoDB.Configuration;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartHome.Data.Infrastructure.MongoDB.Extentions.Tests
{
    public class MongoDbExtentionsTests
    {
        [Fact]
        public void AddMongoDbTest()
        {
            var memoryConfigurationSource = new MemoryConfigurationSource
            {
                InitialData = new KeyValuePair<string, string>[]
                {
                    new("Database:ConnectionStrings", "ValueConnectionStrings"),
                    new("Database:Database", "ValueDatabase")
                }
            };

            IConfiguration configuration = new ConfigurationRoot(new List<IConfigurationProvider>
            {
                new MemoryConfigurationProvider(memoryConfigurationSource)
            });

            IServiceCollection service = new ServiceCollection();
            service.AddMongoDb(configuration);

            var mainService = service.FirstOrDefault(d => d.ImplementationInstance is ConfigurationChangeTokenSource<MongoDbConfiguration>);
            mainService.Should().NotBeNull();
        }
    }
}