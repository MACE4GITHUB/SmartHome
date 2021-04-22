namespace SmartHome.Configuration.Infrastructure.Models
{
    /// <summary>
    /// Represents the sensor in database.
    /// </summary>
    public class SensorDb
    {
        /// <summary>
        /// Gets or sets a SensorId.
        /// </summary>
        public string SensorId { get; set; }

        /// <summary>
        /// The sensor name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The sensor description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the sensor enable. 
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// The sensor precision.
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// Gets or sets a SensorTypeId.
        /// </summary>
        public string SensorTypeId { get; set; }

        /// <summary>
        /// Gets or sets a SensorTypeDb.
        /// </summary>
        public SensorTypeDb SensorType { get; set; }
    }
}