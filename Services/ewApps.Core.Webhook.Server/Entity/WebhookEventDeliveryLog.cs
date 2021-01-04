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

namespace ewApps.Core.Webhook.Server {
    /// <summary>
    /// When the server makes an attempt to deliver a WebhookEvent to a subscriber, 
    /// it logs delivery details in to the log table. 
    /// Note that the delivery may fails, 
    /// and is tried multiple times.
    /// Delivery status maintains the status of the last delivery.
    /// There are three possible statuses, as follows:
    /// InProgress: When a new log entry is created, before any delivery attempt.
    /// Failure: After a delivery attempt has failed.
    /// Success: After a delivery attempt has succeeded.
    /// Delivery attempt count is also kept to avoild multiple tries for delivery.
    /// If Delivery failed and falls in the criteria that the Scheduler stops delivery the event,
    /// the subscriber can gets the event
    /// by getting it directly from the server 
    /// and this process will also marked the status as Suceess. 
    /// </summary>

    [Table("WebhookEventDeliveryLog", Schema = "core")]
    public class WebhookEventDeliveryLog {
        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of the Webhook Server
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ServerName {
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
        /// Name of the event
        /// </summary>
        [Required]
        public string EventName {
            get; set;
        }

        /// <summary>
        /// Event payload as JSon string
        /// </summary>
        public string Payload {
            get; set;
        }

        /// <summary>
        /// Subscriber's Callback endpoint
        /// </summary>
        public string SubscriptionCallBack {
            get; set;
        }

        /// <summary>
        /// Time, When the event is queued
        /// </summary>
        public DateTime EventQueueTime {
            get; set;
        }

        /// <summary>
        /// Number of attempts scheduler makes to delivere event
        /// </summary>
        public int DeliveryAttempts {
            get; set;
        }

        /// <summary>
        /// Last delivery status: Success, Failure and InProgress
        /// </summary>
        [MaxLength(20)]
        public string DeliveryStatus {
            get; set;
        }

        /// <summary>
        /// Time of Last delivery attempt
        /// </summary>
        public DateTime LastDeliveryTime {
            get; set;
        }
    }
}
