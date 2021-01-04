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
using ewApps.AppPortal.Data;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.QData;
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

namespace ewApps.BusinessEntity.DI
{

  public static class BusinessEntityCollection
  {

    #region Business Entity Dependency Methods


    public static IServiceCollection AddBusinessEntityDependency(this IServiceCollection services, IConfiguration Configuration)
    {
      AddBusinessEntityDataServiceDependency(services);
      AddBusinessEntityDataDependency(services);
      AddBusinessEntityOtherDependency(services, Configuration);
      return services;
    }

    public static IServiceCollection AddBusinessEntityDataServiceDependency(this IServiceCollection services)
    {
      services.AddScoped<IAppSyncServiceDS, AppSyncServiceDS>();
      services.AddScoped<IBADeliveryDS, BADeliveryDS>();
      services.AddScoped<IBADeliveryItemDS, BADeliveryItemDS>();
      services.AddScoped<IBADeliveryAttachmentDS, BADeliveryAttachmentDS>();
      services.AddScoped<IBAPurchaseInquiryItemDS, BAPurchaseInquiryItemDS>();

      services.AddScoped<IBAItemMasterDS, BAItemMasterDS>();
      services.AddScoped<IBACustomerDS, BACustomerDS>();
      services.AddScoped<IBACustomerAddressDS, BACustomerAddressDS>();
      services.AddScoped<IBACustomerContactDS, BACustomerContactDS>();
      services.AddScoped<IBACustomerPaymentDetailDS, BACustomerPaymentDetailDS>();


      services.AddScoped<IBAVendorDS, BAVendorDS>();
      services.AddScoped<IBAVendorAddressDS, BAVendorAddressDS>();
      services.AddScoped<IBAVendorContactDS, BAVendorContactDS>();

      services.AddScoped<IBASalesOrderDS, BASalesOrderDS>();
      services.AddScoped<IBASalesOrderItemDS, BASalesOrderItemDS>();
      services.AddScoped<IBASalesOrderAttachmentDS, BASalesOrderAttachmentDS>();
      services.AddScoped<IBASalesQuotationDS, BASalesQuotationDS>();
      services.AddScoped<IBASalesQuotationItemDS, BASalesQuotationItemDS>();
      services.AddScoped<IBASalesQuotationAttachmentDS, BASalesQuotationAttachmentDS>();

      services.AddScoped<IBAARInvoiceDS, BAARInvoiceDS>();
      services.AddScoped<IBAARInvoiceItemDS, BAARInvoiceItemDS>();
      services.AddScoped<IBAARInvoiceAttachmentDS, BAARInvoiceAttachmentDS>();

      services.AddScoped<IBAAPInvoiceDS, BAAPInvoiceDS>();
      services.AddScoped<IBAAPInvoiceItemDS, BAAPInvoiceItemDS>();
      services.AddScoped<IBAAPInvoiceAttachmentDS, BAAPInvoiceAttachmentDS>();

      services.AddScoped<IBAPurchaseInquiryDS, BAPurchaseInquiryDS>();
      services.AddScoped<IBAPurchaseOrderDS, BAPurchaseOrderDS>();
      services.AddScoped<IBAPurchaseOrderItemDS, BAPurchaseOrderItemDS>();
      services.AddScoped<IBAPurchaseOrderAttachmentDS, BAPurchaseOrderAttachmentDS>();
      services.AddScoped<IRoleDS, RoleDS>();
      services.AddScoped<IRoleLinkingDS, RoleLinkingDS>();
      services.AddScoped<IERPConnectorDS, ERPConnectorDS>();
      services.AddScoped<IERPConfigDS, ERPConfigDS>();
      services.AddScoped<ICustomerSignupDS, CustomerSignupDS>();
      services.AddScoped<IAppMgmtDS, AppMgmtDS>();

      services.AddScoped<IBAContractDS, BAContractDS>();
      services.AddScoped<IBAVendorContractDS, BAVendorContractDS>();
      services.AddScoped<IBAContractItemDS, BAContractItemDS>();
      services.AddScoped<IBAContractAttachmentDS, BAContractAttachmentDS>();

      services.AddScoped<IBAASNDS, BAASNDS>();
      services.AddScoped<IBAASNItemDS, BAASNItemDS>();
      services.AddScoped<IBAASNAttachmentDS, BAASNAttachmentDS>();

      services.AddScoped<IBusBAItemMasterDS, BusBAItemMasterDS>();
      services.AddScoped<IBAItemAttachmentDS, BAItemMasterAttachmentDS>();


      services.AddScoped<IBusBADeliveryDS, BusBADeliveryDS>();
      services.AddScoped<IBusBAContractDS, BusBAContractDS>();
      services.AddScoped<IBusBASalesQuotationDS, BusBASalesQuotationDS>();
      services.AddScoped<IBusBASalesQuotationItemDS, BusBASalesQuotationItemDS>();
      services.AddScoped<IBusBASalesOrderDS, BusBASalesOrderDS>();
      services.AddScoped<IBusBAASNDS, BusBAASNDS>();

      services.AddScoped<ICustBASalesQuotationDS, CustBASalesQuotationDS>();
      services.AddScoped<ICustBAItemMasterDS, CustBAItemMasterDS>();
      services.AddScoped<ICustBASalesOrderDS, CustBASalesOrderDS>();
      services.AddScoped<IBAContractItemDS, BAContractItemDS>();
      services.AddScoped<ICustBADeliveryDS, CustBADeliveryDS>();
      services.AddScoped<ICustBAContractDS, CustBAContractDS>();
      services.AddScoped<ISyncTimeLogDS, SyncTimeLogDS>();
      services.AddScoped<ICustBAASNDS, CustBAASNDS>();
      services.AddScoped<IAppPortalDS, AppPortalDS>();
      services.AddScoped<IQNotificationDS, QNotificationDS>();

      services.AddScoped<IASNotificationDS, ASNotificationDS>();

      services.AddScoped<IBusinessEntityNotificationHandler, BusinessEntityNotificationHandler>();
      services.AddScoped<IBusinessEntityNotificationService, BusinessEntityNotificationService>();
      services.AddScoped<IBusinessEntityNotificationRecipientDS, BusinessEntityNotificationRecipientDS>();

      services.AddScoped<ITenantUserAppPreferenceDS, TenantUserAppPreferenceDS>();

      services.AddScoped<IVendorBAContractDS, VendorBAContractDS>();
      services.AddScoped<IVendBAPurchaseOrderDS, VendorBAPurchaseOrderDS>();

      return services;
    }

