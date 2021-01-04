/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.IO;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;


namespace ewApps.Core.UserManagement
{

  /// <summary>
  /// 
  /// </summary>
  public class Startup
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration Configuration
    {
      get;
    }


    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {

      // System MVC depencency
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions((options) => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); }); ;

      // System Cors Dependency for Cross Origin
      services.AddCors();



      // Core Dependency 
     // services.AddCoreDependency();

      // Publisher Dependency
     // services.AddPaymentDependency();

      // System http context for Session Manager
      services.AddHttpContextAccessor();

      // Get App Settings for Identity Server Url
      // AppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>().Value;

      // Identity Server Url
      /* services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                           .AddIdentityServerAuthentication(options => {
                             options.Authority = appSettings.Authentication.IdentityServerUrl;
                             options.RequireHttpsMetadata = false;
                             options.ApiName = appSettings.AppName;
                           });*/

      // User session Dependency
      // services.AddUserSessionDependency();



      // Register the Swagger generator, defining 1 or more Swagger documents
      /*  services.AddSwaggerGen(c => {
          c.SwaggerDoc("v1", new Info
          {
            Version = "v1",
            Title = "iPayment API",
            Description = "Payment Portal Web API",
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

        });*/
      }
  

      /// <summary>
      /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      /// </summary>
      /// <param name="app"></param>
      /// <param name="env"></param>
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      // System Cors middleware for allow client cross origin
      app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


      // System Identity Server Middleware
      app.UseAuthentication();

      
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), 
      // Enable middleware to serve generated Swagger as a JSON endpoint.            
      // specifying the Swagger JSON endpoint.
    //  app.UseSwagger();
     /* app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publisher Portal");
        c.RoutePrefix = string.Empty;
      });*/

      // System http redirection
      app.UseHttpsRedirection();

      // System MVC
      app.UseMvc(

      );
    }
  }
}
