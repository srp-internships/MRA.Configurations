using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Mra.Shared.Initializer.Azure.Insight;

public static class ApplicationInsightsConfig
{
    //todo write summary
    public static void AddApiApplicationInsights(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
        builder.Logging.AddApplicationInsights(
            configureTelemetryConfiguration: (config) => config.ConnectionString = connectionString,
            configureApplicationInsightsLoggerOptions: (options) => { }
        );
    }
}
