using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.DeeplinkServices
{
  /// <summary>
  /// Interface for Deeplink
  /// This interface is common for all ewApps deeplinks, from various modules.
  /// It gathers all data required data for the deeplink.
  /// </summary>
  public interface IDeeplinkService
  {
    /// <summary>
    /// generates deeplink in the branch and setup the return data from branch like URL and jSON back to deeplink object. 
    /// </summary>
    /// <param name="deeplinkPayload"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DeeplinkResultSet> GenerateDeeplinkAsync(DeeplinkPayload deeplinkPayload, CancellationToken token = default(CancellationToken));

    DeeplinkResultSet GetDeeplinkList(DeeplinkPayload payload);

  }
}
