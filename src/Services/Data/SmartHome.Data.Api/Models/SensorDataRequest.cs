using System;

namespace SmartHome.Data.Api.Models
{
    /// <summary>
    /// Represents a data request.
    /// </summary>
    public class SensorDataRequest
    {
        /// <summary>
        /// Gets or sets a SensorTypeId.
        /// </summary>
        public Guid SensorTypeId { get; set; }

        /// <summary>
        /// Gets or sets a Timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets a Value.
        /// </summary>
        public decimal Value { get; set; }
    }
}