    public static IServiceCollection AddBusinessEntityDataDependency(this IServiceCollection services)
    {
      services.AddDbContext<BusinessEntityDbContext>();
      services.AddDbContext<QBusinessEntityDbContext>();
      services.AddTransient<IUnitOfWork, UnitOfWork>(x =>
          new UnitOfWork(x.GetRequiredService<BusinessEntityDbContext>(),
                                 false
                                ));
      services.AddScoped<IBADeliveryRepository, BADeliveryRepository>();
      services.AddScoped<IBADeliveryItemRepository, BADeliveryItemRepository>();
      services.AddScoped<IBADeliveryAttachmentRepository, BADeliveryAttachmentRepository>();
      services.AddScoped<IBAPurchaseInquiryItemRepository, BAPurchaseInquiryItemRepository>();

      services.AddScoped<IBAItemMasterRepository, BAItemMasterRepository>();

      services.AddScoped<IBAItemAttachmentRepository, BAItemAttachmentRepository>();


      services.AddScoped<IBACustomerRepository, BACustomerRepository>();
      services.AddScoped<IBACustomerAddressRepository, BACustomerAddressRepository>();
      services.AddScoped<IBACustomerContactRepository, BACustomerContactRepository>();
      services.AddScoped<IBACustomerPaymentDetailRepository, BACustomerPaymentDetailRepository>();

      services.AddScoped<IBAVendorRepository, BAVendorRepository>();
      services.AddScoped<IBAVendorAddressRepository, BAVendorAddressRepository>();
      services.AddScoped<IBAVendorContactRepository, BAVendorContactRepository>();

      services.AddScoped<IBASalesOrderRepository, BASalesOrderRepository>();
      services.AddScoped<IBASalesOrderItemRepository, BASalesOrderItemRepository>();
      services.AddScoped<IBASalesOrderAttachmentRepository, BASalesOrderAttachmentRepository>();
      services.AddScoped<IBASalesQuotationRepository, BASalesQuotationRepository>();
      services.AddScoped<IBASalesQuotationItemRepository, BASalesQuotationItemRepository>();
      services.AddScoped<IBASalesQuotationAttachmentRepository, BASalesQuotationAttachmentRepository>();

      services.AddScoped<IBAARInvoiceRepository, BAARInvoiceRepository>();
      services.AddScoped<IBAARInvoiceItemRepository, BAARInvoiceItemRepository>();
      services.AddScoped<IBAARInvoiceAttachmentRepository, BAARInvoiceAttachmentRepository>();
      services.AddScoped<IBACustomerRepository, BACustomerRepository>();

      services.AddScoped<IBAAPInvoiceRepository, BAAPInvoiceRepository>();
      services.AddScoped<IBAAPInvoiceItemRepository, BAAPInvoiceItemRepository>();
      services.AddScoped<IBAAPInvoiceAttachmentRepository, BAAPInvoiceAttachmentRepository>();

      services.AddScoped<IBAPurchaseInquiryRepository, BAPurchaseInquiryRepository>();
      services.AddScoped<IBAPurchaseOrderRepository, BAPurchaseOrderRepository>();
      services.AddScoped<IBAPurchaseOrderItemRepository, BAPurchaseOrderItemRepository>();
      services.AddScoped<IBAPurchaseOrderAttachmentRepository, BAPurchaseOrderAttachmentRepository>();

      services.AddScoped<IRoleRepository, RoleRepository>();
      services.AddScoped<IRoleLinkingRepository, RoleLinkingRepository>();
      services.AddScoped<IERPConnectorConfigRepository, ERPConnectorConfigRepository>();
      services.AddScoped<IERPConnectorRepository, ERPConnectorRepository>();

      services.AddScoped<IBAContractRepository, BAContractRepository>();
      services.AddScoped<IBAVendorContractRepository, BAVendorContractRepository>();
      services.AddScoped<IBAContractItemRepository, BAContractItemRepository>();
      services.AddScoped<IBAContractAttachmentRepository, BAContractAttachmentRepository>();

      services.AddScoped<IBAASNRepositorty, BAASNRepositorty>();
      services.AddScoped<IBAASNItemRepositorty, BAASNItemRepositorty>();
      services.AddScoped<IBAASNAttachmentRepository, BAASNAttachmentRepository>();

      services.AddScoped<ISyncTimeLogRepository, SyncTimeLogRepository>();

      services.AddScoped<IASNotificationRepository, ASNotificationRepository>();
      services.AddScoped<IBusinessEntityNotificationRecipientRepository, BusinessEntityNotificationRecipientRepository>();
      services.AddScoped<IQNotificationData, QNotificationData>();

      services.AddScoped<IQBAItemMasterRepository, QBAItemMasterRepository>();
      services.AddScoped<IQBAInvoiceRepository, QBAInvoiceRepository>();

      services.AddScoped<ITenantUserAppPreferenceRepository, TenantUserAppPreferenceRepository>();

      return services;
    }

