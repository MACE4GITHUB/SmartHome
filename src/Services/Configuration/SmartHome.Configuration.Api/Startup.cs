using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartHome.Configuration.Api.Extentions;
using SmartHome.Configuration.Api.Helpers;

namespace SmartHome.Configuration.Api
{
    /// <summary>
    /// Determines the startup configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Creates the startup configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="webHostEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets WebHostEnvironment.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            // Uncomment when add a first validator;
            //.AddSmartHomeConfigurationValidation();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            if (!WebHostEnvironment.IsProduction())
            {
                services.AddSwagger();
            }

            services.AddDatabase(Configuration);
        }

        /// <summary>
        /// Gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            logger.LogInformation("Starting {appInfo}", AppInfo.NameAndVersion);
            logger.LogInformation("Used {environment} mode.", WebHostEnvironment.EnvironmentName);

            if (WebHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Used developer exception page.");
            }

            if (!WebHostEnvironment.IsProduction())
            {
                app.UseSwaggerAndSwaggerUI();
                LogInfo(logger, "swagger");
            }

            //app.UseSmartHomeConfigurationValidation(logger);
            //LogInfo(logger, "validation");

            app.UseRouting();
            LogInfo(logger, "routing");

            app.UseAuthorization();
            LogInfo(logger, "authorization");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            LogInfo(logger, "endpoints");
        }

        private static void LogInfo(ILogger logger, string message)
        {
            logger.LogInformation("Used {value}.", message);
        }
    }
}
