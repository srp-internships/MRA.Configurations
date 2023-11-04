using Microsoft.Extensions.DependencyInjection;
using MRA.Configurations.Common.Interfaces.Services;

namespace MRA.Configurations.Initializer.Azure.EmailService;

public static class EmailServiceInitializer
{
    public static void AddAzureEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, Configurations.Azure.EmailService.EmailService>();
    }
}