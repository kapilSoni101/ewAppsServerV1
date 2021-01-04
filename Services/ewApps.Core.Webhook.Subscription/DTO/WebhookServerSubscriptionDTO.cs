using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Webhook.Subscriber{
/// <summary>
/// DTO object to send servername and subscription Name to the 
/// subscriber on shutdown callback
/// </summary>
  public class WebhookServerSubscriberDTO {
    /// <summary>
    /// Webhook Server Name
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

  }
}
