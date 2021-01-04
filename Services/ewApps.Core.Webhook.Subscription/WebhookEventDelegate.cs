/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.Webhook.Subscriber {
/// <summary>
/// Static class to define delegate 
/// </summary>
  public static class WebhookEventDelegate {
        /// <summary>
        /// Delegate defination for handling the call back
        /// </summary>
        /// <param name="subscription">Subscription object as the subscriber can defined multiple subscriptions</param>
        /// <param name="webhookEvent">List of event to handle</param>
        /// <param name="token">cancellation token</param>
        /// <returns>void</returns>
    public delegate Task WebhookEventHandlerAsync(WebhookSubscription subscription, List<WebhookEvent> webhookEvent, CancellationToken token);
  }
}
