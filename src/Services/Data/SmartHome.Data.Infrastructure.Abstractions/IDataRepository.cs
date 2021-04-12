using SmartHome.Data.Infrastructure.Abstractions.Models;
using System.Threading.Tasks;

namespace SmartHome.Data.Infrastructure.Abstractions
{
    /// <summary>
    /// Represents interfaces for IDataRepository.
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Saves a SensorDataRequest.
        /// </summary>
        /// <param name="sensorDataRequest"></param>
        /// <returns></returns>
        Task SaveSensorDataAsync(SensorDataRequest sensorDataRequest);
    }
}
