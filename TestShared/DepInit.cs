using Microsoft.Extensions.DependencyInjection;
using Mra.Shared.Initializer.OsonSms.SmsService;

namespace TestShared
{
    public static class DepInit
    {
        public static void Test(this IServiceCollection services)
        {
            services.AddOsonSmsService();
        }
    }
}
