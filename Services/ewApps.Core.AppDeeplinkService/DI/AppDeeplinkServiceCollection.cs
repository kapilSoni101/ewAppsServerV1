using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.AppDeeplinkService {

    /// <summary>
    /// Register classes for the injecting.
    /// </summary>
    public static class AppDeeplinkServiceCollection {

    public static IServiceCollection AppDeeplinkDependency(this IServiceCollection services, IConfiguration configuration) {
      services.AddDbContext<AppDeeplinkDBContext>();
      services.AddScoped<IAppDeeplinkManager, AppDeeplinkManager>();
      var appDeeplinkSection = configuration.GetSection("AppDeeplinkAppSettings");
      services.Configure<AppDeeplinkAppSettings>(appDeeplinkSection);
      return services;
    }

  }
}
