//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Extensions.DependencyInjection;

//namespace ewApps.Core.AppDeeplinkService {
//  /// <summary>
//  /// Registerd the all classes for injecting.
//  /// </summary>
//  public static class AppDeeplinkServiceCollection {

//    public static IServiceCollection AddAppDeeplinkManagerDependency(this IServiceCollection services) {
//      services.AddDbContext<AppDeeplinkDBContext>(ServiceLifetime.Transient);
//      services.AddScoped<IAppDeeplinkManager, AppDeeplinkManager>();
//      return services;
//    }

//  }
//}
