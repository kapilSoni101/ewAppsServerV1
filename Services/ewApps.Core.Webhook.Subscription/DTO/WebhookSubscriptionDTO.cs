/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */

using System.Collections.Generic;


namespace ewApps.Core.Webhook.Subscriber {
  /// <summary>
  /// DTO class to send the Subscriber object to the Webhook Server to Add by Http Call
  /// </summary>
  public class WebhookSubscriptionDTO {

    /// <summary>
    /// Name of the subscription
    /// </summary>  
    public string SubscriptionName
    {
      get; set;
    }

    /// <summary>
    /// Webhook server name for which the subscription is made
    /// </summary>
   
    public string ServerName {
      get; set;
    }

    /// <summary>
    /// Event list for the subscription
    /// </summary>
    public string SubscribeEvents {
      get;set;
    }

    /// <summary>
    /// Callback endpoint for the webhook for this subscription
    /// </summary>
   
    public string CallbackEndPoint {
      get; set;
    }

    /// <summary>
    /// Subscription is active or not 
    /// </summary>
    public bool IsActive {
      get; set;
    }
  }
}
