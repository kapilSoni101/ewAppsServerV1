using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace ewApps.Core.CacheService
{
  /// <summary>
  /// Service extension method, that adds all required dependencies to the DI
  /// </summary>
  public static class CacheServiceCollection
  {
    public static IServiceCollection AddCacheService(this IServiceCollection services, CacheConfigurationOptions cacheOptions)
    {
      if (cacheOptions == null)
      { //Read from local Configuration
        return services;
      }
      string host = cacheOptions.Host;
      int port = cacheOptions.Port;
      if (port > 0)
        host += ":" + port;
      string instanceName = cacheOptions.InstanceName;
      string cacheType = cacheOptions.CacheType;

      if (cacheType == "Redis")
      {
        services.AddDistributedRedisCache(options =>
        {
          options.Configuration = host;
          options.InstanceName = instanceName;
        });
        services.AddSingleton<IEwAppsDistributedCache, EwAppsDistributedCache>();
      }

      return services;
    }
  }
}

