using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;

namespace SmartHome.Data.Api.Extentions
{
    using Helpers;

    /// <summary>
    /// Determines Swagger Extentions.
    /// </summary>
    public static class SwaggerExtentions
    {
        /// <summary>
        /// Adds Swagger with base description Web API for the site SmartHome Data.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            services
                .AddOptions<SwaggerGenOptions>()
                .Configure(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "SmartHome Data API",
                        Description = "Description Web API for the SmartHome Data.",
                        License = new OpenApiLicense
                        {
                            Name = "Use under MIT license",
                            Url = new Uri("https://opensource.org/licenses/MIT"),
                        }
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{AppInfo.Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                });

            services
                .AddOptions<SwaggerUIOptions>()
                .Configure(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartHome Data WebApi v1"));

            return services;
        }

        /// <summary>
        /// Uses Swagger and UseSwaggerUI Middleware.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerAndSwaggerUI(this IApplicationBuilder app) =>
            app
                .UseSwagger()
                .UseSwaggerUI();
    }
}
