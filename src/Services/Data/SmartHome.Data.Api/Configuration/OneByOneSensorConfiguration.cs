using SmartHome.Configuration.Abstractions;
using SmartHome.Configuration.Abstractions.Models;
using System;

namespace SmartHome.Data.Api.Configuration
{
    /// <summary>
    /// Represents temporary SensorConfiguration.
    /// </summary>
    public class OneByOneSensorConfiguration : ISensorConfiguration
    {
        private readonly IConfigurationService _configurationService;

        public OneByOneSensorConfiguration(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public bool TryGetSensorConfiguration(Guid sensorId, out Sensor sensor) =>
            (sensor = _configurationService.GetSensorConfigurationsByIdAsync(sensorId).GetAwaiter().GetResult()) != null;
    }
}
