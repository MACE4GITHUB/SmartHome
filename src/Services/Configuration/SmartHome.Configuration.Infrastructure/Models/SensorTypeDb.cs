using System.Collections.Generic;

namespace SmartHome.Configuration.Infrastructure.Models
{
    public class SensorTypeDb
    {
        /// <summary>
        /// Gets or sets a SensorTypeId.
        /// </summary>
        public string SensorTypeId { get; set; }

        /// <summary>
        /// The sensor type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// The sensor description.
        /// </summary>
        public string TypeDescription { get; set; }

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
        /// Navigation property.
        /// </summary>
        public ICollection<SensorDb> Sensors { get; set; }
    }
}