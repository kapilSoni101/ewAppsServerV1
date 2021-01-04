using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.AppDeeplinkService {

  /// <summary>
  /// 1) Contains all necessary information to generate deeplink.
  /// 2) Contains information for navigating the url and show the data on page.
  /// </summary>
  public class AppDeeplinkPayloadDTO {

    /// <summary>
    /// Id
    /// </summary>
    public Guid ID
    {
      get; set;
    }

    /// <summary>
    /// UserId
    /// </summary>
    public Guid? UserId
    {
      get; set;
    }

    /// <summary>
    /// TenantId
    /// </summary>
    public Guid TenantId
    {
      get; set;
    }

    /// <summary>
    /// Password to access url.
    /// Asha - It may be blank, as user will accessthe link from outside 
    /// So we can not get password from him.
    /// </summary>
    public string Password
    {
      get; set;
    }

    /// <summary>
    /// Number of time user can access the url.
    /// </summary>
    public int MaxUseCount
    {
      get; set;
    }

    /// <summary>
    /// Number of time user accessed the url.
    /// </summary>
    public int UserAccessCount
    {
      get; set;
    }

    /// <summary>
    /// Url expiration date, its optional.
    /// </summary>
    public DateTime? ExpirationDate
    {
      get; set;
    }

    /// <summary>
    /// Json data, contain all necessary information required for showing the data on clicked deep link.
    /// </summary>
    public string ActionData
    {
      get; set;
    }

    /// <summary>
    /// call back url where the this action will be handled
    /// </summary>
    public string ActionEndpointUrl
    {
      get; set;
    }

    /// <summary>
    /// The module can assign any name, preferably for example, <namespace>.<action-name>.
    /// Using ActionName, Can identify the module name, from where deeplink generated. 
    /// </summary>
    public string ActionName
    {
      get; set;
    }

  }
}
