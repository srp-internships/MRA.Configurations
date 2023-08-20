using Microsoft.Extensions.DependencyInjection;
using Mra.Shared.Common.Interfaces.Services;

namespace Mra.Shared.Initializer.Azure.EmailService;

public static class EmailServiceInitializer
{
    public static void AddAzureEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, Shared.Azure.EmailService.EmailService>();
    }
}