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
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.DS;
using ewApps.Payment.QData;

using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ewApps.Payment.DI {

    public static class PaymentServiceCollection {

        #region Payment Dependency Methods

        public static IServiceCollection AddPaymentDataDependency(this IServiceCollection services) {
            services.AddDbContext<PaymentDbContext>();
            services.AddDbContext<QPaymentDBContext>();
            services.AddScoped<IPaymentInvoiceLinkingRepository, PaymentInvoiceLinkingRepository>();           
            services.AddTransient<IPaymentUnitOfWork, PaymentUnitOfWork>(x =>
                              new PaymentUnitOfWork(x.GetRequiredService<PaymentDbContext>(), 
                               false
                              ));          

            services.AddScoped<IRecurringPaymentRepository, RecurringPaymentRepository>();
            services.AddScoped<IRecurringPaymentLogRepository, RecurringPaymentLogRepository>();
            services.AddScoped<IPaymentLogRepository, PaymentLogRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleLinkingRepository, RoleLinkingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IQPaymentRepository, QPaymentRepository>();
            services.AddScoped<IQInvoiceRepository, QInvoiceRepository>();
            services.AddScoped<ITenantUserAppPreferenceRepository, TenantUserAppPreferenceRepository>();
            services.AddScoped<IQPayAppServiceRepository, QPayAppServiceRepository>();
            services.AddScoped<IQInvoiceItemRepository, QInvoiceItemRepository>();
            services.AddScoped<IPreAuthPaymentRepository, PreAuthPaymentRepository>();
            services.AddScoped<IQPreAuthPaymentRepository, QPreAuthPaymentRepository>();
            services.AddScoped<IQAPInvoiceRepository, QAPInvoiceRepository>();

            services.AddScoped<IQPaymentNotificationData, QPaymentNotificationData>();
            services.AddScoped<IASNotificationRepository, ASNotificationRepository>();

            return services;
        }

        public static IServiceCollection AddPaymentDataServiceDependency(this IServiceCollection services) {
            services.AddScoped<IPaymentDataService, PaymentDataService>();
            services.AddScoped<IPaymentInvoiceLinkingDataServices, PaymentInvoiceLinkingDataServices>();
            services.AddScoped<IPaymentAccess, PaymentAccess>();
            services.AddScoped<IRecurringPaymentDS, RecurringPaymentDS>();
            services.AddScoped<IRecurringPaymentLogDS, RecurringPaymentLogDS>();
            services.AddScoped<IPaymentLogDS, PaymentLogDS>();
            services.AddScoped<IRoleDS, RoleDS>();
            services.AddScoped<IRoleLinkingDS, RoleLinkingDS>();
            services.AddScoped<IQPaymentDS, QPaymentDS>();
            services.AddScoped<IQInvoiceDS, QInvoiceDS>();
            services.AddScoped<ITenantUserAppPreferenceDS, TenantUserAppPreferenceDS>();
            services.AddScoped<IPaymentAndInvoiceDS, PaymentAndInvoiceDS>();
            services.AddScoped<IPayAppServiceDS, PayAppServiceDS>();
            services.AddScoped<IQPayAppServiceDS, QPayAppServiceDS>();            
            services.AddScoped<ICreditCardAccountDS, CreditCardAccountDS>();
            services.AddScoped<IQInvoiceItemDS, QInvoiceItemDS>();
            services.AddScoped<IBusUserAppManagmentDS, BusUserAppManagmentDS>();
            services.AddScoped<ICustUserAppManagmentDS, CustUserAppManagmentDS>();
            services.AddScoped<IPreAuthPaymentDS, PreAuthPaymentDS>();
            services.AddScoped<IQPreAuthPaymentDS, QPreAuthPaymentDS>();
            services.AddScoped<IQAPInvoiceDS, QAPInvoiceDS>();

            services.AddScoped<IPaymentNotificationHandler, PaymentNotificationHandler>();
            services.AddScoped<IPaymentNotificationService, PaymentNotificationService>();
            services.AddScoped<IPaymentNotificationRecipientDS, PaymentNotificationRecipientDS>();
            services.AddScoped<IASNotificationDS, ASNotificationDS>();
            services.AddScoped<IVendorPaymentAndInvoiceDS, VendorPaymentAndInvoiceDS>();

            return services;
        }

        public static IServiceCollection AddPaymentOtherDependency(this IServiceCollection services, IConfiguration configuration) {
            //services.Configure<PaymentAppSettings>(configuration);
            //// Automapper
            var config = new AutoMapper.MapperConfiguration(cfg => {
                //cfg.AddProfile(new CoreAutoMapperProfileConf());
                cfg.AddProfile(new PaymentAutoMapperProfileConf());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        //public static IServiceCollection AddAllReportDependency(this IServiceCollection services, IConfiguration configuration) {
        //   // return services.AddAllReportDependency(configuration);
        //}

        public static IServiceCollection ConfigureCoreServiceDependencies(this IServiceCollection serivces, IConfiguration configuration) {
            CoreServiceDIOptions coreServiceDIOptions = new CoreServiceDIOptions();
            coreServiceDIOptions.SessionOptions = new UserSessionOptions();
            coreServiceDIOptions.SessionOptions.RemoteSession = true;
            CoreServiceEnum includedCoreServices = CoreServiceEnum.ConnectionManager | CoreServiceEnum.EmailService
            | CoreServiceEnum.ExceptionService | CoreServiceEnum.NotificationService | CoreServiceEnum.ScheduledJobService
            | CoreServiceEnum.SerilogLoggingService | CoreServiceEnum.SMSService 
            | CoreServiceEnum.UserSessionService | CoreServiceEnum.UniqueIdentityGenerator | CoreServiceEnum.AppDeeplinkService;
            return serivces.ConfigureCoreServices(configuration, includedCoreServices, coreServiceDIOptions);
        }

        public static IServiceCollection ConfigurePaymentAppSettings(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<PaymentAppSettings>(configuration);
            return services;
        }

        public static IConfiguration InitAppSetting(IConfiguration configuration, IHostingEnvironment env) {
            string envName = env.EnvironmentName;
            IConfigurationBuilder builder = new ConfigurationBuilder()
             .AddConfiguration(configuration)
             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings"))
             .AddJsonFile($"appsettings.dev.json", false, true)
             .AddEnvironmentVariables();
            return builder.Build();
        }

        public static IApplicationBuilder AddPaymentMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("AllowSpecificOrigin");

            // User Session
            app.AddUserSessionMiddleware(new Core.UserSessionService.UserSessionOptions() { LightSession = false });

            //Serilog Middleware
            app.UseMiddleware<SerilogHttpMiddleware>(Options.Create(new Core.SerilogLoggingService.SerilogHttpMiddlewareOptions() {
                EnableExceptionLogging = false
            }));

            return app;
        }

        #endregion

        #region Authentication Dependencies

        public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration) {
            PaymentAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<PaymentAppSettings>>().Value;

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
            PaymentAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<PaymentAppSettings>>().Value;

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
            // Register the Swagger generator, defining 1 mnor more Swagger documents
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "Payment API",
                    Description = "Payment Portal Web API",
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
                // c.OperationFilter<SwaggerUIRequestHeaderFilter>();
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app) {

            // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), 
            // Enable middleware to serve generated Swagger as a JSON endpoint.            
            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Api");
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
