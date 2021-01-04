using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Webhook.Server {
  /// <summary>
  /// Helper class to Centralize all the logic for delivery log
  /// </summary>
  public static class WebhookEventHelper {
    /// <summary>
    /// Creates new Object for Delivery log
    /// </summary>
    /// <returns> WebhookEventDeliveryLog object</returns>
    public static WebhookEventDeliveryLog GetDeliveryLogObject() {
      WebhookEventDeliveryLog deliveryObject = new WebhookEventDeliveryLog();
      deliveryObject.ID = new Guid();
      deliveryObject.EventQueueTime = DateTime.UtcNow;
      deliveryObject = SetDeilveryLogForInit(deliveryObject);
      return deliveryObject;
    }
    /// <summary>
    /// Sets the Delivery log for Sucess Status
    /// </summary>
    /// <param name="obj">Delivery Log object </param>
    /// <returns>Updated Delivery log Object</returns>
    public static WebhookEventDeliveryLog SetDeilveryLogForSucess(WebhookEventDeliveryLog obj) {
      if (obj == null) return null;
      obj.DeliveryStatus = WebhookServerConstants.StatusSuccess;
      obj.LastDeliveryTime = DateTime.UtcNow;
      return obj;
    }
    /// <summary>
    /// Sets the Delivery Log Object as New delivery Log
    /// </summary>
    /// <param name="obj"> Object that need to be Updated</param>
    /// <returns>Updated object</returns>
    public static WebhookEventDeliveryLog SetDeilveryLogForInit(WebhookEventDeliveryLog obj) {
      if (obj == null) return null;
      obj.DeliveryStatus = WebhookServerConstants.StatusInProgress;
      obj.DeliveryAttempts = 0;
      obj.LastDeliveryTime = DateTime.UtcNow;
      return obj;
    }
    /// <summary>
    /// Sets the object for Sucessful Delivery/Dispatch
    /// </summary>
    /// <param name="obj">Object need to be updated</param>
    /// <returns>Updated Object</returns>
    public static WebhookEventDeliveryLog MarkDeilveryLogForSuccess(WebhookEventDeliveryLog obj) {
      if (obj == null) return null;
      obj.DeliveryStatus = WebhookServerConstants.StatusSuccess;
      obj.LastDeliveryTime = DateTime.UtcNow;
      return obj;
    }

    /// <summary>
    /// Set the Object for Failure DElivery
    /// </summary>
    /// <param name="obj"> Object need to be Updated</param>
    /// <returns>Updated Object</returns>
    public static WebhookEventDeliveryLog MarkDeilveryLogForFailure(WebhookEventDeliveryLog obj) {
      if (obj == null) return null;
      obj.DeliveryStatus = WebhookServerConstants.StatusFailure;
      obj.DeliveryAttempts = obj.DeliveryAttempts + 1;
      obj.LastDeliveryTime = DateTime.UtcNow;
      return obj;
    }

    /// <summary>
    /// Predicate/Condition to get the Delivery log for dispatch by Scheduler
    /// 1. Entry should not be older then 24 hours
    /// 2. Should have max 5 attempts for delivery if has failure status
    /// 3. or item is still in Progress status
    /// </summary>
    public static Predicate<WebhookEventDeliveryLog> PendingForDispatchDefault =
    x => x.LastDeliveryTime > DateTime.UtcNow.AddHours(-WebhookServerConstants.MaxDeliveryallowedHours) && ((x.DeliveryStatus == WebhookServerConstants.StatusInProgress) || (x.DeliveryStatus == WebhookServerConstants.StatusFailure && x.DeliveryAttempts < WebhookServerConstants.MaxDeliveryAttempts));

    /// <summary>
    /// Predicate/Condition to get the Delivery log failed to deliver
    /// 1. Entry should be with Failure status
    /// 2. or last delivery time is less then 24 hours with IN progress status
    /// </summary>
    public static Predicate<WebhookEventDeliveryLog> PendingForDispatchForced =
    x => (x.DeliveryStatus == WebhookServerConstants.StatusFailure || x.DeliveryStatus == WebhookServerConstants.StatusInProgress) && (x.LastDeliveryTime <= DateTime.UtcNow.AddHours(-WebhookServerConstants.MaxDeliveryallowedHours)) || (x.DeliveryStatus == WebhookServerConstants.StatusFailure && x.DeliveryAttempts >= WebhookServerConstants.MaxDeliveryAttempts);

  }
}
