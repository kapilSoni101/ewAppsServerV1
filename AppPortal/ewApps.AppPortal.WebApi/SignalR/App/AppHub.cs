using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ewApps.AppPortal.WebApi
{
  /// <summary>
  /// This class is responsible for managing the SignalR connection 
  /// 
  /// </summary>
  public class AppHub : Hub
  {
 
    private ILogger<AppHub> _logger;


    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="logger">DI for Logger</param>
    public AppHub(ILogger<AppHub> logger)
    {
      _logger = logger;
    }

    /// <summary>
    /// Event handle, executed when client is connected with the hub,It adds the client to the group of tenant
    /// </summary>
    /// <returns>void</returns>
    public override Task OnConnectedAsync()
    {
      string connectionID = Context.ConnectionId;
      string groupId = Context.GetHttpContext().Request.Query["tenantid"];
      //FOR EMPTY TENANTDO NOT ADD TO GROUP
      if (!string.IsNullOrEmpty(groupId))
        Groups.AddToGroupAsync(Context.ConnectionId, groupId);
      // string transport = Context.Items["transport"].ToString();
      _logger.LogDebug("Client connected with connectionId : {0} and groupId:{1} ", Context.ConnectionId, groupId);
      return base.OnConnectedAsync();

    }
    /// <summary>
    /// EventHandler, Executed when client is disconnected.It removes the client from the group of tenant
    /// </summary>
    /// <param name="exception">Exception that causes disconnection</param>
    /// <returns>void</returns>
    public override Task OnDisconnectedAsync(Exception exception)
    {

      string connectionID = Context.ConnectionId;
      string groupId = Context.GetHttpContext().Request.Query["tenantid"];
      if (!string.IsNullOrEmpty(groupId))
        Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
      _logger.LogDebug("Client disconnected, connectionId : {0} and groupId:{1} ", Context.ConnectionId, groupId);
      return base.OnDisconnectedAsync(exception);
    }


  }
}
