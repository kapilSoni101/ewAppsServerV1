using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.Webhook.Server {

  public static class WebhookServerServiceCollection {

    public static IServiceCollection AddWebhookServerDependency(this IServiceCollection services, IConfiguration Configuration) {
      var webhookServerSection = Configuration.GetSection("WebhookServerAppSettings");      
      services.Configure<WebhookServerAppSettings>(webhookServerSection);
      services.AddDbContext<WebhookDBContext>(ServiceLifetime.Transient);
      services.AddScoped<WebhookServerManager>();
      services.AddHostedService<WebhookEventDispatcher>();
      return services;      
    }
  }
}
