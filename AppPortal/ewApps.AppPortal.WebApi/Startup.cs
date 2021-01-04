using ewApps.AppPortal.DI;
using ewApps.AppPortal.WebApi;
using ewApps.Core.DMService;
using ewApps.Core.SignalRService;
using ewApps.Report.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace AppPortal.WebApi {

    public class Startup {

        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            Configuration = AppPortalServiceCollection.InitAppSetting(Configuration, env);
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // AppSettings
            services.ConfigureAppPortalAppSettings(Configuration);

            // Core Services
            services.ConfigureCoreServiceDependencies(Configuration);
            
            // AppPortal DS
            services.AddAppPortalDataServiceDependency();

            //DM Dependency
            services.AddDMDependency(Configuration);

            // AppPortal QDS
            services.AddAppPortalQDataServiceDependency();

            // AppPortal Data
            services.AddAppPortalDataDependency();

            // AppPortal QData
            services.AddAppPortalQDataDependency();

            // AppPortal Other
            services.AddAppPortalOtherDependency(Configuration);           

            // Response Compression
            services.ConfigureResponseCompressionService();

            // Authenticatin
            services.ConfigureAuthenticationService(Configuration);

            // CORS
            services.ConfigurationCORSService();

            services.ConfigureSwaggerService();

            //Report
            services.AddReportDependency(Configuration);
            
            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //SignalR Dependancies
            services.AddTransient<IRealTimeUpdate, AppRealTimeUpdate<AppHub>>();
            services.AddSignalR(config => { config.EnableDetailedErrors = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

            // Response Compression
            app.ConfigureResponseCompressionMiddleware();

            // AppPortal
            app.AddAppPortalMiddleware();

            // Authentication
            app.ConfigurationAuthentication();

            // Swagger
            app.ConfigureSwagger();

            // CORS
            app.ConfigureCORS();


            // SignalR
            app.UseSignalR(options => {
                options.MapHub<AppHub>("/paymenthub");
            });

            // Default Web API
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
