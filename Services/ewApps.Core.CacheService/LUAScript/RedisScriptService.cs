using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.CacheService
{
//LUA Script Service
  public class RedisScriptService : IScriptService
  { 
    internal readonly IDatabase _database;
    private ConnectionMultiplexer _connection;

    public RedisScriptService(IConnectionFactory connectionFactory)
    {
      _connection = connectionFactory.Connection();
      _database = connectionFactory.Database();

    }

    public RedisResult ExecuteScript(string command)
    {
      var prepared = LuaScript.Prepare(command);
      RedisResult s = _database.ScriptEvaluate(prepared);
      return s;
    }

    //public T Get(string key)
    //{
    //  key = this.GenerateKey(key);
    //  var hash = this.Db.HashGetAll(key);
    //  return this.MapFromHash(hash);
    //}

    //public void Save(string key, T obj)
    //{
    //  if (obj != null)
    //  {
    //    var hash = this.GenerateHash(obj);
    //    key = this.GenerateKey(key);

    //    if (this.Db.HashLength(key) == 0)
    //    {
    //      this.Db.HashSet(key, hash);
    //    }
    //    else
    //    {
    //      var props = this.Properties;
    //      foreach (var item in props)
    //      {
    //        if (this.Db.HashExists(key, item.Name))
    //        {
    //          this.Db.HashIncrement(key, item.Name, Convert.ToInt32(item.GetValue(obj)));
    //        }
    //      }
    //    }

    //  }
    //}

   

   
  }
}

