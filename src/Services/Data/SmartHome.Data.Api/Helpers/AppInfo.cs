using System.Reflection;

namespace SmartHome.Data.Api.Helpers
{
    /// <summary>
    /// Represents the application information.
    /// </summary>
    public static class AppInfo
    {
        private static AssemblyName AssemblyName => Assembly.GetExecutingAssembly().GetName();

        /// <summary>
        /// Gets the application name.
        /// </summary>
        public static string Name => AssemblyName.Name;

        /// <summary>
        /// Gets the application name and version in format: "ApplicationName v1.0.0.0".
        /// </summary>
        public static string NameAndVersion
        {
            get
            {
                var assemblyName = AssemblyName;

                return $"{assemblyName.Name} v{assemblyName.Version}";
            }
        }
    }
}
