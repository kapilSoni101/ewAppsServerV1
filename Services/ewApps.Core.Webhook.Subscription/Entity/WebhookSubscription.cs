/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static ewApps.Core.Webhook.Subscriber.WebhookEventDelegate;

namespace ewApps.Core.Webhook.Subscriber {
    /// <summary>
    /// A subscriber may subscribe to events raised by a Webhook Server.
    /// It registers its subscription for the targeted Webhook Server.
    /// It may subscriber for a subset of the server events.
    /// </summary>
    /// 
    [Table("WebhookClientSubscription", Schema = "core")]
    public class WebhookSubscription {

        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of the subscription
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string SubscriptionName {
            get; set;
        }
        /// <summary>
        /// Webhook server name for which the subscription is made
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ServerName {
            get; set;
        }

        /// <summary>
        /// Webhook server Host End Point
        /// </summary>
        [Required]
        public string ServerHostEndPoint {
            get; set;
        }

        /// <summary>
        /// Event list for the subscription
        /// </summary>
        public string SubscribedEvents {
            get; set;
        }

        /// <summary>
        /// Event list the Subscription is listening as a Generic List<> of SubscriberEvents
        /// </summary>
        [NotMapped]
        public List<string> SubscribedEventsAsList {
            get {
                if (!string.IsNullOrEmpty(SubscribedEvents))
                    return new List<string>(SubscribedEvents.Split(","));
                else
                    return null;
            }
            set {
                SubscribedEvents = string.Join(",", value);
            }
        }
        /// <summary>
        /// Call back endpoint
        /// </summary>
        public string CallBackEndPoint {
            get; set;
        }
        /// <summary>
        /// Sutdown event call back endpoint
        /// </summary>
        public string ServerShutDownCallBackEndPoint {
            get; set;
        }

        /// <summary>
        /// Event delegate defined by client to handle the callbacks
        /// </summary>
        [NotMapped]
        public WebhookEventHandlerAsync EventDelegation {
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
