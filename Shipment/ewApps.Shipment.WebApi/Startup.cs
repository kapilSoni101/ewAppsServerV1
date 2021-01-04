using ewApps.Core.Services.DI;
using ewApps.Shipment.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace ewApps.Shipment.WebApi {
    public class Startup {
        DIConfigurationModel _diConfigurationModel;

        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            Configuration = ShipmentServiceCollection.InitAppSetting(configuration, env);
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.ConfigureShipmentAppSettings(Configuration);

            services.ConfigureCoreServiceDependencies(Configuration);

            services.AddShipmentDataServiceDependency();

            services.AddShipmentDataDependency();

            services.AddShipmentOtherDependency(Configuration);

            services.ConfigureResponseCompressionService();

            services.ConfigureAuthenticationService(Configuration);

            services.ConfigurationCORSService();

            services.ConfigureSwaggerService();

            //services.AddAllReportDependency(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //SignalR Dependancies
            // services.AddTransient<IRealTimeUpdate, AppRealTimeUpdate<AppHub>>();
            services.AddSignalR(config => { config.EnableDetailedErrors = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.ConfigureResponseCompressionMiddleware();

            app.AddShipmentMiddleware();

            app.ConfigurationAuthentication();

            app.ConfigureSwagger();

            app.ConfigureCORS();

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
