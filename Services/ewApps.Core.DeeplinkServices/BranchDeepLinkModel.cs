using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ewApps.Core.DeeplinkServices
{
  /// <summary>
  /// This class model a branch's request to generate short deeplink uri.
  /// </summary>
  [DataContract]
  public class BranchDeepLinkModel
  {

    /// <summary>
    /// The Branch key string to communicate with branch API.
    /// </summary>
    [DataMember]
    public string branch_key
    {
      get;
      set;
    }

    /// <summary>
    /// The data required to generate deep link through Branch API.
    /// </summary>
    [DataMember]
    public DeepLinkModel data
    {
      get;
      set;
    }

  }
}
