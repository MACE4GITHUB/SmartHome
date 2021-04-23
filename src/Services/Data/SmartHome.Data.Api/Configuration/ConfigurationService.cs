using System;
using SmartHome.Configuration.Abstractions;
using SmartHome.Configuration.Abstractions.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartHome.Data.Api.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly HttpClient _httpClient;
        private const string PartRequestUri = "http://localhost:5001/api/v1/sensors/";

        public ConfigurationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensorConfigurationsAsync()
        {
            try
            {
                var response = await GetStringAsync("getAll");
                var sensors = JsonSerializer.Deserialize<IEnumerable<Sensor>>(response, GetJsonSerializerOptions());

                return sensors;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Sensor> GetSensorConfigurationsByIdAsync(Guid id)
        {
            try
            {
                var response = await GetStringAsync($"get?id={id}");
                var sensor = JsonSerializer.Deserialize<Sensor>(response, GetJsonSerializerOptions());

                return sensor;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GetStringAsync(string requestUri) => 
            await _httpClient.GetStringAsync($"{PartRequestUri}{requestUri}");


        private static JsonSerializerOptions GetJsonSerializerOptions() => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {new JsonStringEnumConverter()},
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}
