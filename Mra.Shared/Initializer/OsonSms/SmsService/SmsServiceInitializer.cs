using Microsoft.Extensions.DependencyInjection;
using Mra.Shared.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mra.Shared.Initializer.OsonSms.SmsService;
public static class SmsServiceInitializer
{
    public static void AddOsonSmsService(this IServiceCollection services)
    {
        services.AddScoped<ISmsService, Shared.OsonSms.SmsService.SmsService>();
    }
}
