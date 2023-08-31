using Microsoft.Extensions.DependencyInjection;
using Mra.Shared.Common.Interfaces.Services;
using Mra.Shared.Services;

namespace Mra.Shared.Initializer.Services;

public static class FileEmailServiceExtension
{
    public static void AddFileEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, FileEmailService>();
    }
}