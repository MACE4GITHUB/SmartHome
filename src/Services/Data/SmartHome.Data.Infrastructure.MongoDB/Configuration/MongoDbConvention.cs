using MongoDB.Bson.Serialization.Conventions;

namespace SmartHome.Data.Infrastructure.MongoDB.Configuration
{
    /// <summary>
    /// Determines the mongoDb convention.
    /// </summary>
    public static class MongoDbConvention
    {
        /// <summary>
        /// Sets the camel case element name convention.
        /// </summary>
        public static void SetCamelCaseElementNameConvention()
        {
            ConventionRegistry.Register("camelCase", new ConventionPack
            {
                new CamelCaseElementNameConvention()
            }, t => true);
        }
    }
}
