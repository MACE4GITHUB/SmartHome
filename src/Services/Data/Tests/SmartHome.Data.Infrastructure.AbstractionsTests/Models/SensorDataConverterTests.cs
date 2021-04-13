using FluentAssertions;
using Moq;
using SmartHome.Configuration.Abstractions.Models;
using System;
using Xunit;

namespace SmartHome.Data.Infrastructure.Abstractions.Models.Tests
{
    public class SensorDataConverterTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1.5)]
        [InlineData(-61.55)]
        public void NormalValueFromExceptionTest(decimal value)
        {
            var sensor = new Sensor
            {
                MinValue = value,
                MaxValue = value,
            };

            var sensorDataConverter = new SensorDataConverter(sensor);

            Action action = () => sensorDataConverter.NormalValueFrom(It.IsAny<decimal>());

            action.Should().Throw<ArithmeticException>().WithMessage("Abnormal sensor configuration.");
        }

        [Theory]
        [InlineData(2, -100, +2000, 0, 100, 852, 45.33)]
        [InlineData(2, +200, -1000, 0, 100, 100, 8.33)]
        [InlineData(2, +100, -1000, 8.33, 100, 200, 0)]
        [InlineData(2, -10, 100, -10, 100, 45, 45)]
        [InlineData(3, -270, 32, -167.78, 0, 80.6, 27)]
        public void NormalValueFromTest(byte precision, decimal minValue, decimal maxValue, decimal minNormalValue, decimal maxNormalValue, decimal value, decimal expectedNormalValue)
        {
            var sensor = new Sensor
            {
                MinValue = minValue,
                MaxValue = maxValue,
                MinNormalValue = minNormalValue,
                MaxNormalValue = maxNormalValue,
                Precision = precision
            };

            var sensorDataConverter = new SensorDataConverter(sensor);

            var normalValue = sensorDataConverter.NormalValueFrom(value);

            normalValue.Should().Be(expectedNormalValue);
        }
    }
}
