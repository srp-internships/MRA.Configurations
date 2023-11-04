using Microsoft.Extensions.DependencyInjection;
using MRA.Configurations.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA.Configurations.Initializer.OsonSms.SmsService;
public static class SmsServiceInitializer
{
    public static void AddOsonSmsService(this IServiceCollection services)
    {
        services.AddScoped<ISmsService, Configurations.OsonSms.SmsService.SmsService>();
    }
}
