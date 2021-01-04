using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CacheService {
  public interface IConnectionFactory
  {
    ConnectionMultiplexer Connection();
    IDatabase Database();
   
  }

}
