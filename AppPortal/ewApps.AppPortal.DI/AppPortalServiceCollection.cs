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
using ewApps.AppPortal.Common;
using ewApps.AppPortal.QData;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DS;
using ewApps.Core.DI;
using ewApps.Core.ExceptionService;
using ewApps.Core.SerilogLoggingService;
using ewApps.Core.Services.DI;
using ewApps.Core.UserSessionService;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ewApps.AppPortal.DI {

    public static class AppPortalServiceCollection {

        #region AppPortal Dependency Methods

        public static IServiceCollection AddAppPortalDataServiceDependency(this IServiceCollection services) {

            services.AddScoped<IPlatformDS, PlatformDS>();
            services.AddScoped<ITenantUserAppPreferenceDS, TenantUserAppPreferenceDS>();
            services.AddScoped<ITokenInfoDS, TokenInfoDS>();
            services.AddScoped<IAuthenticationDS, AuthenticationDS>();
            services.AddScoped<IBusinessDS, BusinessDS>();
            services.AddScoped<IBusinessSignUpDS, BusinessSignUpDS>();
            services.AddScoped<IPlatformAppDS, PlatformAppDS>();
            services.AddScoped<IPlatformSubscriptionPlanDS, PlatformSubscriptionPlanDS>();
            services.AddScoped<IRoleDS, RoleDS>();
            services.AddScoped<IRoleLinkingDS, RoleLinkingDS>();
            services.AddScoped<IBusinessAddressDS, BusinessAddressDS>();
            services.AddScoped<ICustomerSignUpDS, CustomerSignUpDS>();
            services.AddScoped<ICustomerDS, CustomerDS>();
            services.AddScoped<IVendorSignUpDS, VendorSignUpDS>();
            services.AddScoped<IVendorDS, VendorDS>();
            services.AddScoped<IBusinessExtDS, BusinessExtDS>();
            services.AddScoped<IBusinessUpdateDS, BusinessUpdateDS>();

            services.AddScoped<IAboutUsDS, AboutUsDS>();
            services.AddScoped<IBusCustomerDS, BusCustomerDS>();
            services.AddScoped<IBusVendorDS, BusVendorDS>();
            services.AddScoped<IPaymentAndInvoiceDS, PaymentAndInvoiceDS>();
            services.AddScoped<ITenantAppLinkingDS, TenantAppLinkingDS>();
            services.AddScoped<IAppPortalDS, AppPortalDS>();
            services.AddScoped<ICustomerAccountDetailDS, CustomerAccountDetailDS>();
            services.AddScoped<IPlatVersionDS, PlatVersionDS>();
            services.AddScoped<INotesDS, NotesDS>();
            services.AddScoped<IFavoriteDS, FavoriteDS>();
            services.AddScoped<IASNotificationDS, ASNotificationDS>();
            services.AddScoped<IViewSettingsDS, ViewSettingsDS>();
            services.AddScoped<IContactUsDS, ContactUsDS>();


            #region Publisher

            services.AddScoped<IPublisherUpdateDS, PublisherUpdateDS>();
            services.AddScoped<IPublisherExtDS, PublisherExtDS>();
            services.AddScoped<IPublisherSignUpDS, PublisherSignUpDS>();
            services.AddScoped<IQPubBusinessSubsPlanAppServiceDS, QPubBusinessSubsPlanAppServiceDS>();
            services.AddScoped<IPubBusinessSubsPlanAppServiceDS, PubBusinessSubsPlanAppServiceDS>();
            services.AddScoped<IPubBusinessSubsPlanDS, PubBusinessSubsPlanDS>();
            services.AddScoped<IPublisherAppDS, PublisherAppDS>();
            services.AddScoped<IPublisherAppSettingDS, PublisherAppSettingDS>();
            services.AddScoped<IPublisherDS, PublisherDS>();
            services.AddScoped<IPublisherAddressDS, PublisherAddressDS>();
            services.AddScoped<IPublisherUpdateDS, PublisherUpdateDS>();
            services.AddScoped<IPubSubscriptionPlanDS, PubSubscriptionPlanDS>();

            #endregion

            #region Support

            services.AddScoped<ISupportCommentDS, SupportCommentDS>();
            services.AddScoped<ISupportGroupDS, SupportGroupDS>();
            services.AddScoped<ISupportNotificationService, SupportNotificationService>();
            services.AddScoped<ISupportTicketAssigneeHelper, SupportTicketAssigneeHelper>();
            services.AddScoped<ILevelTransitionHistoryDS, LevelTransitionHistoryDS>();
            services.AddScoped<ISupportTicketDSNew, SupportTicketDSNew>();
            services.AddScoped<IPaymentSupportTicketDSNew, PaymentSupportTicketDSNew>();
            services.AddScoped<IPublisherSupportTicketDSNew, PublisherSupportTicketDSNew>();
            services.AddScoped<IPlatformSupportTicketDSNew, PlatformSupportTicketDSNew>();
            // services.AddScoped<IShipmentSupportTicketDS, ShipmentSupportTicketDS>();
            // services.AddScoped<IBusinessSetupSupportTicketDS, BusinessSetupSupportTicketDS>();
            services.AddScoped<ICustomerSupportTicketDSNew, CustomerSupportTicketDSNew>();

            #endregion Support

            #region TenantUser

            #region Platform

            services.AddScoped<IPlatTenantUserUpdateDS, PlatTenantUserUpdateDS>();
            services.AddScoped<IPlatTenantUserDeleteDS, PlatTenantUserDeleteDS>();
            services.AddScoped<IPlatTenantUserDS, PlatTenantUserDS>();
            services.AddScoped<IPlatTenantUserSignUpDS, PlatTenantUserSignUpDS>();

            #endregion Platform

            #region Publisher

            services.AddScoped<IPubTenantUserUpdateDS, PubTenantUserUpdateDS>();
            services.AddScoped<IPubTenantUserDeleteDS, PubTenantUserDeleteDS>();
            services.AddScoped<IPubTenantUserSignUpDS, PubTenantUserSignUpDS>();
            services.AddScoped<IPubTenantUserDS, PubTenantUserDS>();

            #endregion Publisher

            #region Business

            services.AddScoped<IBusTenantUserSignUpDS, BusTenantUserSignUpDS>();
            services.AddScoped<IBusTenantUserUpdateDS, BusTenantUserUpdateDS>();
            services.AddScoped<IBusTenantUserDS, BusTenantUserDS>();
            services.AddScoped<IBusTenantUserDeleteDS, BusTenantUserDeleteDS>();

            #endregion Business

            #region Customer            

            services.AddScoped<ICustTenantUserDS, CustTenantUserDS>();
            services.AddScoped<ICustTenantUserSignUpDS, CustTenantUserSignUpDS>();
            services.AddScoped<ICustTenantUserUpdateDS, CustTenantUserUpdateDS>();
            services.AddScoped<ICustTenantUserDeleteDS, CustTenantUserDeleteDS>();

            #endregion

            #region Vendor
            services.AddScoped<IVendTenantUserDS, VendTenantUserDS>();
            services.AddScoped<IVendorUserPreferenceDS, VendorUserPreferenceDS>();

            #endregion

            services.AddScoped<IPlatTenantUserStatusDS, PlatTenantUserStatusDS>();
            services.AddScoped<IPubTenantUserStatusDS, PubTenantUserStatusDS>();
            services.AddScoped<IBusTenantUserStatusDS, BusTenantUserStatusDS>();
            services.AddScoped<ICustTenantUserStatusDS, CustTenantUserStatusDS>();
            services.AddScoped<IVendTenantUserStatusDS, VendTenantUserStatusDS>();

            services.AddScoped<IVendorNotificationHandler, VendorNotificationHandler>();
            services.AddScoped<IVendorNotificationRecipientDS, VendorNotificationRecipientDS>();
            services.AddScoped<IVendorNotificationService, VendorNotificationService>();

            #endregion TenantUser

            #region Access

            services.AddScoped<IBusinessAccess, BusinessAccess>();
            services.AddScoped<IPlatformAppAccess, PlatformAppAccess>();
            services.AddScoped<IPlatformAppUserAccess, PlatformAppUserAccess>();
            services.AddScoped<IPlatformSubscriptionPlanAccess, PlatformSubscriptionPlanAccess>();
            services.AddScoped<IPublisherAccess, PublisherAccess>();

            #endregion Access

            #region Notification 

            #region Platform

            services.AddScoped<IPlatformNotificationHandler, PlatformNotificationHandler>();
            services.AddScoped<IPlatformNotificationRecipientDS, PlatformNotificationRecipientDS>();
            services.AddScoped<IPlatformNotificationService, PlatformNotificationService>();
            services.AddScoped<IPlatformSubscriptionPlanDS, PlatformSubscriptionPlanDS>();
            services.AddScoped<IBizNotificationHandler, BizNotificationHandler>();
            services.AddScoped<IBizNotificationService, BizNotificationService>();
            services.AddScoped<IBizNotificationRecipientDS, BizNotificationRecipientDS>();


            #endregion Platorm

            #region Publisher

            services.AddScoped<IPublisherNotificationHandler, PublisherNotificationHandler>();
            services.AddScoped<IPublisherNotificationRecipientDS, PublisherNotificationRecipientDS>();
            services.AddScoped<IPublisherNotificationService, PublisherNotificationService>();

            #endregion Publisher

            #region Business

            services.AddScoped<IBusinessNotificationHandler, BusinessNotificationHandler>();
            services.AddScoped<IBusinessNotificationReceipentDataService, BusinessNotificationReceipentDataService>();
            services.AddScoped<IBusinessNotificationService, BusinessNotificationService>();

            #endregion Business

            #region Customer

            services.AddScoped<ICustNotificationHandler, CustNotificationHandler>();
            services.AddScoped<ICustNotificationRecipientsDS, CustNotificationRecipientsDS>();
            services.AddScoped<ICustNotificationService, CustNotificationService>();

            #endregion Customer

            services.AddScoped<INotificationDS, NotificationDS>();

            #endregion Notification 

            #region BA

            services.AddScoped<IBusBAItemMasterDS, BusBAItemMasterDS>();
            services.AddScoped<IBusBADeliveryDS, BusBADeliveryDS>();
            services.AddScoped<IBusBAContractDS, BusBAContractDS>();
            services.AddScoped<IBusBASalesOrderDS, BusBASalesOrderDS>();
            services.AddScoped<IBusBASalesQuotationDS, BusBASalesQuotationDS>();
            services.AddScoped<IBusBAASNDS, BusBAASNDS>();
            services.AddScoped<IBAPurchaseOrderDS, BAPurchaseOrderDS>();
            services.AddScoped<IPurchaseOrderDS, PurchaseOrderDS>();
            services.AddScoped<IBusUserPreferenceDS, BusUserPreferenceDS>();

            services.AddScoped<ICustBASalesQuotationDS, CustBASalesQuotationDS>();
            services.AddScoped<ICustBAItemMasterDS, CustBAItemMasterDS>();
            services.AddScoped<ICustBASalesOrderDS, CustBASalesOrderDS>();
            services.AddScoped<ICustBADeliveryDS, CustBADeliveryDS>();
            services.AddScoped<ICustBAContractDS, CustBAContractDS>();
            services.AddScoped<ICustBAASNDS, CustBAASNDS>();
            services.AddScoped<ICustUserPreferenceDS, CustUserPreferenceDS>();

            #endregion BA

            #region Vendor

            services.AddScoped<IVendorPaymentInvoiceDS, VendorPaymentInvoiceDS>();

            #endregion Vendor

            return services;
        }

        public static IServiceCollection AddAppPortalQDataServiceDependency(this IServiceCollection services) {
            services.AddScoped<IQPlatformUserSessionDS, QPlatformUserSessionDS>();
            services.AddScoped<IQPublisherUserSessionDS, QPublisherUserSessionDS>();
            services.AddScoped<IQBusinessUserSessionDS, QBusinessUserSessionDS>();
            services.AddScoped<IQCustUserSessionDS, QCustUserSessionDS>();
            services.AddScoped<IQVendUserSessionDS, QVendUserSessionDS>();
            services.AddScoped<IQPlatformAndUserDS, QPlatformAndUserDS>();
            services.AddScoped<IQPublisherAndUserDS, QPublisherAndUserDS>();
            services.AddScoped<IQBusinessAndUserDS, QBusinessAndUserDS>();
            services.AddScoped<IQPlatBusinessDS, QPlatBusinessDS>();
            services.AddScoped<IQPubBusinessSubsPlanAppServiceRepository, QPubBusinessSubsPlanAppServiceRepository>();
            services.AddScoped<IQBusinessAppDS, QBusinessAppDS>();
            services.AddScoped<IQBACustomerDS, QBACustomerDS>();
            services.AddScoped<IQAppPortalDS, QAppPortalDS>();
            services.AddScoped<IQConfigurationDS, QConfigurationDS>();
            services.AddScoped<IQNotesDS, QNotesDS>();
            services.AddScoped<IQContactUsDS, QContactUsDS>();

            #region Customer

            services.AddScoped<IQCustomerAndUserDS, QCustomerAndUserDS>();
            services.AddScoped<IQCustomerAppDS, QCustomerAppDS>();

            #endregion Customer

            #region Vendor

            services.AddScoped<IQVendorAndUserDS, QVendorAndUserDS>();
            services.AddScoped<IQVendorAppDS, QVendorAppDS>();

            #endregion Vendor

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppPortalDataDependency(this IServiceCollection services) {
            services.AddDbContext<AppPortalDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>(x =>
                      new UnitOfWork(x.GetRequiredService<AppPortalDbContext>(),
                                             false
                                            ));
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();

            services.AddScoped<IPublisherAddressRepository, PublisherAddressRepository>();
            services.AddScoped<ITenantUserAppPreferenceRepository, TenantUserAppPreferenceRepository>();
            services.AddScoped<ITokenInfoRepository, TokenInfoRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IPubBusinessSubsPlanAppServiceRepository, PubBusinessSubsPlanAppServiceRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleLinkingRepository, RoleLinkingRepository>();
            services.AddScoped<IPublisherAppSettingRepository, PublisherAppSettingRepository>();
            services.AddScoped<IBusinessAddressRepository, BusinessAddressRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IPubBusinessSubsPlanRepository, PubBusinessSubsPlanRepository>();
            services.AddScoped<ITenantAppLinkingRepository, TenantAppLinkingRepository>();
            services.AddScoped<ICustomerAccountDetailRepository, CustomerAccountDetailRepository>();
            services.AddScoped<INotesRespository, NotesRespository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IASNotificationRepository, ASNotificationRepository>();
            services.AddScoped<IViewSettingsRepository, ViewSettingsRepository>();
            services.AddScoped<IViewSettingsRepository, ViewSettingsRepository>();
            

            #region Support

            services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
            services.AddScoped<ISupportCommentRepository, SupportCommentRepository>();
            services.AddScoped<ISupportGroupRepository, SupportGroupRepository>();
            services.AddScoped<ILevelTransitionHistoryRepository, LevelTransitionHistoryRepository>();


            #endregion Support



            return services;
        }

        public static IServiceCollection AddAppPortalQDataDependency(this IServiceCollection services) {
            services.AddDbContext<QAppPortalDbContext>();
            services.AddScoped<IQBusinessAndUserRepository, QBusinessAndUserRepository>();
            services.AddScoped<IQPlatformUserSessionRepository, QPlatformUserSessionRepository>();
            services.AddScoped<IQPublisherUserSessionRepository, QPublisherUserSessionRepository>();
            services.AddScoped<IQBusinessUserSessionRepository, QBusinessUserSessionRepository>();
            services.AddScoped<IQCustUserSessionRepository, QCustUserSessionRepository>();
            services.AddScoped<IQVendUserSessionRepository, QVendUserSessionRepository>();
            services.AddScoped<IQBusinessAndUserRepository, QBusinessAndUserRepository>();
            services.AddScoped<IQPlatformAndUserRepository, QPlatformAndUserRepository>();
            services.AddScoped<IQPlatBusinessRepository, QPlatBusinessRepository>();
            services.AddScoped<IQBusinessAppRepository, QBusinessAppRepository>();
            services.AddScoped<IQPublisherAndUserRepository, QPublisherAndUserRepository>();
            services.AddScoped<IQBACustomerRepository, QBACustomerRepository>();
            services.AddScoped<IQAppPortalRepository, QAppPortalRepository>();
            services.AddScoped<IQConfigurationRepository, QConfigurationRepository>();
            services.AddScoped<IQCustomerAppRepository, QCustomerAppRepository>();
            services.AddScoped<IQNotesRepository, QNotesRepository>();
            services.AddScoped<IQContactUsRepository, QContactUsRepository>();

            services.AddScoped<IQVendorNotificationRecipientsData, QVendorNotificationRecipientsData>();
            

            #region customer

            services.AddScoped<IQCustomerAndUserRepository, QCustomerAndUserRepository>();

            #endregion customer

            #region Vendor

            services.AddScoped<IQVendorAndUserRepository, QVendorAndUserRepository>();
            services.AddScoped<IQVendorAppRepository, QVendorAppRepository>();

            #endregion Vendor


            #region Notification

            services.AddScoped<IQPlatformNotificationRecipientRepository, QPlatformNotificationRecipientRepository>();
            services.AddScoped<IQPublisherNotificationRecipientRepository, QPublisherNotificationRecipientRepository>();
            services.AddScoped<IQBusinessNotificationRecepientRepository, QBusinessNotificationRecepientRepository>();
            services.AddScoped<IQBizNotificationRecipientData, QBizNotificationRecipientData>();
            services.AddScoped<IQCustNotificationRecipientsData, QCustNotificationRecipientsData>();

            #endregion Notification

            return services;
        }

        public static IServiceCollection AddAppPortalOtherDependency(this IServiceCollection services, IConfiguration Configuration) {
            // Automapper
            var config = new AutoMapper.MapperConfiguration(cfg => {
                cfg.AddProfile(new AppPortalAutoMapperProfileConf());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection ConfigureAppPortalAppSettings(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<AppPortalAppSettings>(configuration);
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

        public static IApplicationBuilder AddAppPortalMiddleware(this IApplicationBuilder app) {

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
            AppPortalAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppPortalAppSettings>>().Value;

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
            AppPortalAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppPortalAppSettings>>().Value;

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
                    Title = "App Portal API",
                    Description = "App Portal Web API",
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publisher Portal");
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

        #region Core Service

        public static IServiceCollection ConfigureCoreServiceDependencies(this IServiceCollection serivces, IConfiguration configuration) {
            CoreServiceDIOptions coreServiceDIOptions = new CoreServiceDIOptions();
            coreServiceDIOptions.SessionOptions = new UserSessionOptions();
            coreServiceDIOptions.SessionOptions.RemoteSession = false;
            CoreServiceEnum includedCoreServices = CoreServiceEnum.ConnectionManager | CoreServiceEnum.EmailService
            | CoreServiceEnum.ExceptionService | CoreServiceEnum.NotificationService | CoreServiceEnum.ScheduledJobService
            | CoreServiceEnum.SerilogLoggingService | CoreServiceEnum.SMSService | CoreServiceEnum.DMService
            | CoreServiceEnum.UserSessionService | CoreServiceEnum.UniqueIdentityGenerator | CoreServiceEnum.Money | CoreServiceEnum.ServiceProcessor;
            return serivces.ConfigureCoreServices(configuration, includedCoreServices, coreServiceDIOptions);
        }

        #endregion Core Service
    }
}
