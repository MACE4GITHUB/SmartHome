using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SmartHome.Data.Api
{
    using Common.Extentions;

    /// <summary>
    /// Determines the entry point into the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the web application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates HostBuilder using the default Builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .SetupLogger();
    }
}
