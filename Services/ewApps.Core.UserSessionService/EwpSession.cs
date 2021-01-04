using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.UserSessionService
{

  /// <summary>
  /// This class contains session property of TenantId, Tenant Name, User Id and User Name.
  /// </summary>
  public class EwpSession
  {

    #region Constructor

    /// <summary>
    /// Initialize new instance of EwAppsSession class.
    /// </summary>
    public EwpSession()
    {
    }

    /// <summary>
    /// Initialize new instance of EwAppsSession class.
    /// </summary>
    /// <param name="userLoginId">Logged-in user id.</param>
    /// <param name="userName">Logged-in user name.</param>
    /// <param name="tenantId">Logged-in user's tenant id.</param>
    /// <param name="tenantName">Business Name of logged-in tenant.</param>
    /// <param name="systemAdmin">System Admin flag.</param>
    /// <param name="accountAdmin">Account admin flag.</param>
    /// <param name="loginAppId">Application id where user going to login.</param>
    /// <param name="loginAppName">Application name where user going to login.</param>
    /// <param name="localRegion">The local client region.</param>
    /// <param name="localIANATimeZone">The local client iana time zone.</param>
    /// <param name="browser">Name of browser</param>
    /// <param name="os">Operating name</param>
    public EwpSession(Guid userLoginId, string userName, Guid tenantId, string tenantName, bool systemAdmin, bool accountAdmin, Guid loginAppId, string loginAppName, string localRegion, string localIANATimeZone, string requesterId, string requesterDeviceName, string browser, string os, string platform)
    {
      UserId = userLoginId;
      UserName = userName;
      TenantId = tenantId;
      TenantName = tenantName;
      LoginTime = DateTime.Now.ToUniversalTime();
      SystemAdmin = systemAdmin;
      AccountAdmin = accountAdmin;
      LoginAppId = loginAppId;
      LoginAppName = loginAppName;
      LocalRegion = localRegion;
      //LocalIANATimeZone = localIANATimeZone;
      RequesterId = requesterId;
      RequesterDeviceName = requesterDeviceName;
      Browser = browser;
      OS = os;
      Platform = platform;
    }

    #endregion Constructor

    #region Public Properties

    // Collection of session properties.
    private Dictionary<string, object> _loggerExtendedProperties = null;

    /// <summary>
    /// It provides dictionary of session properties.
    /// </summary>
//    [System.Web.Script.Serialization.ScriptIgnore]
    [System.Xml.Serialization.XmlIgnore]
    public Dictionary<string, object> LoggerExtendedProperties
    {
      get
      {
        if (_loggerExtendedProperties == null)
          GenerateLoggerExtendedProperties();
        return _loggerExtendedProperties;
      }
    }

    /// <summary>
    /// Login user's tenant id.
    /// </summary>
    public Guid TenantId
    {
      get;
      set;
    }

    /// <summary>
    /// Business name of logged-in tenant.
    /// </summary>
    public string TenantName
    {
      get;
      set;
    }

    /// <summary>
    /// The logged-in user id.
    /// </summary>
    public Guid UserId
    {
      get;
      set;
    }

    /// <summary>
    /// Logged-in user name.
    /// </summary>
    public string UserName
    {
      get;
      set;
    }

    /// <summary>
    /// User's login date and time.
    /// </summary>
    public DateTime LoginTime
    {
      get;
      set;
    }

    /// <summary>
    /// User's login date and time.
    /// </summary>
    public bool SystemAdmin
    {
      get;
      set;
    }

    /// <summary>
    /// Is login user is Account Admin
    /// </summary>
    public bool AccountAdmin
    {
      get;
      set;
    }

    /// <summary>
    /// Logged-in User RequesterId.
    /// </summary>
    public string RequesterId
    {
      get;
      set;
    }

    /// <summary>
    /// Logged-in user RequesterDeviceName.
    /// </summary>
    public string RequesterDeviceName
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the id of the login application.
    /// </summary>
    /// <value>
    /// The id of the login application.
    /// </value>
//    [System.Web.Script.Serialization.ScriptIgnore]
    [System.Xml.Serialization.XmlIgnore]
    public Guid LoginAppId
    {
      get;
      set;
    }


    /// <summary>
    /// Gets or sets the name of the login application.
    /// </summary>
    /// <value>
    /// The name of the login application.
    /// </value>
    //[System.Web.Script.Serialization.ScriptIgnore]
    [System.Xml.Serialization.XmlIgnore]
    public string LoginAppName
    {
      get;
      set;
    }


    /// <summary>
    /// Gets or sets the local region.
    /// </summary>
    /// <value>
    /// The local region.
    /// </value>
    public string LocalRegion
    {
      get;
      set;
    }

    ///// <summary>
    ///// Gets or sets the local iana time zone.
    ///// </summary>
    ///// <value>
    ///// The local iana time zone.
    ///// </value>
    //public string LocalIANATimeZone {
    //    get;
    //    set;
    //}


    /// <summary>
    /// Gets or sets the external user identifier.
    /// </summary>
    /// <value>
    /// The external user identifier.
    /// </value>
    public Guid ExternalUserId
    {
      get;
      set;
    }

    /// <summary>
    /// Represents browser name
    /// </summary>
    public string Browser
    {
      get; set;
    }

    /// <summary>
    /// Represents operating system name
    /// </summary>
    public string OS
    {
      get; set;
    }

    /// <summary>
    /// Client Platform 
    /// </summary>
    public string Platform { get; set; }

    public Guid UserSessionToken { get; set; }

    #endregion

    #region Private Methods

    // This method provide extended property for a session object.
    private void GenerateLoggerExtendedProperties()
    {
      if (_loggerExtendedProperties != null)
        _loggerExtendedProperties.Clear();
      else
        _loggerExtendedProperties = new Dictionary<string, object>();

      _loggerExtendedProperties.Add("TenantId", TenantId);
      _loggerExtendedProperties.Add("TenantName", TenantName);
      _loggerExtendedProperties.Add("UserId", UserId);
      _loggerExtendedProperties.Add("UserName", UserName);
      _loggerExtendedProperties.Add("LoginAppName", LoginAppName);
      _loggerExtendedProperties.Add("RequesterId", RequesterId);
      _loggerExtendedProperties.Add("RequesterDeviceName", RequesterDeviceName);
      _loggerExtendedProperties.Add("Browser", Browser);
      _loggerExtendedProperties.Add("OS", OS);
      _loggerExtendedProperties.Add("Platform", Platform);
    }

    #endregion

  }
}

