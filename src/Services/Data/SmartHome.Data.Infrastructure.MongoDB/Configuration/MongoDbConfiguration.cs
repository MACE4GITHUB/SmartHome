namespace SmartHome.Data.Infrastructure.MongoDB.Configuration
{
    /// <summary>
    /// Represents the mongoDB configuration.
    /// </summary>
    public class MongoDbConfiguration
    {
        /// <summary>
        /// Gets or sets ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets Database.
        /// </summary>
        public string Database { get; set; }
    }
}
