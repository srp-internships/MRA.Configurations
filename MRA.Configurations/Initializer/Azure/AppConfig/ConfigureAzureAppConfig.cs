using Microsoft.Extensions.Configuration;

namespace MRA.Configurations.Initializer.Azure.AppConfig
{
    /// <summary>
    /// Extension methods for configuring Azure App Configuration in the application.
    /// </summary>
    public static class ConfigureAzureAppConfig
    {
        /// <summary>
        /// Adds Azure App Configuration to the configuration manager.
        /// </summary>
        /// <param name="configurations">The <see cref="ConfigurationManager"/> instance.</param>
        /// <param name="connectionString">The Azure App Configuration connection string.</param>
        public static void AddAzureAppConfig(this ConfigurationManager configurations, string connectionString)
        {
            // Connects to Azure App Configuration using the provided connection string.
            configurations.AddAzureAppConfiguration(options => options.Connect(connectionString));
        }
    }
}