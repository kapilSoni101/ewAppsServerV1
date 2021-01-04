using System.Collections.Generic;

namespace ewApps.Core.DeeplinkServices {
  /// <summary>
  /// It contains the result from the deeplink generated in a Dictionary for each link
  /// </summary>
  public class DeeplinkResultSet {
    // For each Deeplink generated:It keeps inforation abt DeeplinkURL and DeeplinkJson
    //it is a dictionary of required data for each link.
    public Dictionary<string, DeeplinkResult> DeeplinkResults { get; set; } = new Dictionary<string, DeeplinkResult>();

    public List<DeepLinkResultSetList> DeepLinkResultSetLists {
      get;
      set;
    }
  }

  /// <summary>
  /// Each link generated from Branch API keepsthe information on DeeplinkURL and DeeplinkJSON
  /// For error case we marked the hasDeeplink error flag true.
  /// </summary>
  public class DeeplinkResult {

    // For each Deeplink generated:It keeps inforation abt DeeplinkURL and DeeplinkJson
    //it is a dictionary of required data for each link.
    public string DeeplinkURL {
      get; set;
    }

    public string DeeplinkJson {
      get; set;
    }

    public bool HasLinkError {
      get; set;
    }

  }


  /// <summary>
  /// This class contains generated deeplink information.
  /// </summary>
  public class DeepLinkResultSetList {
    public string DeeplingResultKey;
    public Dictionary<string, DeeplinkResult> DeeplinkResults { get; set; } = new Dictionary<string, DeeplinkResult>();
  }
}
