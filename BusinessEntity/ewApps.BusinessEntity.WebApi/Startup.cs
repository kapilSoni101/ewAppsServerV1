using ewApps.BusinessEntity.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace BusinessEntity.WebApi {
  public class Startup {

    public Startup(IConfiguration configuration, IHostingEnvironment env) {
      Configuration = configuration;
      Configuration = BusinessEntityCollection.InitAppSetting(Configuration, env);
    }

    public IConfiguration Configuration {
      get;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      services.ConfigureBusinessEntityAppSettings(Configuration);

      services.ConfigureCoreServiceDependencies(Configuration);

      services.AddBusinessEntityDataServiceDependency();

      services.AddBusinessEntityDataDependency();

      services.AddBusinessEntityOtherDependency(Configuration);

      services.AddFactoryServiceDependency(Configuration);

      services.ConfigureResponseCompressionService();

      services.ConfigureAuthenticationService(Configuration);

      services.ConfigurationCORSService();

      services.ConfigureSwaggerService();

      services.AddAllReportDependency(Configuration);

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      app.ConfigureResponseCompressionMiddleware();

      app.AddBusinessEntityMiddleware();

      app.ConfigurationAuthentication();

      app.ConfigureSwagger();

      app.ConfigureCORS();

      app.UseHsts();

      app.UseHttpsRedirection();

      app.UseMvc();
    }
  }
}
