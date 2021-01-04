using StackExchange.Redis;

namespace ewApps.Core.RedisLUAServer
{
  public interface ILUAScriptExecutor
  {
    object ExecuteScript(string script);
  }
}