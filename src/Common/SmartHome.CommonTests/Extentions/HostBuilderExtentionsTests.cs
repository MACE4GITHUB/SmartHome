using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SmartHome.Common.Extentions;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartHome.CommonTests.Extentions
{
    public class HostBuilderExtentionsTests
    {
        [Fact]
        public void SetupLoggerThrowArgumentExceptionLoggingNotDefinedTest()
        {
            var builder = new HostBuilder().ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("NoLogging", "enabled"),
                    })
                    .Build();
            });

            Action action = () => builder.SetupLogger().Build();

            action.Should().Throw<ArgumentException>().WithMessage("'Logging' is not defined.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData("enabled")]
        [InlineData(null)]
        public void SetupLoggerThrowArgumentExceptionLogToSerilogNotDefinedTest(string enabled)
        {
            var builder = new HostBuilder().ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("Logging", "enabled"),
                        new("LogToSerilog", enabled),
                    })
                    .Build();
            });

            Action action = () => builder.SetupLogger().Build();

            action.Should().Throw<ArgumentException>().WithMessage("'LogToSerilog' is not defined.");
        }

        [Theory]
        [InlineData("false")]
        [InlineData("False")]
        [InlineData("FAlse")]
        [InlineData(" fAlse  ")]
        public void SetupLoggerLogToSerilogFalseTest(string enabled)
        {
            var builder = new HostBuilder().ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("Logging", "enabled"),
                        new("LogToSerilog", enabled),
                    })
                    .Build();
            });

            builder.SetupLogger().Build();

            Environment.GetEnvironmentVariable("BASEDIR").Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData("true")]
        [InlineData("True")]
        [InlineData("TRue")]
        [InlineData(" trUe  ")]
        public void SetupLoggerLogToSerilogTrueTest(string enabled)
        {
            var builder = new HostBuilder().ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("Logging", "enabled"),
                        new("LogToSerilog", enabled),
                    })
                    .Build();
            });

            builder.SetupLogger().Build();

            Environment.GetEnvironmentVariable("BASEDIR").Should().NotBeNullOrEmpty();
        }
    }
}