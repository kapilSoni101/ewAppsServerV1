/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */
using ewApps.Core.DI;
using ewApps.Core.SerilogLoggingService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace ewApps.Report.WebApi {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration {
      get;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      ////Cusome Logging
      //services.AddLogging(
      //        builder => {
      //          builder.AddFilter("Microsoft", LogLevel.None)
      //                       .AddFilter("System", LogLevel.None)
      //                       .AddFilter("NToastNotify", LogLevel.None);
      //        });


      // System Cors Dependency for Cross Origin


      //services.AddCors(o => o.AddPolicy("AllowAllOrigins",
      //        builder => {
      //          builder.AllowAnyOrigin().AllowAnyMethod().WithExposedHeaders("content-disposition").AllowAnyHeader().AllowCredentials().SetPreflightMaxAge(TimeSpan.FromSeconds(86800));
      //          //services.AddCors(o => o.AddPolicy("AllowSpecificOrigin",
      //          //      builder => {
      //          //        builder.WithOrigins("http://localhost:4202").AllowAnyMethod().WithExposedHeaders("content-disposition").AllowAnyHeader().AllowCredentials().SetPreflightMaxAge(TimeSpan.FromSeconds(86800));
      //        }));


      // System MVC depencency
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
      }); ;


      // Core Dependency 
      //services.AddCoreDependency();

      // Report Dependency
      //services.AddReportDependency();

      // System http context for Session Manager
      services.AddHttpContextAccessor();

      //// Get App Settings for Identity Server Url
      //AppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>().Value;

      //// Identity Server Url
      //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
      //                    .AddIdentityServerAuthentication(options => {
      //                      options.Authority = appSettings.Authentication.IdentityServerUrl;
      //                      options.RequireHttpsMetadata = false;
      //                      options.ApiName = appSettings.AppName;
      //                    });

      //// System Cors Dependency for Cross Origin
      //services.AddCors(o => o.AddPolicy("AllowSpecificOrigin",
      //                builder => {
      //                  builder.WithOrigins(appSettings.SpecificOriginsUrl.OriginUrls).AllowAnyMethod().WithExposedHeaders("content-disposition").AllowAnyHeader().AllowCredentials().SetPreflightMaxAge(TimeSpan.FromSeconds(86800));
      //                }));

      // User session Dependency
      services.AddUserSessionDependency(Configuration);


      // Register the Swagger generator, defining 1 or more Swagger documents
      //services.AddSwaggerGen(c => {
      //  c.SwaggerDoc("v1", new Info {
      //    Version = "v1",
      //    Title = "iPayment API",
      //    Description = "Payment Portal Web API",
      //    TermsOfService = "None",
      //    Contact = new Contact {
      //      Name = "eWorkplace Apps",
      //      Email = "support@eworkplaceapps.com",
      //      Url = "https://eworkplaceapps.com"
      //    },
      //    License = new License {
      //      Name = "Use under LICX",
      //      Url = "https://example.com/license"
      //    }
      //  });

      //  // Set the comments path for the Swagger JSON and UI.
      //  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      //  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      //  c.IncludeXmlComments(xmlPath);

      //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      //Serilog Middleware
      app.UseMiddleware<SerilogHttpMiddleware>(Options.Create(new Core.SerilogLoggingService.SerilogHttpMiddlewareOptions() {
        EnableExceptionLogging = false
      }));

      if(env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }
      else {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
