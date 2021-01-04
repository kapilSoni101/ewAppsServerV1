using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppManagement.DI;
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace AppManagement.WebApi {
    public class Startup {
        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            Configuration = AppMgmtCollection.InitAppSetting(Configuration, env);
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.ConfigureAppMgmtAppSettings(Configuration);

            services.ConfigureCoreServiceDependencies(Configuration);

            services.AddAppMgmtDataServiceDependency();

            services.AddAppMgmtDataDependency();

            services.AddAppMgmtOtherDependency(Configuration);

            services.ConfigureResponseCompressionService();

            services.ConfigureAuthenticationService(Configuration);

            services.ConfigurationCORSService();

            services.ConfigureSwaggerService();

            //DM Dependency
            services.AddDMDependency(Configuration);

            services.AddAllReportDependency(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

            app.ConfigureResponseCompressionMiddleware();

            app.AddAppMgmtMiddleware();

            app.ConfigurationAuthentication();

            app.ConfigureSwagger();

            app.ConfigureCORS();

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
