using System;
using Microsoft.Extensions.Caching.Redis;
using StackExchange.Redis;
using Microsoft.Extensions.Options;


namespace ewApps.Core.CacheService
{


  public class ConnectionFactory : IConnectionFactory
  {
    /// <summary>
    ///     The _connection.
    /// </summary>
    private readonly Lazy<ConnectionMultiplexer> _connection;
    private bool _persistantCache;

    public ConnectionFactory(CacheConfigurationOptions options)
    {
      if (options.CacheType == "Redis")
        this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options.Host + ":" + options.Port));
  
    }

    public ConnectionMultiplexer Connection()
    {
      return this._connection.Value;
    }
    public IDatabase Database()
    {
      return this._connection.Value.GetDatabase();
    }

  
  }
}