    public static IServiceCollection AddBusinessEntityOtherDependency(this IServiceCollection services, IConfiguration Configuration)
    {


      return services;
    }

    public static IServiceCollection AddAllReportDependency(this IServiceCollection services, IConfiguration configuration)
    {
      return services.AddReportDependency(configuration);
    }

    public static IServiceCollection ConfigureCoreServiceDependencies(this IServiceCollection serivces, IConfiguration configuration)
    {
      CoreServiceDIOptions coreServiceDIOptions = new CoreServiceDIOptions();
      coreServiceDIOptions.SessionOptions = new UserSessionOptions();
      coreServiceDIOptions.SessionOptions.RemoteSession = true;
      CoreServiceEnum includedCoreServices = CoreServiceEnum.ConnectionManager | CoreServiceEnum.EmailService
      | CoreServiceEnum.ExceptionService | CoreServiceEnum.NotificationService | CoreServiceEnum.ScheduledJobService
      | CoreServiceEnum.SerilogLoggingService | CoreServiceEnum.SMSService | CoreServiceEnum.DMService
      | CoreServiceEnum.UserSessionService | CoreServiceEnum.UniqueIdentityGenerator;

      return serivces.ConfigureCoreServices(configuration, includedCoreServices, coreServiceDIOptions);
    }

