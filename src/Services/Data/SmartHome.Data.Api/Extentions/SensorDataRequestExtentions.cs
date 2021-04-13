using SmartHome.Data.Infrastructure.Abstractions.Models;
using System;

namespace SmartHome.Data.Api.Extentions
{
    /// <summary>
    /// Represents extentions for SensorDataRequest.
    /// </summary>
    public static class SensorDataRequestExtentions
    {
        /// <summary>
        /// Gets true when SensorDataRequest is Null or it SensorTypeId is Empty otherwise false.
        /// </summary>
        /// <param name="sensorDataRequest"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this SensorDataRequest sensorDataRequest) =>
            sensorDataRequest == null || sensorDataRequest.SensorTypeId == Guid.Empty;
    }
}
