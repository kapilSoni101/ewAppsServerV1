using ewApps.Core.CommonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ewApps.Core.DeeplinkServices
{
  /// <summary>
  /// This class provides common methods properties for link prepration.
  /// </summary>
  [DataContract]
  public class Link
  {

    private DeepLinkModel _deepLinkModel = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="Link"/> class and its member variables.
    /// </summary>
    public Link()
    {
      _deepLinkModel = new DeepLinkModel();
      //GenerateDefaultParams();
    }

    /// <summary>
    /// Action list.
    /// </summary>
    [IgnoreDataMember]
    public List<string> GetAction
    {
      get
      {
        return _deepLinkModel.action;
      }
    }

    /// <summary>
    /// The parameter list.
    /// </summary>
    [IgnoreDataMember]
    public List<KeyValuePair<string, string>> GetParamaters
    {
      get
      {
        return _deepLinkModel.parameters;
      }
    }

    /// <summary>
    /// Base desktop url as configured.
    /// </summary>
    [IgnoreDataMember]
    public string GetBaseDesktopURL
    {
      get
      {
        // Read from confirguration
        return AppSettingHelper.GetDeeplinkBaseDesktopUrl();
      }
    }

    ///// <summary>
    ///// The complete desktop url.
    ///// </summary>
    //[IgnoreDataMember]
    //public string GetDesktopURL {
    //  get {
    //    return _deepLinkModel.desktop_url;
    //  }
    //}

    /// <summary>
    /// Deeplink model instance contains all related data.
    /// </summary>
    [DataMember]
    public DeepLinkModel ActionAndPara
    {
      get
      {
        return _deepLinkModel;
      }
    }

    // Mark it protected becuase we are not allowing to add action directly to collection. But I think we should allow.
    /// <summary>
    /// Adds the action.
    /// </summary>
    /// <param name="action">The action string.</param>
    protected void AddAction(string action)
    {
      if (string.IsNullOrEmpty(action) == false && _deepLinkModel.action.Contains(action) == false)
      {
        _deepLinkModel.action.Add(action);
      }
    }

    // Mark it protected becuase we are not allowing to add parameters directly to collection. But I think we should allow.
    /// <summary>
    /// Adds the parameter.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    protected void AddParameter(KeyValuePair<string, string> parameter)
    {
      KeyValuePair<string, string> existingRecord = _deepLinkModel.parameters.FirstOrDefault(i => i.Key.Equals(parameter.Key));
      if (existingRecord.Equals(default(KeyValuePair<string, string>)))
      {
        _deepLinkModel.parameters.Remove(existingRecord);
      }
      _deepLinkModel.parameters.Add(new KeyValuePair<string, string>(parameter.Key, parameter.Value));
    }

    /// <summary>
    /// Sets the desktop URL.
    /// </summary>
    /// <param name="relativeUrl">The relative URL.</param>
    protected void SetDesktopURL(string relativeUrl)
    {
      //_deepLinkModel.desktop_url = GetBaseDesktopURL + "/" + relativeUrl;
    }

    private void GenerateDefaultParams()
    {
      this._deepLinkModel.parameters.Add(new KeyValuePair<string, string>("SyncRowId", "{{SyncRowId}}"));
    }

  }
}
