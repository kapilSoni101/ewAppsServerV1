using System;
using System.IO;
using System.Reflection;
using ewApps.Core.DI;
using ewApps.Core.ExceptionService;
using ewApps.Core.SerilogLoggingService;
using ewApps.Core.Services.DI;
using ewApps.Core.UserSessionService;
using ewApps.Notification.Common;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using License = Swashbuckle.AspNetCore.Swagger.License;

namespace ewApps.Notification.DI {
    public static class NotificationServiceCollection {

        public static IConfiguration InitAppSetting(IConfiguration configuration, IHostingEnvironment env) {
            string envName = env.EnvironmentName;
            IConfigurationBuilder builder = new ConfigurationBuilder()
             .AddConfiguration(configuration)
             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings"))
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
             .AddEnvironmentVariables();
            return builder.Build();
        }

        public static IServiceCollection ConfigureNotificationAppSettings(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<NotificationAppSettings>(configuration);

            return services;
        }

        /// <summary>
        /// This method will add data service depenency
        /// </summary>
        /// <param name="services">An instance of <see cref="IServiceCollection"/> to add DI contract of dependencies.</param>
        /// <returns>Returns updated instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNotificationDataServiceDependency(this IServiceCollection services) {
            return services;
        }

        /// <summary>
        /// This method will add data layer depenency
        /// </summary>
        public static IServiceCollection AddNotificationDataDependency(this IServiceCollection services) {
                   return services;
        }

        /// <summary>
        /// This method will add other dependency like app settings, automapper etc.
        /// </summary>
        /// <param name="services">An instance of <see cref="IServiceCollection"/> to add DI contract of dependencies.</param>
        /// <param name="configuration">An instance of <see cref="IConfiguration"/> to get and set application configuration.</param>
        /// <returns>Returns updated instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNotificationOtherDependency(this IServiceCollection services, IConfiguration configuration) {
           
            return services;
        }

        public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration) {
            // Get App Settings for Identity Server Url
            NotificationAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<NotificationAppSettings>>().Value;

            // Identity Server Url
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                                .AddIdentityServerAuthentication(options => {
                                    options.Authority = appSettings.IdentityServerUrl;
                                    options.RequireHttpsMetadata = false;
                                    options.ApiName = appSettings.AppName;
                                });

            return services;
        }

        public static IApplicationBuilder ConfigurationAuthentication(this IApplicationBuilder app) {
            // System Identity Server Middleware
            app.UseAuthentication();
            return app;
        }

        public static IServiceCollection ConfigurationCORSService(this IServiceCollection services) {
            // Get App Settings for Identity Server Url
            NotificationAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<NotificationAppSettings>>().Value;

            // System Cors Dependency for Cross Origin      
            services.AddCors(o => o.AddPolicy("AllowSpecificOrigin",
                builder => {
                    builder.SetIsOriginAllowedToAllowWildcardSubdomains().WithOrigins(appSettings.CrossOriginsUrls).AllowAnyMethod().WithExposedHeaders("content-disposition").AllowAnyHeader().AllowCredentials().SetPreflightMaxAge(TimeSpan.FromSeconds(86800));
                }));

            return services;
        }

        public static IApplicationBuilder ConfigureCORS(this IApplicationBuilder app) {
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("AllowSpecificOrigin");
            return app;
        }

        public static IServiceCollection ConfigureSwaggerService(this IServiceCollection services) {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "Notification API",
                    Description = "Notification Portal Web API",
                    TermsOfService = "None",
                    Contact = new Contact {
                        Name = "eWorkplace Apps",
                        Email = "support@eworkplaceapps.com",
                        Url = "https://eworkplaceapps.com"
                    },
                    License = new License {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                //  c.OperationFilter<SwaggerUIRequestHeaderFilter>();

            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app) {
            // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), 
            // Enable middleware to serve generated Swagger as a JSON endpoint.            
            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Portal");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }

        /// <summary>
        /// Adds the notification api middleware(s).
        /// </summary>
        /// <param name="app">The instance of application builder to append middleware(s).</param>
        /// <returns>Returns updated application budiler instance.</returns>
        public static IApplicationBuilder AddNotificationMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("AllowSpecificOrigin");

            // User Session
            app.AddUserSessionMiddleware(new UserSessionOptions() { LightSession = false });

            //Serilog Middleware
            app.UseMiddleware<SerilogHttpMiddleware>(Options.Create(new SerilogHttpMiddlewareOptions() {
                EnableExceptionLogging = false
            }));

            return app;
        }

        public static IServiceCollection ConfigureCoreServiceDependencies(this IServiceCollection serivces, IConfiguration configuration) {
            CoreServiceDIOptions coreServiceDIOptions = new CoreServiceDIOptions();
            coreServiceDIOptions.SessionOptions = new UserSessionOptions();
            coreServiceDIOptions.SessionOptions.RemoteSession = false;
            CoreServiceEnum includedCoreServices = CoreServiceEnum.EmailService
                | CoreServiceEnum.ExceptionService | CoreServiceEnum.ScheduledJobService
                | CoreServiceEnum.SerilogLoggingService | CoreServiceEnum.SMSService
                | CoreServiceEnum.UserSessionService;

            return serivces.ConfigureCoreServices(configuration, includedCoreServices, coreServiceDIOptions);
        }

        #region Response Compression Dependencies

        public static IApplicationBuilder ConfigureResponseCompressionMiddleware(this IApplicationBuilder app) {
            app.UseResponseCompression();
            return app;
        }

        public static IServiceCollection ConfigureResponseCompressionService(this IServiceCollection services) {
            services.AddResponseCompression(options => {
                options.EnableForHttps = true;
            });

            return services;
        }

        #endregion

    }
}
