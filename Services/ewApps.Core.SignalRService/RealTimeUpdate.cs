using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.SignalRService
{
  /// <summary>
  /// Signal R implementation for event delegates
  /// </summary>
  public class RealTimeUpdate<T> : IRealTimeUpdate  where T:Hub
  {
    private IHubContext<T> _hubContext;
    private ILogger<RealTimeUpdate<T>> _logger;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hubContext">DI for SignalR hub context</param>
    /// <param name="logger">DI for logger</param>
    public RealTimeUpdate(IHubContext<T> hubContext, ILogger<RealTimeUpdate<T>> logger)
    {
      _hubContext = hubContext;
      _logger = logger;
    }

    /// <inheritdoc/>
    public async virtual Task SendMessageAsync(string messageName, MessagePayload messagePayload, string receiverGroupName, bool sendToAll = false, ClientInfo clientInfo=null, CancellationToken token = default(CancellationToken)){ 
      //fillup client info by session
      //otherproperties can not be set as caller/user/client is 
      //not available outside ofthe hub, as this method is called from server we do not need caller
      _logger.LogDebug("Message send at @messageName with @msgPayload", messageName, messagePayload);
      if (sendToAll)
        await _hubContext.Clients.All.SendAsync(messageName, messagePayload, clientInfo, token);
      else
        await _hubContext.Clients.Group(receiverGroupName).SendAsync(messageName,messagePayload, clientInfo, token);
    }
  }
}
