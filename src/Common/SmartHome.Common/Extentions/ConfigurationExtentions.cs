using Microsoft.Extensions.Configuration;

namespace SmartHome.Common.Extentions
{
    using Exceptions;

    /// <summary>
    /// Represents extentions for gets values from Configuration.
    /// </summary>
    public static class ConfigurationExtentions
    {
        /// <summary>
        /// Gets string value.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(this IConfiguration config, string key) =>
            config.ParseSection(key).Value;

        /// <summary>
        /// Gets IConfigurationSection from Configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationSection ParseSection(this IConfiguration configuration, string key)
        {
            var section = configuration.GetSection(key);
            return section.Exists()
                ? section
                : throw new NotDefinedException(key);
        }
    }
}
