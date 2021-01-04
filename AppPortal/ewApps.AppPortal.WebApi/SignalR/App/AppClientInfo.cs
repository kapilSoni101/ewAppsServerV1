using ewApps.Core.SignalRService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.AppPortal.WebApi {
  /// <summary>
  /// Information about the caller
  /// </summary>
  public class AppClientInfo:ClientInfo {
    /// <summary>
    /// UserId of the logged in user
    /// </summary>
    public string UserId {
      get; set;
    }
    /// <summary>
    /// TenantId of the logged in user
    /// </summary>
    public string TenantId {
      get; set;
    }
  }
}
