using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MRA.Configurations.Initializer.Azure.Insight;

public static class ApplicationInsightsConfig
{
    //todo write summary
    public static void AddApiApplicationInsights(this ILoggingBuilder logging,IConfiguration configurations)
    {
        var connectionString = configurations["Logging:ApplicationInsights:ConnectionString"];
        logging.AddApplicationInsights(
            configureTelemetryConfiguration: config => config.ConnectionString = connectionString,
            configureApplicationInsightsLoggerOptions: _ => { }
        );
    }
}
