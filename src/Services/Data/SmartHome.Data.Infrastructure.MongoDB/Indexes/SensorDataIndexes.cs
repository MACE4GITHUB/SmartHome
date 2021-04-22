using MongoDB.Driver;
using SmartHome.Data.Infrastructure.MongoDB.Models;
using System.Collections.Generic;

namespace SmartHome.Data.Infrastructure.MongoDB.Indexes
{
    /// <summary>
    /// Determines SensorData indexes.
    /// </summary>
    public static class SensorDataIndexes
    {
        /// <summary>
        /// Gets SensorData indexes.
        /// </summary>
        public static List<CreateIndexModel<SensorDataDb>> GetIndexes => new()
        {
            new CreateIndexModel<SensorDataDb>(Builders<SensorDataDb>.IndexKeys.Descending(f => f.Id)),
            new CreateIndexModel<SensorDataDb>(Builders<SensorDataDb>.IndexKeys.Ascending(f => f.SensorId)),
            new CreateIndexModel<SensorDataDb>(Builders<SensorDataDb>.IndexKeys.Ascending(f => f.Timestamp))
        };
    }
}
