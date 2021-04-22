﻿using SmartHome.Data.Infrastructure.Abstractions.Models;
using SmartHome.Data.Infrastructure.MongoDB.Models;

namespace SmartHome.Data.Infrastructure.MongoDB.Extentions
{
    public static class SensorDataExtentions
    {
        public static SensorDataDb ToSensorDataDb(this SensorDataRequest sensorDataRequest) =>
            new()
            {
                SensorId = sensorDataRequest.SensorId.ToString(),
                Timestamp = sensorDataRequest.Timestamp,
                Value = sensorDataRequest.Value,
                NormalValue = sensorDataRequest.NormalValue
            };
    }
}
