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
    /// Entity Class - Linked with Database WebhookEventQueue Table
    /// 1. It keeps track of all the Events raised by the Webhook Server, 
    /// 2. Data is immediately entered in to a queue when event is raise.
    /// 3. It is Scheduler's reponsibility to process and dequeues Events for delivery.
    /// </summary>

    [Table("WebhookEventQueue", Schema = "core")]
    public class WebhookEvent {
        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of the Event raised by the Server
        /// </summary>
        
        public string EventName {
            get; set;
        }

        /// <summary>
        /// Event payload as JSON string
        /// </summary>
        public string Payload {
            get; set;
        }

        /// <summary>
        /// Event generation time
        /// </summary>
        public DateTime CreatedDate {
            get; set;
        }
    }
}
