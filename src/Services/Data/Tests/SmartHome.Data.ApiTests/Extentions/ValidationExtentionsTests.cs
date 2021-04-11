using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SmartHome.Data.Api.Extentions.Tests
{
    public class ValidationExtentionsTests
    {
        private readonly IServiceCollection _service = new ServiceCollection();

        public ValidationExtentionsTests()
        {
            _service.AddControllers().AddSmartHomeDataValidation();
        }

        [Fact]
        public void AddCrazyPriceValidationTest()
        {
            var serviceName = "ServiceProviderValidatorFactory";
            var hasService = _service.Any(serviceDescriptor => serviceDescriptor?.ImplementationType?.Name == serviceName);

            hasService.Should().BeTrue($"ServiceCollection is not contains {serviceName}.");
        }

        [Fact]
        public void UseCrazyPriceValidationTest()
        {
            var appBuilder = new ApplicationBuilder(_service.BuildServiceProvider());

            var value = ValidatorOptions.Global.LanguageManager.Enabled;
            value.Should().BeTrue();

            var mocklogger = new Mock<IMockLogger>();
            mocklogger.Setup(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()));

            appBuilder.UseSmartHomeDataValidation(mocklogger.Object).Build();

            value = ValidatorOptions.Global.LanguageManager.Enabled;
            value.Should().BeFalse();
        }

        internal interface IMockLogger : ILogger
        {
            void LogInformation(string message, params object[] args);
        }
    }
}