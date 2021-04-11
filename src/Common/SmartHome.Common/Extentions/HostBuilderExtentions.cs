using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace SmartHome.Common.Extentions
{
    using Exceptions;

    /// <summary>
    /// Represents Host Builder Extentions.
    /// </summary>
    public static class HostBuilderExtentions
    {
        /// <summary>
        /// Creates logging. Default logger is Serilog. When Serilog is not defined loads default logger providers.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder SetupLogger(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureLogging((hostingContext, logging) =>
            {
                var loggingConfiguration = hostingContext.Configuration.ParseSection("Logging");
                
                if (!hostingContext.HasLog("LogToSerilog"))
                {
                    return;
                }

                logging.ClearProviders();
                logging.AddConfiguration(loggingConfiguration);

                Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);
                logging.AddSerilog(new LoggerConfiguration()
                    .ReadFrom
                    .Configuration(hostingContext.Configuration)
                    .CreateLogger());
            });

        private static bool HasLog(this HostBuilderContext hostingContext, string kindLog)
        {
            if (!bool.TryParse(hostingContext.Configuration.GetString(kindLog), out var hasLog))
            {
                throw new NotDefinedException(kindLog);
            }

            return hasLog;
        }
    }
}
