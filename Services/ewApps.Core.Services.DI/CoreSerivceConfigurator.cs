using ewApps.Core.AppDeeplinkService;
using ewApps.Core.DbConProvider;
using ewApps.Core.DI;
using ewApps.Core.DMService;
using ewApps.Core.EmailService;
using ewApps.Core.ExceptionService;
using ewApps.Core.Money;
using ewApps.Core.NotificationService;
using ewApps.Core.ScheduledJobService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.SMSService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.Webhook.Server;
using ewApps.Core.Webhook.Subscription;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.Services.DI {

    public class CoreServiceConfigurator {
        DIConfigurationModel _diConfigurator;

        public CoreServiceConfigurator(DIConfigurationModel diConfigurator) {
            _diConfigurator = diConfigurator;
        }


        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration, CoreServiceDIOptions coreServiceDIOptions) {

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.EmailService)) {
                services.AddEmailDependency(configuration, EmailDispatcherType.HMailSMTP);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.SMSService)) {
                services.AddSMSServiceDependency(configuration, SMSDispatcherType.Twilio);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.AppDeeplinkService)) {
                services.AppDeeplinkDependency(configuration);
            }

            //if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.AppDeeplinkService)) {
            //    services.AppDeeplinkDependency(configuration);
            //}

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.Money)) {
                services.AddDocumentCurrencyDependency(configuration);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.NotificationService)) {
                services.AddNotificationDependency(configuration);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ScheduledJobService)) {
                services.ScheduledJobDependency(configuration);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.DMService)) {
                services.AddDMDependency(configuration);            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.UserSessionService)) {
                services.AddUserSessionDependency(configuration, coreServiceDIOptions.SessionOptions);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.WebhookServer)) {
                services.AddWebhookServerDependency(configuration);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.WebhookSubscription)) {
                services.AddWebhookSubscriptionSDependency(configuration);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ConnectionManager)) {
                // Manage Transaction And Connection
                services.AddScoped<IConnectionManager, ConnectionManager>();
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ExceptionService)) {
                // Manage Transaction And Connection
                services.AddExceptionDependency(configuration);
            }

            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.UniqueIdentityGenerator)) {
                services.AddUniqueIdentityGeneratorDependency(configuration);
            }

        }


    }

    public static class CoreServiceConfiguratorNew {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services, IConfiguration configuration, CoreServiceEnum includedCoreServices, CoreServiceDIOptions coreServiceDIOptions) {

            services.AddScoped<IASNotificationService, ASNotificationService>();


            if(includedCoreServices.HasFlag(CoreServiceEnum.EmailService)) {
                services.AddEmailDependency(configuration, EmailDispatcherType.HMailSMTP);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.SMSService)) {
                services.AddSMSServiceDependency(configuration, SMSDispatcherType.Twilio);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.AppDeeplinkService)) {
                services.AppDeeplinkDependency(configuration);
            }

            //if(includedCoreServices.HasFlag(CoreServiceEnum.AppDeeplinkService)) {
            //    services.AppDeeplinkDependency(configuration);
            //}

            if(includedCoreServices.HasFlag(CoreServiceEnum.Money)) {
                services.AddDocumentCurrencyDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.NotificationService)) {
                services.AddNotificationDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.ScheduledJobService)) {
                services.ScheduledJobDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.DMService)) {
                services.AddDMDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.UserSessionService)) {
                services.AddUserSessionDependency(configuration, coreServiceDIOptions.SessionOptions);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.WebhookServer)) {
                services.AddWebhookServerDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.WebhookSubscription)) {
                services.AddWebhookSubscriptionSDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.ConnectionManager)) {
                // Manage Transaction And Connection
                services.AddScoped<IConnectionManager, ConnectionManager>();
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.ExceptionService)) {
                // Manage Transaction And Connection
                services.AddExceptionDependency(configuration);
            }

            if(includedCoreServices.HasFlag(CoreServiceEnum.ServiceProcessor)) {
        services.AddServiceProcessorDependency(configuration);
            }


            if(includedCoreServices.HasFlag(CoreServiceEnum.UniqueIdentityGenerator)) {
                services.AddUniqueIdentityGeneratorDependency(configuration);
            }

            return services;

        }
    }
}
