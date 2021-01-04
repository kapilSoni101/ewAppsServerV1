using ewApps.Core.Webhook.Subscriber;
using ewApps.Core.Webhook.Subscription;
using ewApps.Payment.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace ewApps.Payment.WebApi
{
  public class Startup
  {

    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      Configuration = configuration;
      Configuration = PaymentServiceCollection.InitAppSetting(configuration, env);
    }

    public IConfiguration Configuration
    {
      get;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.ConfigurePaymentAppSettings(Configuration);

      services.ConfigureCoreServiceDependencies(Configuration);

      services.AddPaymentDataServiceDependency();

      services.AddPaymentDataDependency();
      services.AddScoped<IWebhookEventDelegate, PaymentWebhookEventDelegate>();

      services.AddPaymentOtherDependency(Configuration);

      services.ConfigureResponseCompressionService();

      services.ConfigureAuthenticationService(Configuration);

      services.ConfigurationCORSService();

      services.ConfigureSwaggerService();

      //services.AddAllReportDependency(Configuration);
      //SignalR Dependancies
      // services.AddTransient<IRealTimeUpdate, AppRealTimeUpdate<AppHub>>();
      services.AddSignalR(config => { config.EnableDetailedErrors = true; });
      services.AddWebhookSubscriptionSDependency(Configuration);
      services.AddHostedService<WebhookEventHandler>();
      services.AddScoped<PaymentWebhookSubscriptionManager>();

      // Webhook Server
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
      })
      .AddApplicationPart(Assembly.Load(new AssemblyName("ewApps.Core.Webhook.Subscription")));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.ConfigureResponseCompressionMiddleware();

      app.AddPaymentMiddleware();

      app.ConfigurationAuthentication();

      app.ConfigureSwagger();

      app.ConfigureCORS();

      // Webhook Registration
      using (IServiceScope scope = app.ApplicationServices.CreateScope())
      {

        PaymentWebhookSubscriptionManager manager = scope.ServiceProvider.GetRequiredService<PaymentWebhookSubscriptionManager>();
        manager.InitWebhookSubscription(app);
      }

      app.UseHsts();

      app.UseHttpsRedirection();

      app.UseMvc();
    }
  }
}
