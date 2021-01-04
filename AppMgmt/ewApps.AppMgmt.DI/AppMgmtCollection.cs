using System;
using System.IO;
using System.Reflection;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.QData;
using ewApps.Core.DI;
using ewApps.Core.ExceptionService;
using ewApps.Core.SerilogLoggingService;
using ewApps.Core.Services.DI;
using ewApps.Core.UserSessionService;
using ewApps.Report.DI;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AppManagement.DI {
    public static class AppMgmtCollection {

        #region App Mgmt Dependency Methods


        public static IServiceCollection AddBusinessEntityDependency(this IServiceCollection services, IConfiguration Configuration) {
            AddAppMgmtDataServiceDependency(services);
            AddAppMgmtDataDependency(services);
            AddAppMgmtOtherDependency(services, Configuration);
            return services;
        }

        public static IServiceCollection AddAppMgmtDataServiceDependency(this IServiceCollection services) {
            services.AddScoped<ISessionMgmtDS, SessionMgmtDS>();
            services.AddScoped<ITenantDS, TenantDS>();
            services.AddScoped<IBrandingDS, BrandingDS>();
            services.AddScoped<ITenantSignUpForPublisherDS, TenantSignUpForPublisherDS>();
            services.AddScoped<ITenantUserDS, TenantUserDS>();
            services.AddScoped<IUserTenantLinkingDS, UserTenantLinkingDS>();
            services.AddScoped<IIdentityServerDS, IdentityServerDS>();
            services.AddScoped<ITenantUserAppLinkingDS, TenantUserAppLinkingDS>();
            services.AddScoped<IThemeDS, ThemeDS>();
            services.AddScoped<IAppDS, AppDS>();
            services.AddScoped<ITenantLinkingDS, TenantLinkingDS>();
            services.AddScoped<ITenantSubscriptionDS, TenantSubscriptionDS>();
            services.AddScoped<ITenantSignUpForBusinessDS, TenantSignUpForBusinessDS>();
            services.AddScoped<ITenantForBusinessDS, TenantForBusinessDS>();
            services.AddScoped<ITenantUpdateForBusinessDS, TenantUpdateForBusinessDS>();
            services.AddScoped<IServiceAccountDetailDS, ServiceAccountDetailDS>();
            services.AddScoped<ITenantAppServiceLinkingDS, TenantAppServiceLinkingDS>();
            services.AddScoped<ITenantSignUpForCustomerDS, TenantSignUpForCustomerDS>();
            services.AddScoped<IAppServiceAttributeDS, AppServiceAttributeDS>();
            services.AddScoped<IAppServiceDS, AppServiceDS>();
            services.AddScoped<ICustomerAppServiceLinkingDS, CustomerAppServiceLinkingDS>();
            services.AddScoped<ISubscriptionPlanDS, SubscriptionPlanDS>();
            services.AddScoped<ISubscriptionPlanServiceDS, SubscriptionPlanServiceDS>();
            services.AddScoped<ISubscriptionPlanServiceAttributeDS, SubscriptionPlanServiceAttributeDS>();
            services.AddScoped<IConfigurationDS, ConfigurationDS>();
            services.AddScoped<ITenantUserStatusDS, TenantUserStatusDS>();
            services.AddScoped<ITenantUserAppLastAccessInfoDS, TenantUserAppLastAccessInfoDS>();
            services.AddScoped<IQSubscriptionPlanDS, QSubscriptionPlanDS>();
            services.AddScoped<ITenantUpdateForPublisherDS, TenantUpdateForPublisherDS>();
            services.AddScoped<ITenantUpdateForCustomerDS, TenantUpdateForCustomerDS>();
      services.AddScoped<ITenantUpdateForVendorDS, TenantUpdateForVendorDS>();

      #region TenantUser

      #region Platfrom

      services.AddScoped<ITenantUserForPlatformDS, TenantUserForPlatformDS>();
            services.AddScoped<ITenantUserSignUpForPlatformDS, TenantUserSignUpForPlatformDS>();
            services.AddScoped<ITenantUserUpdateForPlatformDS, TenantUserUpdateForPlatformDS>();
            services.AddScoped<ITenantUserDeleteForPlatformDS, TenantUserDeleteForPlatformDS>();

            #endregion Platfrom

            #region Publisher

            services.AddScoped<ITenantUserForPublisherDS, TenantUserForPublisherDS>();
            services.AddScoped<ITenantUserSignUpForPublisherDS, TenantUserSignUpForPublisherDS>();
            services.AddScoped<ITenantUserUpdateForPublisherDS, TenantUserUpdateForPublisherDS>();
            services.AddScoped<ITenantUserDeleteForPublisherDS, TenantUserDeleteForPublisherDS>();

            #endregion Publisher

            #region Business
           
            services.AddScoped<ITenantUserSignUpForBusiness, TenantUserSignUpForBusiness>();
            services.AddScoped<ITenantUserUpdateForBusinessDS, TenantUserUpdateForBusinessDS>();
            services.AddScoped<ITenantUserDeleteForBusinessDS, TenantUserDeleteForBusinessDS>();

            #endregion Business

            #region Customer

            services.AddScoped<ITenantUserSignUpForCustomer, TenantUserSignUpForCustomer>();
            services.AddScoped<ITenantUserDeleteForCustomerDS, TenantUserDeleteForCustomerDS>();
            services.AddScoped<ITenantUserUpdateForCustomerDS, TenantUserUpdateForCustomerDS>();

            #endregion Customer

            services.AddScoped<ITenantUserExtDS, TenantUserExtDS>();

            #endregion TenantUser

            return services;
        }

        public static IServiceCollection AddAppMgmtDataDependency(this IServiceCollection services) {
            services.AddDbContext<AppMgmtDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>(x =>
                      new UnitOfWork(x.GetRequiredService<AppMgmtDbContext>(),
                                             false
                                            ));
            services.AddDbContext<QAppMgmtDbContext>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IThemeRepository, ThemeRepository>();
            services.AddScoped<ITenantUserRepository, TenantUserRepository>();
            services.AddScoped<IUserTenantLinkingRepository, UserTenantLinkingRepository>();
            services.AddScoped<ITenantUserAppLinkingRepository, TenantUserAppLinkingRepository>();
            services.AddScoped<IAppRepository, AppRepository>();
            services.AddScoped<ITenantLinkingRepository, TenantLinkingRepository>();
            services.AddScoped<ITenantSubscriptionRepository, TenantSubscriptionRepository>();
            services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
            services.AddScoped<IAppServiceAccountDetailRepository, AppServiceAccountDetailRepository>();
            services.AddScoped<ITenantAppServiceLinkingRepository, TenantAppServiceLinkingRepository>();
            services.AddScoped<IAppServiceRepository, AppServiceRepository>();
            services.AddScoped<IAppServiceAttributeRepository, AppServiceAttributeRepository>();
            services.AddScoped<ICustomerAppServiceLinkingRepository, CustomerAppServiceLinkingRepository>();
            services.AddScoped<ITenantUserAppLastAccessInfoRepository, TenantUserAppLastAccessInfoRepository>();
            services.AddScoped<ISubscriptionPlanServiceRepository, SubscriptionPlanServiceRepository>();
            services.AddScoped<ISubscriptionPlanServiceAttributeRepository, SubscriptionPlanServiceAttributeRepository>();
            services.AddScoped<IQSubscriptionPlanRepository, QSubscriptionPlanRepository>();
      
            return services;
        }

        public static IServiceCollection AddAppMgmtOtherDependency(this IServiceCollection services, IConfiguration Configuration) {
            return services;
        }

        public static IServiceCollection AddAllReportDependency(this IServiceCollection services, IConfiguration configuration) {
            return services.AddReportDependency(configuration);
        }

        public static IServiceCollection ConfigureCoreServiceDependencies(this IServiceCollection serivces, IConfiguration configuration) {
            CoreServiceDIOptions coreServiceDIOptions = new CoreServiceDIOptions();
            coreServiceDIOptions.SessionOptions = new UserSessionOptions();
            coreServiceDIOptions.SessionOptions.RemoteSession = false;
            CoreServiceEnum includedCoreServices = CoreServiceEnum.ConnectionManager | CoreServiceEnum.EmailService
          | CoreServiceEnum.ExceptionService | CoreServiceEnum.NotificationService | CoreServiceEnum.ScheduledJobService
          | CoreServiceEnum.SerilogLoggingService | CoreServiceEnum.SMSService | CoreServiceEnum.DMService
          | CoreServiceEnum.UserSessionService | CoreServiceEnum.UniqueIdentityGenerator;

            return serivces.ConfigureCoreServices(configuration, includedCoreServices, coreServiceDIOptions);
        }

        public static IServiceCollection ConfigureAppMgmtAppSettings(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<AppMgmtAppSettings>(configuration);
            return services;
        }

        public static IConfiguration InitAppSetting(IConfiguration configuration, IHostingEnvironment env) {
            string envName = env.EnvironmentName;
            IConfigurationBuilder builder = new ConfigurationBuilder()
             .AddConfiguration(configuration)
             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings"))
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
             .AddEnvironmentVariables();
            return builder.Build();
        }

        public static IApplicationBuilder AddAppMgmtMiddleware(this IApplicationBuilder app) {
            app.AddExceptionMiddleware();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("AllowSpecificOrigin");

            // Custome User session Middelware
            app.AddUserSessionMiddleware(new UserSessionOptions() { LightSession = false });

            //Serilog Middleware
            app.UseMiddleware<SerilogHttpMiddleware>(Options.Create(new SerilogHttpMiddlewareOptions() {
                EnableExceptionLogging = false
            }));

            return app;
        }
        #endregion

        #region Authentication Dependencies

        public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration) {
            // Get App Settings for Identity Server Url
            AppMgmtAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppMgmtAppSettings>>().Value;

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

        #endregion

        #region CORS Dependencies

        public static IServiceCollection ConfigurationCORSService(this IServiceCollection services) {
            // Get App Settings for Identity Server Url
            AppMgmtAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppMgmtAppSettings>>().Value;

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

        #endregion

        #region Swagger Dependencies

        public static IServiceCollection ConfigureSwaggerService(this IServiceCollection services) {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "App Managment API",
                    Description = "App Managment Web API",
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Entity Service");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
        #endregion

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