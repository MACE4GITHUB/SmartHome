using SmartHome.Configuration.Abstractions;
using SmartHome.Configuration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Data.Api.Configuration
{
    public class ConfigurationCacheService : IConfigurationService
    {
        private readonly IConfigurationService _service;
        private readonly Dictionary<Guid, Sensor> _sensors;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public ConfigurationCacheService(IConfigurationService service)
        {
            _service = service;
            _sensors = new Dictionary<Guid, Sensor>();
        }

        public async Task<IEnumerable<Sensor>> GetAllSensorConfigurationsAsync()
        {
            return await _service.GetAllSensorConfigurationsAsync();
        }

        public async Task<Sensor> GetSensorConfigurationsByIdAsync(Guid id)
        {
            await Task.Yield();

            if (_sensors.TryGetValue(id, out var sensor1))
            {
                return sensor1;
            }

            await _semaphoreSlim.WaitAsync();

            if (_sensors.TryGetValue(id, out var sensor2))
            {
                return sensor2;
            }

            var sensorValue = await SensorConfigurationsByIdAsync(id);
            _semaphoreSlim.Release();

            return sensorValue;
        }

        private async Task<Sensor> SensorConfigurationsByIdAsync(Guid id)
        {
            var value = await _service.GetSensorConfigurationsByIdAsync(id);
            if (value != null)
            {
                _sensors.Add(value.SensorId, value);
            }

            return value;
        }
    }
}
