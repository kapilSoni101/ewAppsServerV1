/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.Webhook.Subscriber {

    /// <summary>
    /// Events send by the Server to the subscription are logged by this Entity Table. 
    /// Further Scheduler picks all incoming events from this Queue
    /// </summary>
    [Table("WebhookClientEventQueue", Schema = "core")]
    public class WebhookEvent {

        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Server Name
        /// </summary>
        [MaxLength(100)]
        public string ServerName {
            get; set;
        }

        /// <summary>
        /// Subscription Name
        /// </summary>
        [MaxLength(100)]
        public string SubscriptionName {
            get; set;
        }

        /// <summary>
        /// Generated event name from Webhook Server
        /// </summary>
        public string EventName {
            get; set;
        }

        /// <summary>
        /// Event object as a JSON string
        /// </summary>
        public string Payload {
            get; set;
        }

        /// <summary>
        /// Event received time
        /// </summary>
        public DateTime ReceivedDate {
            get; set;
        }
        /// <summary>
        /// Status
        /// </summary>
        public string Status {
            get; set;
        }

        /// <summary>
        /// Processed Date
        /// </summary>
        public DateTime ProcessedDate {
            get; set;
        }
    }
}
