using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SmartHome.Configuration.Abstractions.Repositories;
using SmartHome.Configuration.Infrastructure;
using SmartHome.Configuration.Infrastructure.Models;
using SmartHome.Configuration.Infrastructure.Repositories;
using SmartHome.Configuration.Infrastructure.Validators;

namespace SmartHome.Configuration.Api.Extentions
{
    public static class DatabaseExtentions
    {
        /// <summary>
        /// Adds SqlServer services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbConfiguration>(configuration.GetSection("Database"));
            services.TryAddSingleton<IValidateOptions<DbConfiguration>, DbConfigurationValidation>();

            services.AddDbContext<ConfiguringContext>((sp, options) =>
            {
                var dbConfiguration = sp.GetService<IOptionsMonitor<DbConfiguration>>()!.CurrentValue;
                options.UseSqlServer(dbConfiguration.ConnectionString);
            });

            services.AddScoped<IRepository<SensorDb>, SensorsRepository>();

            return services;
        }
    }
}
