using System;
using ewApps.Core.EmailService;
using ewApps.Core.SMSService;
using ewApps.Notification.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace ewApps.Notification.WebApi {
    public class Startup {

        //DIConfigurationModel _diConfigurationModel;
        //NotificationDIConfigurator _configurator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The env.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            Configuration = NotificationServiceCollection.InitAppSetting(Configuration, env);
        }

        /// <summary>
        /// The configuration.
        /// </summary>
        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.        
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services) {
            services.AddHostedService<EmailNotificationManager>();

            services.AddHostedService<SMSNotificationManager>();

            // System http context for Session Manager
            services.AddHttpContextAccessor();

            services.ConfigureNotificationAppSettings(Configuration);

            // Core Services
            services.ConfigureCoreServiceDependencies(Configuration);

            // AppPortal DS
            services.AddNotificationDataServiceDependency();

            // AppPortal Data
            services.AddNotificationDataDependency();

            // AppPortal Other
            services.AddNotificationOtherDependency(Configuration);

            // Response Compression
            services.ConfigureResponseCompressionService();

            // Authenticatin
            services.ConfigureAuthenticationService(Configuration);

            // CORS
            services.ConfigurationCORSService();

            // Swagger
            services.ConfigureSwaggerService();

            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        public void Stop(object obj) {
            Serilog.Log.Information("Stop event.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime) {
            appLifetime.ApplicationStarted.Register(OnAppStarted);

            appLifetime.ApplicationStopping.Register(OnAppStopping);

            appLifetime.ApplicationStopped.Register(OnAppStopped);
            //_configurator.Configure(Configuration, app, env);

            //// System
            //app.UseHsts();

            //// System MVC
            //app.UseMvc();

            //app.UseHttpsRedirection();

            // Response Compression
            app.ConfigureResponseCompressionMiddleware();

            // AppPortal
            app.AddNotificationMiddleware();

            // Authentication
            app.ConfigurationAuthentication();

            // Swagger
            app.ConfigureSwagger();

            // CORS
            app.ConfigureCORS();

            // Default Web API
            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseMvc();
        }

        #region Api Life Events
        public void OnAppStarted() {
            string contents = $"Notification API started at {DateTime.Now} - {TimeZoneInfo.Local.StandardName}";
            Serilog.Log.Information(contents);
        }


        public void OnAppStopping() {
            string contents = $"Notification API stopping at {DateTime.Now} - {TimeZoneInfo.Local.StandardName}";
            Serilog.Log.Information(contents);
        }

        public void OnAppStopped() {
            string contents = $"Notification API stopped at {DateTime.Now} - {TimeZoneInfo.Local.StandardName}";
            Serilog.Log.Information(contents);
        } 
        #endregion
    }
}
