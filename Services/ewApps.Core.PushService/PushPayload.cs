using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.PushService
{
  /// <summary>
  /// It has all the data required to generate push.
  /// </summary>

  public class PushPayload
  {
    public Guid AppId
    {
      get; set;
    }
    public int AppType
    {
      get; set;
    }
    public Guid TenantId
    {
      get; set;
    }
    public Guid RecepientUserId
    {
      get; set;
    }
    public string UserLanguage
    {
      get; set;
    }
    public bool IsLoggedInUser
    {
      get; set;
    }

    public bool APNPush
    {
      get; set;
    }
    public bool GCMPush
    {
      get; set;
    }
    public bool DesktopPush
    {
      get; set;
    }
    public int BadgeCount
    {
      get; set;
    }
    public bool SilentPush
    {
      get; set;
    }
    public long SyncRowId
    {
      get; set;
    }
    public DateTime DeliveryTime
    {
      get; set;
    }
    public string EventXMLData
    {
      get; set;
    }
    public string DeeplinkJson
    {
      get; set;
    }
    public bool UseCacheTemplate
    {
      get; set;
    }
    public string CustomData
    {
      get; set;
    }
    public Dictionary<string, string> NotificationInfo
    {
      get; set;
    }
    public Dictionary<string, string> XSLTArguments
    {
      get; set;
    }
  }
}
