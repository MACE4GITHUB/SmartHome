using System;

namespace SmartHome.Configuration.Abstractions.Models
{
    /// <summary>
    /// Represents the sensor.
    /// </summary>
    public class Sensor
    {
        /// <summary>
        /// Gets or sets a SensorTypeId.
        /// </summary>
        public Guid SensorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the sensor enable. 
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for a sensor.
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the normalized minimum value for a sensor. 
        /// </summary>
        public decimal MinNormalValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for a sensor.  
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the normalized maximum value for a sensor.  
        /// </summary>
        public decimal MaxNormalValue { get; set; }

        /// <summary>
        /// The sensor precision.
        /// </summary>
        public byte Precision { get; set; }
    }
}