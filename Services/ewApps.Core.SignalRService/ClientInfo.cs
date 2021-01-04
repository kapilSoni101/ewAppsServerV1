using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.SignalRService
{
  /// <summary>
  /// Information about the caller
  /// </summary>
  public class ClientInfo {
    /// <summary>
    /// Connection Id used by the SignalR
    /// </summary>
    public string ConnectionId {
      get; set;
    }
   
  }
}
