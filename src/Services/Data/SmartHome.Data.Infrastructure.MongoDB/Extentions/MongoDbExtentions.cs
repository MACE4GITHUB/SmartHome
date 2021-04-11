using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SmartHome.Data.Infrastructure.MongoDB.Extentions
{
    using Configuration;
    using Validators;

    /// <summary>
    /// Represents the method for add MongoDb.
    /// </summary>
    public static class MongoDbExtentions
    {
        /// <summary>
        /// Gets MongoDb services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();

            services.Configure<MongoDbConfiguration>(configuration.GetSection("Database"));
            services.TryAddSingleton<IValidateOptions<MongoDbConfiguration>, MongoDbConfigurationValidation>();
            
            // Repositories (Singleton):

            return services;
        }
    }
}
