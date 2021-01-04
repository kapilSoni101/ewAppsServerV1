using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.NotificationService {

    public static class NotificationServiceCollection {

        public static IServiceCollection AddNotificationDependency(this IServiceCollection services, IConfiguration Configuration) {

            services.AddScoped<IASNotificationService, ASNotificationService>();

            services.AddScoped<IDeeplinkService, DeeplinkService>();

            var notificationSection = Configuration.GetSection("NotificationAppSettings");
            services.Configure<NotificationAppSettings>(notificationSection);

            return services;
        }
    }
}
