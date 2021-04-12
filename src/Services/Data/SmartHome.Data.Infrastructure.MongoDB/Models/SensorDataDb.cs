using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SmartHome.Data.Infrastructure.MongoDB.Models
{
    /// <summary>
    /// Represents a data request.
    /// </summary>
    public class SensorDataDb
    {
        /// <summary>
        /// Gets or sets a SensorTypeId.
        /// </summary>
        [BsonId]
        public string SensorTypeId { get; set; }

        /// <summary>
        /// Gets or sets a Timestamp.
        /// </summary>
        [BsonIgnoreIfDefault]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets a Value.
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets a normalized Value.
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal NormalValue { get; set; }
    }
}
