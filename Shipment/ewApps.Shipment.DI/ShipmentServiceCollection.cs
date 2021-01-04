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
using ewApps.Core.DI;
using ewApps.Core.ExceptionService;
using ewApps.Core.SerilogLoggingService;
using ewApps.Core.Services.DI;
using ewApps.Core.UserSessionService;
//using ewApps.Report.DI;
using ewApps.Shipment.Common;
using ewApps.Shipment.QData;
using ewApps.Shipment.Data;
using ewApps.Shipment.DS;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ewApps.Shipment.DI {

    public static class ShipmentServiceCollection {

        #region Shipment Dependency Methods

        //Declairing All Data Class Of Shipment to Create Its Instance on Runtime Through Dependency Injection
        public static IServiceCollection AddShipmentDataDependency(this IServiceCollection services) {

            services.AddTransient<IShipmentUnitOfWork, ShipmentUnitOfWork>(x =>
                      new ShipmentUnitOfWork(x.GetRequiredService<ShipmentDbContext>(),
                                             false
                                            ));

            services.AddDbContext<ShipmentDbContext>();
            services.AddScoped<IPackageMasterRepository, PackageMasterRepository>();
            services.AddScoped<ISalesOrderPkgItemRepository, SalesOrderPkgDetailRepository>();
            services.AddScoped<IShipmentItemRepository, ShipmentItemRepository>();
            services.AddScoped<IShipmentPkgItemRepository, ShipmentPkgItemRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<IVerifiedAddressRepository, VerifiedAddressRepository>();
            services.AddScoped<ISalesOrderPkgRepository, SalesOrderPkgRepository>();
            //services.AddTransient<IShipmentUnitOfWork, ShipmentUnitOfWork>(x =>
            //                       new ShipmentUnitOfWork(x.GetRequiredService<ShipmentDbContext>(), x.GetRequiredService<IUnitOfWork>(),
            //                        false
            //                       ));
            services.AddScoped<IShipmentPkgRepository, ShipmentPkgRepository>();
            services.AddScoped<ICarrierPackageLinkingRepository, CarrierPackageDetailRepository>();
            services.AddScoped<IFavourateShipmentPkgSettingRepository, FavourateShipmentPkgSettingRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleLinkingRepository, RoleLinkingRepository>();
            services.AddScoped<ITenantUserAppPreferenceRepository, TenantUserAppPreferenceRepository>();

            services.AddDbContext<QShipmentDbContext>();

            return services;
        }

        //Declairing All Data Service Class Of Shipment to Create Its Instance on Runtime Through Dependency Injection
        public static IServiceCollection AddShipmentDataServiceDependency(this IServiceCollection services) {
            services.AddScoped<IPackageMasterDS, PackageMasterDS>();
            services.AddScoped<ISalesOrderPkgItemDS, SalesOrderPkgItemDS>();
            services.AddScoped<IShipmentDS, ShipmentDS>();
            services.AddScoped<IShipmentItemDS, ShipmentItemDS>();
            services.AddScoped<IShipmentPkgItemDS, ShipmentPkgItemDS>();
            services.AddScoped<IVerifiedAddressDS, VerifiedAddressDS>();
            services.AddScoped<ISalesOrderPkgDS, SalesOrderPkgDS>();
            services.AddScoped<IShipmentPkgDS, ShipmentPkgDS>();
            services.AddScoped<ICarrierPackageDetailDS, CarrierPackageDetailDS>();
            services.AddScoped<IFavouriteShipmentPkgSettingDS, FavouriteShipmentPkgSettingDS>();
            services.AddScoped<IRoleDS, RoleDS>();
            services.AddScoped<IRoleLinkingDS, RoleLinkingDS>();
            services.AddScoped<ITenantUserAppPreferenceDS, TenantUserAppPreferenceDS>();
            services.AddScoped<ICarrierAccountDS, CarrierAccountDS>();
            services.AddScoped<IBusUserAppManagmentDS, BusUserAppManagmentDS>();
            //


            return services;
        }

        public static IServiceCollection AddShipmentOtherDependency(this IServiceCollection services, IConfiguration configuration) {
            return services;
        }

        //public static IServiceCollection AddAllReportDependency(this IServiceCollection services, IConfiguration configuration) {
        //    return services.AddReportDependency(configuration);
        //}

        public static IServiceCollection ConfigureCoreServiceDependencies(this IServiceCollection serivces, IConfiguration configuration) {
            CoreServiceDIOptions coreServiceDIOptions = new CoreServiceDIOptions();
            coreServiceDIOptions.SessionOptions = new UserSessionOptions();
            coreServiceDIOptions.SessionOptions.RemoteSession = true;
            CoreServiceEnum includedCoreServices = CoreServiceEnum.ConnectionManager | CoreServiceEnum.EmailService
            | CoreServiceEnum.ExceptionService | CoreServiceEnum.NotificationService | CoreServiceEnum.ScheduledJobService
            | CoreServiceEnum.SerilogLoggingService | CoreServiceEnum.SMSService | CoreServiceEnum.DMService
            | CoreServiceEnum.UserSessionService | CoreServiceEnum.UniqueIdentityGenerator;
            return serivces.ConfigureCoreServices(configuration, includedCoreServices, coreServiceDIOptions);
        }

        public static IServiceCollection ConfigureShipmentAppSettings(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<ShipmentAppSettings>(configuration);
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

        public static IApplicationBuilder AddShipmentMiddleware(this IApplicationBuilder app) {
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

        #endregion

        #region Authentication Dependencies

        public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration) {

            // Identity Server Url
            ShipmentAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ShipmentAppSettings>>().Value;
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
            ShipmentAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ShipmentAppSettings>>().Value;
            //System Cors Dependency for Cross Origin      
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
                    Title = "Shipment Service",
                    Description = "Shipment Portal Web API",
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
                //   c.OperationFilter<SwaggerUIRequestHeaderFilter>();

            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app) {
            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shipment Service");
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