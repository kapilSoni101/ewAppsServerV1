using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.DeeplinkServices {

  /// <summary>
  /// This class helps to find the branch key.
  /// </summary>
  public class BranchKeyDecisionPayload {

    /// <summary>
    /// App key.
    /// </summary>
    public Guid AppKey {
      get; set;
    }

    /// <summary>
    /// User type.
    /// </summary>
    public int UserType {
      get; set;
    }

  }
}
