/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */

using System;

namespace ewApps.Core.Webhook.Server {

  /// <summary>
  /// DTO to send events to the subscribers when demanded
  /// Similar to WebhookeventDTO with DeliveryLogId property
  /// </summary>
  public class PendingWebhookEventDTO {

    /// <summary>
    /// Server Name
    /// </summary>
    public string ServerName {
      get; set;
    }

    /// <summary>
    /// Subscription Name
    /// </summary>
    public string SubscriptionName {
      get; set;
    }

    /// <summary>
    /// Generated event name 
    /// </summary>
    public string EventName {
      get; set;
    }

    /// <summary>
    /// Event Payload as a JSON string
    /// </summary>
    public string Payload {
      get; set;
    }

    /// <summary>
    /// Event Delivery Log Id
    /// </summary>
    public Guid DeliveryLogId {
      get; set;
    }

  }
}
