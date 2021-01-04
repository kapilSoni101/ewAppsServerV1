using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ewApps.Core.DeeplinkServices
{
  /// <summary>
  /// This class model a request data required to generate deep link uri.
  /// </summary>
  [DataContract]
  public class DeepLinkModel
  {

    // Action list related to current user action.
    private List<string> _actionList;
    // Parameter list related to current user action.
    private List<KeyValuePair<string, string>> _parameterList;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeepLinkModel"/> class and member variables.
    /// </summary>
    public DeepLinkModel()
    {
      _actionList = new List<string>();
      _parameterList = new List<KeyValuePair<string, string>>();
    }

    /// <summary>
    /// Action list.
    /// </summary>
    [DataMember]
    public List<string> action
    {
      get
      {
        return _actionList;
      }
    }

    /// <summary>
    /// Action related parameter list.
    /// </summary>
    [DataMember]
    public List<KeyValuePair<string, string>> parameters
    {
      get
      {
        return _parameterList;
      }
    }

    ///// <summary>
    ///// Gets or sets the desktop_url.
    ///// </summary>
    ///// <value>
    ///// The desktop_url.
    ///// </value>
    //[DataMember]
    //public string desktop_url {
    //  get;
    //  set;
    //}

  }
}
