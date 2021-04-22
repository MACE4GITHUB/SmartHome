using FluentAssertions;
using SmartHome.Data.Infrastructure.Abstractions.Models;
using System;
using Xunit;

namespace SmartHome.Data.Api.Extentions.Tests
{
    public class SensorDataRequestExtentionsTests
    {
        [Fact]
        public void IsNullOrEmptyNullTest()
        {
            SensorDataRequest sensorDataRequest = null;
            sensorDataRequest.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmptyIdEmptyTest()
        {
            var sensorDataRequest = new SensorDataRequest { SensorId = Guid.Empty };
            sensorDataRequest.IsNullOrEmpty().Should().BeTrue();
        }
    }
}