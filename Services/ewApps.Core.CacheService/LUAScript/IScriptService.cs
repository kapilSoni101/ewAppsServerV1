using StackExchange.Redis;

namespace ewApps.Core.CacheService
{
  public interface IScriptService
  {
    RedisResult ExecuteScript(string command);
  }
}