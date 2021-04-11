using System;

namespace SmartHome.Configuration.Abstractions
{
    using Models;

    /// <summary>
    /// Determines the interface for a sensor configuration.
    /// </summary>
    public interface ISensorConfiguration
    {
        /// <summary>
        /// Gets true when a sensor configuration exists otherwise false.
        /// </summary>
        /// <param name="sensorTypeId"></param>
        /// <param name="sensor"></param>
        /// <returns></returns>
        bool TryGetSensorConfiguration(Guid sensorTypeId, out Sensor sensor);
    }
}
