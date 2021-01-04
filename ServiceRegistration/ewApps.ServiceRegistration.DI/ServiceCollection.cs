
using ewApps.ServiceRegistration.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ewApps.ServiceRegistration.DS;

namespace ewApps.ServiceRegistration.DI
{
  /// <summary>
  /// Extention method the Add Services to services collection for DI
  /// </summary>
  public static class ServiceCollection
  {

    public static IServiceCollection AddServiceDependency(this IServiceCollection services, IConfiguration Configuration)
    {
      AddDataDependency(services, Configuration);
      AddDataServiceDependency(services);
      return services;
    }

    /// <summary>
    /// Adds all service specific Data layer dependencies
    /// </summary>
    /// <param name="services">Service collection DI</param>
    /// <param name="Configuration"> Configuration DI</param>
    /// <returns></returns>
    private static IServiceCollection AddDataDependency(this IServiceCollection services, IConfiguration Configuration)
    {
      services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      string db = Configuration.GetValue<string>("RegistrationByDB");
      string cache = Configuration.GetValue<string>("UseCacheService");
      if (cache == "Y")
      {
        services.AddSingleton<IRegistrationRepository, RegistrationCacheRepository>();
      }
      else
      {
        if (string.IsNullOrEmpty(db) || db == "N")
        {
          services.AddSingleton<IRegistrationRepository>(s => new RegistrationRepository(false));
        }
        else
        {
          services.AddTransient<IRegistrationRepository, RegistrationRepository>();
        }
      }
      return services;
    }

    /// <summary>
    /// Adds all Service specific Data Services dependencies
    /// </summary>
    /// <param name="services">Services Collection DI</param>
    /// <returns></returns>

    private static IServiceCollection AddDataServiceDependency(this IServiceCollection services)
    {
      services.AddTransient<IRegistrationDS, RegistrationDS>();
      return services;
    }



  }
}
