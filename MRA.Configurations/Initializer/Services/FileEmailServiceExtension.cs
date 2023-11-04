using Microsoft.Extensions.DependencyInjection;
using MRA.Configurations.Common.Interfaces.Services;
using MRA.Configurations.Services;

namespace MRA.Configurations.Initializer.Services;

public static class FileEmailServiceExtension
{
    public static void AddFileEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, FileEmailService>();
    }
}