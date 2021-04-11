using SmartHome.Configuration.Abstractions;
using SmartHome.Configuration.Abstractions.Models;
using System;

namespace SmartHome.Data.Api.Configuration
{
    /// <summary>
    /// Represents temporary SensorConfiguration.
    /// </summary>
    public class SensorConfiguration : ISensorConfiguration
    {
        /// <summary>
        /// Gets fake configuration.
        /// </summary>
        /// <param name="sensorTypeId"></param>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public bool TryGetSensorConfiguration(Guid sensorTypeId, out Sensor sensor)
        {
            sensor = new Sensor()
            {
                SensorTypeId = new Guid("2223cc1a-0be0-4ea4-a7b1-e905d23e8e9c"),
                IsEnabled = true,
                MinValue = -10,
                MinNormalValue = -10,
                MaxValue = 100,
                MaxNormalValue = 100,
                Precision = 0
            };

            return sensorTypeId == sensor.SensorTypeId;
        }
    }
}
