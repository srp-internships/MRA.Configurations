using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mra.Shared.Initializer.Azure.Insight;

public static class ApplicationInsightsConfig
{
    //todo write summary
    public static void AddApiApplicationInsights(this ILoggingBuilder logging,IConfiguration configurations)
    {
        var connectionString = configurations["ApplicationInsights:ConnectionString"];
        logging.AddApplicationInsights(
            configureTelemetryConfiguration: config => config.ConnectionString = connectionString,
            configureApplicationInsightsLoggerOptions: _ => { }
        );
    }
}