    public static IServiceCollection ConfigureBusinessEntityAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<BusinessEntityAppSettings>(configuration);
      return services;
    }

    public static IConfiguration InitAppSetting(IConfiguration configuration, IHostingEnvironment env)
    {
      string envName = env.EnvironmentName;
      IConfigurationBuilder builder = new ConfigurationBuilder()
       .AddConfiguration(configuration)
       .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings"))
       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
       .AddEnvironmentVariables();
      return builder.Build();
    }

    public static IApplicationBuilder AddBusinessEntityMiddleware(this IApplicationBuilder app)
    {


      app.AddExceptionMiddleware();

      // Shows UseCors with CorsPolicyBuilder.
      app.UseCors("AllowSpecificOrigin");

      // Custome User session Middelware
      app.AddUserSessionMiddleware(new UserSessionOptions() { LightSession = false });

      //Serilog Middleware
      app.UseMiddleware<SerilogHttpMiddleware>(Options.Create(new SerilogHttpMiddlewareOptions()
      {
        EnableExceptionLogging = false
      }));

      return app;
    }
    #endregion

    public static IServiceCollection AddFactoryServiceDependency(this IServiceCollection services, IConfiguration Configuration)
    {
      IServiceScopeFactory scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

      var factory = new SyncServiceFactory();
      factory.Register("SAP", new SAPSyncServiceDS(scopeFactory));
      factory.Register("BME", new BMSyncServiceDS());
      services.AddSingleton<ISyncServiceFactory>(factory);

      return services;
    }

    #region Authentication Dependencies

    public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
      // Get App Settings for Identity Server Url
      BusinessEntityAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<BusinessEntityAppSettings>>().Value;

      // Identity Server Url
      services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                          .AddIdentityServerAuthentication(options =>
                          {
                            options.Authority = appSettings.IdentityServerUrl;
                            options.RequireHttpsMetadata = false;
                            options.ApiName = appSettings.AppName;
                          });

      return services;
    }

    public static IApplicationBuilder ConfigurationAuthentication(this IApplicationBuilder app)
    {
      // System Identity Server Middleware
      app.UseAuthentication();
      return app;
    }

    #endregion

    #region CORS Dependencies

    public static IServiceCollection ConfigurationCORSService(this IServiceCollection services)
    {
      // Get App Settings for Identity Server Url
      BusinessEntityAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<BusinessEntityAppSettings>>().Value;

      // System Cors Dependency for Cross Origin      
      services.AddCors(o => o.AddPolicy("AllowSpecificOrigin",
          builder =>
          {
            builder.SetIsOriginAllowedToAllowWildcardSubdomains().WithOrigins(appSettings.CrossOriginsUrls).AllowAnyMethod().WithExposedHeaders("content-disposition").AllowAnyHeader().AllowCredentials().SetPreflightMaxAge(TimeSpan.FromSeconds(86800));
          }));


      return services;
    }

    public static IApplicationBuilder ConfigureCORS(this IApplicationBuilder app)
    {
      // Shows UseCors with CorsPolicyBuilder.
      app.UseCors("AllowSpecificOrigin");
      return app;
    }

    #endregion

    #region Swagger Dependencies

    public static IServiceCollection ConfigureSwaggerService(this IServiceCollection services)
    {
      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info
        {
          Version = "v1",
          Title = "Business Entity API",
          Description = "Business Entity Web API",
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
        //  c.OperationFilter<SwaggerUIRequestHeaderFilter>();

      });

      return services;
    }

    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
    {
      // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), 
      // Enable middleware to serve generated Swagger as a JSON endpoint.            
      // specifying the Swagger JSON endpoint.
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Entity Service");
        c.RoutePrefix = string.Empty;
      });

      return app;
    }
    #endregion

    #region Response Compression Dependencies

    public static IApplicationBuilder ConfigureResponseCompressionMiddleware(this IApplicationBuilder app)
    {
      app.UseResponseCompression();
      return app;
    }

    public static IServiceCollection ConfigureResponseCompressionService(this IServiceCollection services)
    {
      services.AddResponseCompression(options =>
      {
        options.EnableForHttps = true;
      });

      return services;
    }

    #endregion

  }
}
