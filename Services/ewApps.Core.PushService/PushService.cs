using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.PushService
{
  /// <summary>
  /// Implementes the push service interface, it has responsibility to send APN,GCM and Desktop push.
  /// </summary>
  public class PushService : IPushService
  {

    private IPUSHNotificationQueueDataService _pnqDS;
    public PushService()
    {
      _pnqDS = new PUSHNotificationQueueDataService();
    }

    /// <summary>
    /// This method Generates all XML data,Send it to XSLT and gets the Push Body
    /// Pushbody is further for
    /// APN push
    /// GCM Push
    /// DEsktop Alerts.
    /// </summary>
    public async Task<bool> SendPushAsync(PushPayload pushPayload, CancellationToken token = default(CancellationToken))
    {
      try
      {
        // Gets recipient user id and other data from data table.
        #region variables
        Guid recipientUserId = pushPayload.RecepientUserId;
        Guid tenantId = pushPayload.TenantId;
        Guid trackingId = new Guid(pushPayload.NotificationInfo["TrackingId"]);
        Guid linkedNotificationId = new Guid(pushPayload.NotificationInfo["LinkNotificationId"]);
        DateTime deliveryTime = pushPayload.DeliveryTime;
        Guid appId = EDApplicationInfo.AppId;
        string language = string.Empty;
        language = pushPayload.UserLanguage;
        Dictionary<string, string> xsltArguments = pushPayload.XSLTArguments;
        string pushTitle = string.Empty, pushBody = string.Empty;
        #endregion

        //Gets Push body and title string 
        Tuple<string, string> pushDetail = GetPushTitleAndBody(pushPayload, xsltArguments, trackingId, language);
        pushTitle = pushDetail.Item1;
        pushBody = pushDetail.Item2;
        //Get user tags
        string userTagString = PUSHNotificationDataService.GetUserTagString(recipientUserId);

        //If Push setting is on by client -iOS push
        if (pushPayload.APNPush)
          SendAPNPushNotification(pushTitle, pushBody, recipientUserId, tenantId, appId, linkedNotificationId, pushPayload.DeeplinkJson, pushPayload.BadgeCount, pushPayload.AppType, pushPayload.SilentPush, pushPayload.DeliveryTime, pushPayload.SyncRowId, userTagString);
        //If Push setting is on by client -Android Push
        if (pushPayload.GCMPush)
          SendGCMPushNotification(pushTitle, pushBody, recipientUserId, tenantId, appId, linkedNotificationId, pushPayload.DeeplinkJson, pushPayload.BadgeCount, pushPayload.AppType, pushPayload.SilentPush, pushPayload.DeliveryTime, pushPayload.SyncRowId, userTagString);
        //If Push setting is on by client - Desktop Push/Alert
        if (pushPayload.DesktopPush && !pushPayload.SilentPush)
          SendDesktopAlertNotification(pushTitle, pushBody, recipientUserId, tenantId, linkedNotificationId, pushPayload.DeeplinkJson, pushPayload.BadgeCount, pushPayload.AppType, pushPayload.SilentPush, deliveryTime);
        return true;
      }
      catch (Exception ex)
      {
        //ExceptionHandler.HandleAsyncException(ex);
        return false;
      }
    }

    #region APN
    private async void SendAPNPushNotification(string messagePart1, string messagePart2, Guid userId, Guid tenantId, Guid appId, Guid linkNotificationId, string customData, int badgeCount, int appType, bool silentPush, DateTime deliveryTime, long syncRowId, string userTagString)
    {
      try
      {
        PUSHNotificationQueueOpsModel apnPushNQOpsModel = SetAPNNotificationModel(messagePart1, messagePart2, userId, tenantId, appId, linkNotificationId, customData, badgeCount, appType, silentPush, deliveryTime, syncRowId, userTagString);
        List<PUSHNotificationQueueOpsModel> list = new List<PUSHNotificationQueueOpsModel>();
        list.Add(apnPushNQOpsModel);
        _pnqDS.AddPUSHNotificationQueueList(list);
      }
      catch (Exception ex)
      {
        //ExceptionHandler.HandleAsyncException(ex);
      }
    }
    private PUSHNotificationQueueOpsModel SetAPNNotificationModel(string messagePart1, string messagePart2, Guid userId, Guid tenantId, Guid appId, Guid linkNotificationId, string customData, int badgeCount, int appType, bool silentPush, DateTime deliveryTime, long syncRowId, string userTagString)
    {
      // Creates notification request model to send push notification.
      PUSHNotificationQueueOpsModel apnPushNQOpsModel = new PUSHNotificationQueueOpsModel();
      apnPushNQOpsModel.CustomData = customData;
      apnPushNQOpsModel.ApplicationId = appId;
      apnPushNQOpsModel.DeliverySubType = (int)LocalNotificationType.Banner;
      apnPushNQOpsModel.DeliveryTime = deliveryTime;//DateTime.UtcNow;
      apnPushNQOpsModel.DeliveryType = (int)NotificationDeliveryType.PUSH;
      apnPushNQOpsModel.DeviceType = (int)PUSHNotificationPlatformType.APN;
      apnPushNQOpsModel.MessagePart1 = messagePart1;
      apnPushNQOpsModel.MessagePart2 = messagePart2;
      apnPushNQOpsModel.OperationType = (int)OperationType.Add;
      apnPushNQOpsModel.UserId = userId;
      apnPushNQOpsModel.Tag = userTagString;
      apnPushNQOpsModel.TenantId = tenantId;
      apnPushNQOpsModel.NotificationId = linkNotificationId;
      apnPushNQOpsModel.BadgeCount = badgeCount;

      if (silentPush)
      {
        apnPushNQOpsModel.PushType = PushType.SilentWithBadge;
      }
      else
      {
        apnPushNQOpsModel.PushType = PushType.Remote;
      }

      apnPushNQOpsModel.OtherInfo.Add("AppType", Convert.ToString(appType));
      //TODO:ewApps.CommonRuntime.Common.Utils.DateTimeHelper.DateTimeHelper.GetStringDateFromDateTime(DateTime.UtcNow)
      apnPushNQOpsModel.OtherInfo.Add("UTime", deliveryTime.ToString());
      apnPushNQOpsModel.OtherInfo.Add("SyncRowId", Convert.ToString(syncRowId));
      return apnPushNQOpsModel;
    }
    #endregion APN

    #region GCM
    protected async void SendGCMPushNotification(string messagePart1, string messagePart2, Guid userId, Guid tenantId, Guid appId, Guid linkNotificationId, string customData, int badgeCount, int appType, bool silentPush, DateTime deliveryTime, long syncRowId, string userTagString)
    {
      try
      {
        PUSHNotificationQueueOpsModel gcmPushNQOpsModel = SetGCMNotificationModel(messagePart1, messagePart2, userId, tenantId, appId, linkNotificationId, customData, badgeCount, appType, silentPush, deliveryTime, syncRowId, userTagString);
        List<PUSHNotificationQueueOpsModel> list = new List<PUSHNotificationQueueOpsModel>();
        list.Add(gcmPushNQOpsModel);
        _pnqDS.AddPUSHNotificationQueueList(list);
      }
      catch (Exception ex)
      {
        //ExceptionHandler.HandleAsyncException(ex);
      }
    }
    private PUSHNotificationQueueOpsModel SetGCMNotificationModel(string messagePart1, string messagePart2, Guid userId, Guid tenantId, Guid appId, Guid linkNotificationId, string customData, int badgeCount, int appType, bool silentPush, DateTime deliveryTime, long syncRowId, string userTagString)
    {
      PUSHNotificationQueueOpsModel gcmPushNQOpsModel = null;
      if (silentPush == false)
      {
        gcmPushNQOpsModel = new PUSHNotificationQueueOpsModel();
        gcmPushNQOpsModel.CustomData = customData;
        gcmPushNQOpsModel.ApplicationId = appId;
        gcmPushNQOpsModel.DeliverySubType = (int)LocalNotificationType.Banner;
        gcmPushNQOpsModel.DeliveryTime = deliveryTime;
        gcmPushNQOpsModel.DeliveryType = (int)NotificationDeliveryType.PUSH;
        gcmPushNQOpsModel.DeviceType = (int)PUSHNotificationPlatformType.GCM;
        gcmPushNQOpsModel.MessagePart1 = messagePart1;
        gcmPushNQOpsModel.MessagePart2 = messagePart2;
        gcmPushNQOpsModel.OperationType = (int)OperationType.Add;
        gcmPushNQOpsModel.UserId = userId;
        gcmPushNQOpsModel.Tag = userTagString;
        gcmPushNQOpsModel.TenantId = tenantId;
        gcmPushNQOpsModel.NotificationId = linkNotificationId;
        if (silentPush)
        {
          gcmPushNQOpsModel.PushType = PushType.SilentWithBadge;
        }
        else
        {
          gcmPushNQOpsModel.PushType = PushType.Remote;
        }
        gcmPushNQOpsModel.OtherInfo.Add("AppType", Convert.ToString(appType));
        ////TODO:ewApps.CommonRuntime.Common.Utils.DateTimeHelper.DateTimeHelper.GetStringDateFromDateTime(DateTime.UtcNow)
        gcmPushNQOpsModel.OtherInfo.Add("UTime", deliveryTime.ToString());
        gcmPushNQOpsModel.OtherInfo.Add("SyncRowId", Convert.ToString(syncRowId));


        gcmPushNQOpsModel.BadgeCount = badgeCount;

      }
      return gcmPushNQOpsModel;
    }
    #endregion

    #region Desktop
    protected async void SendDesktopAlertNotification(string messagePart1, string messagePart2, Guid userId, Guid tenantId, Guid lastNotificationId, string customData, int badgeCount, int appType, bool silentPush, DateTime deliveryTime)
    {
      try
      {
        DesktopPushModel desktopPushModel = new DesktopPushModel();
        desktopPushModel.Title = messagePart1;
        desktopPushModel.Body = messagePart2;
        desktopPushModel.ImageUrl = "";
        desktopPushModel.JsonData = customData;
        desktopPushModel.UserIds = new List<string>() { userId.ToString() };
        // XMPPService.NotifyDesktopPush(desktopPushModel);

      }
      catch (Exception ex)
      {
        ExceptionHandler.HandleAsyncException(ex);
      }
    }
    #endregion Desktop
    /// <summary>
    /// Gets the Template from Cache or
    /// Generate the template by XML and XSLT arguments and sets in cache fornext use.
    /// </summary>
    /// <param name="pushPayload"></param>
    /// <param name="xsltArguments"></param>
    /// <param name="trackingId"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    private Tuple<string, string> GetPushTitleAndBody(PushPayload pushPayload, Dictionary<string, string> xsltArguments, Guid trackingId, string language)
    {

      //Get email title and body template key.
      //Each event notification has unique id for the given event for all users.
      string templateTitleKey = string.Format("{0}-{1}-{2}", trackingId, "PushTitle", language);
      string templateBodyKey = string.Format("{0}-{1}-{2}", trackingId, "PushBody", language);
      string pushBody;
      string pushTitle;
      //Generate the XSLT file if not in cache or cache is notsuppose to be used.
      if (!pushPayload.UseCacheTemplate || !CacheHelper.IsInCache(templateTitleKey))
      {
        string pushXsltFileText = XSLTHelper.GetPushXsltFile(language);
        pushBody = XSLHelper.XSLTransformByXslAndXmlString(pushXsltFileText, pushPayload.EventXMLData, "title", "body", out pushTitle, xsltArguments);
        pushBody = ewApps.CommonRuntime.Common.Utils.HtmlDecode(pushBody);
        //Add to cache if cahe can be used.
        if (pushPayload.UseCacheTemplate)
        {
          CacheHelper.SetData(templateTitleKey, pushTitle, string.Empty);
          CacheHelper.SetData(templateBodyKey, pushBody, string.Empty);
        }
      }
      else
      {
        pushTitle = CacheHelper.GetData<string>(templateTitleKey);
        pushBody = CacheHelper.GetData<string>(templateBodyKey);
      }
      return new Tuple<string, string>(pushTitle, pushBody);
    }

    #region Support

    #endregion
  }
}
