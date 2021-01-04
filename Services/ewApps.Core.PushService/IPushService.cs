using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.PushService
{
  /// <summary>
  /// Interface for Push and Alerts
  /// This interface is common for all ewApps Push, from various modules.
  /// </summary>
  public interface IPushService
  {
    Task<bool> SendPushAsync(PushPayload pushPayload, CancellationToken token = default(CancellationToken));

  }
}
