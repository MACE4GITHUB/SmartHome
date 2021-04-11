using FluentAssertions;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Linq;
using Xunit;

namespace SmartHome.Data.Infrastructure.MongoDB.Configuration.Tests
{
    public class MongoDbConventionTests
    {
        [Fact]
        public void MongoDbConventionTest()
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();
            var convention = ConventionRegistry.Lookup(typeof(Type)).Conventions.FirstOrDefault(c => c.Name == "CamelCaseElementName");
            convention.Should().NotBeNull();
        }
    }
}
