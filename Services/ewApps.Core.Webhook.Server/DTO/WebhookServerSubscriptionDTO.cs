using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Webhook.Server{
/// <summary>
/// DTO object to send servername and subscription Name to the 
/// Subscription on shutdown callback
/// </summary>
  public class WebhookServerSubscriptionDTO {
    /// <summary>
    /// Webhook Server NAme
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
