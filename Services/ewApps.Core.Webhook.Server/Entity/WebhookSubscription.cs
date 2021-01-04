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

namespace ewApps.Core.Webhook.Server {
    /// <summary>
    /// A subscriber may subscribe to events raised by a Webhook Server.
    /// It registers its subscription for the targeted Webhook Server.
    /// It may subscriber for a subset of the server events.
    /// </summary>

    [Table("WebhookSubscription", Schema = "core")]
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
        /// Comma seperated list of Events on whcih this subscription is listening
        /// </summary>
        public string SubscribeEvents {
            get; set;
        }

        /// <summary>
        /// Callback endpoint for the webhook for this subscription
        /// </summary>
        [Required]
        public string CallbackEndPoint {
            get; set;
        }
        /// <summary>
        /// Callback endpoint when server is shutting down 
        /// </summary>
        public string ServerShutDownCallBackEndPoint {
            get; set;
        }

        /// <summary>
        /// Subscription is active or not 
        /// </summary>
        public bool IsActive {
            get; set;
        }

        /// <summary>
        /// Event list on which Subscription is listning as the generic list form of the SubscribeEvents
        /// </summary>
        /// <remarks>It uses comma seperated events and converts in to list</remarks>
        [NotMapped]
        public List<string> SubscribeEventsAsList {
            get {
                if(!string.IsNullOrEmpty(SubscribeEvents))
                    return new List<string>(SubscribeEvents.Split(","));
                else
                    return null;
            }
            set {
                SubscribeEvents = string.Join(",", value);
            }
        }
    }
}