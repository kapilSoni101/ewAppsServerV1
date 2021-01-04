using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace ewApps.Core.RedisLUAServer.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LUAScriptController : ControllerBase
  {
    private ILUAScriptExecutor _executor;

/// <summary>
/// Controller
/// </summary>
/// <param name="executor"></param>
    public LUAScriptController(ILUAScriptExecutor executor) {
      _executor = executor;
}
    
    // POST api/values
    [HttpPost]
    public object Post([FromBody] string script)
    {
      return _executor.ExecuteScript(script);
    }

   
  }
}
