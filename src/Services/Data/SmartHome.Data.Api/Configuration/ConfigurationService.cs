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
        public async Task<IEnumerable<Sensor>> GetAllSensorConfigurations()
        {
            // A very simple code

            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetStringAsync("http://localhost:5001/api/v1/sensors/getAll");

                var sensors = JsonSerializer.Deserialize<IEnumerable<Sensor>>(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                return sensors;
            }
            catch
            {
                return null;
            }
        }
    }
}
