using ewApps.Core.CacheService;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.RedisLUAServer
{
  public class LUAScriptExecutor : ILUAScriptExecutor
  {
    private IScriptService _service;

    public LUAScriptExecutor(IScriptService service) {
      _service = service;

}
    public object ExecuteScript(string script) {
      //"redis.call('set', 'LUATest', 'Success')"
     // "return redis.call('hgetall', '123')"
      RedisResult result = _service.ExecuteScript(script);
      if (!result.IsNull) {
        var lines = (RedisResult[])result;
        var response = (string[])lines[1];
        return response;
      }
        return result;
    }



   

  }
}
