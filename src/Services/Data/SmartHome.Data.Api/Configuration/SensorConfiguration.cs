using SmartHome.Configuration.Abstractions;
using SmartHome.Configuration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHome.Data.Api.Configuration
{
    /// <summary>
    /// Represents temporary SensorConfiguration.
    /// </summary>
    public class SensorConfiguration : ISensorConfiguration
    {
        private readonly IConfigurationService _configurationService;
        private Dictionary<Guid, Sensor> _sensors;

        public SensorConfiguration(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// Gets fake configuration.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public bool TryGetSensorConfiguration(Guid sensorId, out Sensor sensor)
        {
            if (_sensors == null)
            {
                var sensors = _configurationService.GetAllSensorConfigurations().GetAwaiter().GetResult().ToList();

                if (!sensors.Any())
                {
                    sensor = new Sensor();
                    return false;
                }

                _sensors = new Dictionary<Guid, Sensor>();
                foreach (var s in sensors)
                {
                    _sensors.Add(s.SensorId, s);
                }
            }

            return _sensors.TryGetValue(sensorId, out sensor);
        }
    }
}
