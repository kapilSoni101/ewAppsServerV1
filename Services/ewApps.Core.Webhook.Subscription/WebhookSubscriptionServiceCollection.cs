
using ewApps.Core.Webhook.Subscriber;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.Webhook.Subscription {

    public static class WebhookSubscriptionServiceCollection {

        public static IServiceCollection AddWebhookSubscriptionSDependency(this IServiceCollection services, IConfiguration Configuration) {

            var webhookSubscription = Configuration.GetSection("WebhookSubscriptionAppSettings");
            services.Configure<WebhookSubscriptionAppSettings>(webhookSubscription);
            //Add Webhook subscription
            //services.AddDbContext<WebhookSubscriptionDBContext>(ServiceLifetime.Transient);
            services.AddDbContext<WebhookSubscriptionDBContext>();
            services.AddScoped<WebhookSubscriptionManager>();

            return services;
        }
    }
}
