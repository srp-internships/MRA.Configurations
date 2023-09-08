using Microsoft.Extensions.DependencyInjection;
using Mra.Shared.Common.Interfaces.Services;
using Mra.Shared.Services;

namespace Mra.Shared.Initializer.Services;
public static class FileSmsServiceExtension
{
    public static void AddFileSmsService(this IServiceCollection services)
    {
        services.AddScoped<ISmsService, FileSmsService>();
    }
}

