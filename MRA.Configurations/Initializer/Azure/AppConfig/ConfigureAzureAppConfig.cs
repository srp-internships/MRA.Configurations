using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA.Configurations.Initializer.Azure.AppConfig
{
    public static class ConfigureAzureAppConfig
    {
        public static void AddAzureAppConfig(this ConfigurationManager configurations, string connectionString)
        {
            configurations.AddAzureAppConfiguration(options => options.Connect(connectionString));
        }
    }
}
