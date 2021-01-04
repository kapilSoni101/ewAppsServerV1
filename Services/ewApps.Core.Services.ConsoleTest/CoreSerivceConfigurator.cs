//using System;
//using ewApps.Core.AppDeeplinkService;
//using ewApps.Core.DbConProvider;
//using ewApps.Core.DI;
//using ewApps.Core.EmailService;
//using ewApps.Core.ExceptionService;
//using ewApps.Core.Money;
//using ewApps.Core.NotificationService;
//using ewApps.Core.ScheduledJobService;
//using ewApps.Core.SerilogLoggingService;
//using ewApps.Core.SMSService;
//using ewApps.Core.ThumbnailService;
//using ewApps.Core.UserSessionService;
//using ewApps.Core.Webhook.Server;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;

//namespace ewApps.Core.Services.ConsoleTest {
//    [Flags]
//    public enum CoreServiceEnum {

//        AppDeeplinkService = 1,
//        DeeplinkServices = 2,
//        EmailService = 4,
//        ExceptionService = 8,
//        ExportService = 16,
//        GeocodingService = 32,
//        LoggingService = 64,
//        Money = 128,
//        NotificationService = 256,
//        ScheduledJobService = 512,
//        SerilogLoggingService = 1024,
//        SMSService = 2048,
//        StorageService = 4096,
//        ThumbnailService = 8192,
//        UserSessionService = 16384,
//        WebhookServer = 32768,
//        WebhookSubscription = 65536,
//        ConnectionManager = 131072
//    }


//    public abstract class DIConfigurator {

//        // Add DI
//        // Add AppSettings

//        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration, IApplicationBuilder app, IHostingEnvironment env);



//        // Add Middleware
//        public abstract void Configure(IServiceCollection services, IConfiguration configuration, IApplicationBuilder app, IHostingEnvironment env);

//    }

//    public class DIConfigurationModel {

//        public CoreServiceEnum IncludedCoreServices {
//            get; set;
//        } = 0;

//        public bool AddDSDependencies {
//            get; set;
//        }

//        public bool AddDataDependencies {
//            get; set;
//        }

//        public bool AddReportDependencies {
//            get; set;
//        }

//        public bool AddAppSettings {
//            get; set;
//        }

//        public bool AddMiddleware {
//            get; set;
//        } = false;

//        public bool CoreServiceMask {
//            get; set;
//        } = false;

//        public bool ApplyWebApiConfiguration {
//            get; set;
//        } = false;

//        //public static DIConfigurationModel Clone(DIConfigurationModel configurationModel) {
//        //    DIConfigurationModel clonedConfigurationModel = new DIConfigurationModel();
//        //    clonedConfigurationModel.AddAppSettings = configurationModel.AddAppSettings;
//        //    clonedConfigurationModel.AddDataDependencies = configurationModel.AddDataDependencies;
//        //    clonedConfigurationModel.AddDSDependencies = configurationModel.AddDSDependencies;
//        //    clonedConfigurationModel.AddMiddleware = configurationModel.AddMiddleware;
//        //    clonedConfigurationModel.ApplyWebApiConfiguration = configurationModel.ApplyWebApiConfiguration;
//        //    clonedConfigurationModel.CoreServiceMask = configurationModel.CoreServiceMask;
//        //    clonedConfigurationModel.MemberwiseClone()
//        //}


//        //  DataService
//        //  Data
//        //  DbContext
//        //  AppSettings
//        //  Middleware	- Define methods for each middle ware
//        //      (Maksvalue or named property)
//        //- UserSession
//        //- Logging
//        //      (Specific configuration)
//        //- Webhook
//        //  Services
//    }

//    public class CoreServiceConfigurator {
//        DIConfigurationModel _diConfigurator;

//        public CoreServiceConfigurator(DIConfigurationModel diConfigurator) {
//            _diConfigurator = diConfigurator;
//        }

//        public virtual void Configure(IConfiguration configuration, IApplicationBuilder app, IHostingEnvironment env) {
//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ExceptionService)) {
//                app.UseMiddleware<ErrorHandlingMiddleware>();
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.Money)) {
//                app.UseMiddleware<CurrencyConverterMiddleware>();
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.Money)) {
//                app.UseMiddleware<CurrencyConverterMiddleware>();
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.SerilogLoggingService)) {
//                app.UseMiddleware<SerilogHttpMiddleware>(Options.Create(new SerilogHttpMiddlewareOptions() {
//                    EnableExceptionLogging = false
//                }));
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.SerilogLoggingService)) {
//                app.UseMiddleware<UserSessionMiddleware>();
//            }

//        }

//        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration) {
//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.EmailService)) {
//                services.AddEmailDependency(configuration, DispatcherType.HMailSMTP);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.SMSService)) {
//                services.AddSMSServiceDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.AppDeeplinkService)) {
//                services.AppDeeplinkDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.AppDeeplinkService)) {
//                services.AppDeeplinkDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.Money)) {
//                services.AddDocumentCurrencyDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.NotificationService)) {
//                services.AddNotificationDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ScheduledJobService)) {
//                services.ScheduledJobDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ThumbnailService)) {
//                services.AddThumbnailDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.UserSessionService)) {
//                services.AddUserSessionDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.WebhookServer)) {
//                services.AddWebhookServerDependency(configuration);
//            }

//            if(_diConfigurator.IncludedCoreServices.HasFlag(CoreServiceEnum.ConnectionManager)) {
//                // Manage Transaction And Connection
//                services.AddScoped<IConnectionManager, ConnectionManager>();
//            }

//        }
//    }
//}
