using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ewApps.Core.CacheService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ewApps.Core.RedisLUAServer
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      CacheConfigurationOptions cacheOptions = CreateCacheConfiguration();
      services.AddScoped<IConnectionFactory>(conn => new ConnectionFactory(cacheOptions));
      services.AddScoped<IScriptService, RedisScriptService>();
      services.AddScoped<ILUAScriptExecutor,LUAScriptExecutor>();

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new Info
        {
          Version = "v1",
          Title = "Redis LUA Script API",
          Description = "Redis LUA Script API",
          TermsOfService = "None",
          Contact = new Contact
          {
            Name = "eWorkplace Apps",
            Email = "support@eworkplaceapps.com",
            Url = "https://eworkplaceapps.com"
          },
          License = new License
          {
            Name = "Use under LICX",
            Url = "https://example.com/license"
          }
        });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

      });
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

  
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }



      app.UseHttpsRedirection();
      // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), 
      // Enable middleware to serve generated Swagger as a JSON endpoint.            
      // specifying the Swagge r JSON endpoint.
      app.UseSwagger();
      app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publisher Portal");
        c.RoutePrefix = string.Empty;
      });

      app.UseMvc();
    }

    private CacheConfigurationOptions CreateCacheConfiguration()
    {
      //Read the configuration from config file, right now it is hard coded
      CacheConfigurationOptions cacheOptions = new CacheConfigurationOptions();
      cacheOptions.Host = "127.0.0.1";
      cacheOptions.Port = 6379;
      cacheOptions.CacheType = "Redis";
      cacheOptions.InstanceName = "";
      return cacheOptions;
    }
  }
}
