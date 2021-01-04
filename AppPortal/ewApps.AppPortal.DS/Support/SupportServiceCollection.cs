//using ewApps.Core.LoggingService;
//using Microsoft.Extensions.DependencyInjection;

//namespace ewApps.AppPortal.DS {
//  public static class SupportServiceCollection {

//    public static IServiceCollection AddSupportDependency(this IServiceCollection services) {
//      AddSupportDataServiceDependency(services);
//      AddSupportDataDependency(services);
//      AddSupportOtherDependency(services);
//      return services;
//    }

//    public static IServiceCollection AddSupportDataServiceDependency(this IServiceCollection services) {
//      //services.AddTransient<ISupportDataService, SupportDataService>();
//      services.AddTransient<ISupportNotificationHandler, SupportNotificationHandler>();
//      services.AddTransient<ISupportNotificationReceipentDataService, SupportNotificationReceipentDataService>();     
//      // Support Ticket
//      services.AddTransient<IPaymentSupportTicketDS, PaymentSupportTicketDS>();


//      return services;
//    }

//    public static IServiceCollection AddSupportDataDependency(this IServiceCollection services) {
      
//      return services;
//    }

//    public static IServiceCollection AddSupportOtherDependency(this IServiceCollection services) {
//      services.AddTransient<ISupportNotificationService, SupportNotificationService>();
//      services.AddSingleton<Logger>();
//      return services;
//    }

//  }
//}
