using Microsoft.Extensions.DependencyInjection;
using MRA.Configurations.Common.Interfaces.Services;
using MRA.Configurations.Services;

namespace MRA.Configurations.Initializer.Services;
public static class FileSmsServiceExtension
{
    public static void AddFileSmsService(this IServiceCollection services)
    {
        services.AddScoped<ISmsService, FileSmsService>();
    }
}

