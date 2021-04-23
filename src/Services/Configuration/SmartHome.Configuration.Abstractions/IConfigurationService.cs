using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Configuration.Abstractions
{
    using Models;

    /// <summary>
    /// Determines the interface of configuration.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets all sensor configurations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Sensor>> GetAllSensorConfigurationsAsync();

        /// <summary>
        /// Gets a sensor configuration by async.
        /// </summary>
        /// <returns></returns>
        Task<Sensor> GetSensorConfigurationsByIdAsync(Guid id);
    }
}