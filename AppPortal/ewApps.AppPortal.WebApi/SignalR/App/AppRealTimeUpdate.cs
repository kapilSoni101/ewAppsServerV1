using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.SignalRService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;


namespace ewApps.AppPortal.WebApi
{
  /// <summary>
  /// Signal R implementation for event delegates
  /// </summary>
  public class AppRealTimeUpdate<T> : RealTimeUpdate<T> where T : AppHub
  {
    private IHubContext<T> _hubContext;
    private ILogger<AppRealTimeUpdate<T>> _logger;
    private IUserSessionManager _sessionManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hubContext">DI for SignalR hub context</param>
    /// <param name="sessionManager">DI for Session Manager to get user inforamtion</param>
    /// <param name="logger">DI for Logger</param>
    public AppRealTimeUpdate(IHubContext<T> hubContext, IUserSessionManager sessionManager, ILogger<AppRealTimeUpdate<T>> logger) : base(hubContext, logger)
    {
      _hubContext = hubContext;
      _logger = logger;
      _sessionManager = sessionManager;

    }

    /// <inheritdoc/>
    public override async Task SendMessageAsync(string messageName, MessagePayload msgPayload, string receiverGroupName, bool sendToAll = false, ClientInfo clientInfo =null,  CancellationToken token = default(CancellationToken))
    {
      AppClientInfo cInfo = new AppClientInfo();
      if (clientInfo != null)
        cInfo = (AppClientInfo)clientInfo;
      string groupName = "";
      if (!string.IsNullOrEmpty(receiverGroupName))
        groupName = receiverGroupName;

      UserSession session = _sessionManager.GetSession();
      //Fill up client info
      if (session != null)
      {
        cInfo.TenantId = session.TenantId.ToString();
        cInfo.UserId = session.TenantUserId.ToString();
        groupName = session.TenantId.ToString(); //Group name is set by Tenant Id Forcefully 
      }
      _logger.LogDebug(" Message send at @messageName with @msgPayload", messageName, msgPayload);
      await base.SendMessageAsync(messageName, msgPayload, groupName, sendToAll,cInfo, token);
    }
  }
}
