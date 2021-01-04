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
    /// A Webhook server primarily raises its events and delivers them to its subscribers.
    /// An app may include many Webhook servers. 
    /// Each webhook server may have many subscribers,
    /// each subscriber subscribing for a sub-set of server's events.
    ///  </summary>
    ///  
    [Table("WebhookServer", Schema = "core")]
    public class WebhookServer {

        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of the webhook Server
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ServerName {
            get; set;
        }

        /// <summary>
        ///  Comma Seperated List of events raised by the server
        /// </summary>
        public string ServerEvents {
            get; set;
        }

        /// <summary>
        /// List of events raised by the server in form of Generic List 
        /// mapped to the Comma Seperated ServerEvents
        /// </summary>
        [NotMapped]
        public List<string> ServerEventsAsList {
            get {
                if(!string.IsNullOrEmpty(ServerEvents))
                    return new List<string>(ServerEvents.Split(","));
                else
                    return null;

            }
            set {
                ServerEvents = string.Join(",", value);
            }
        }

    }
}
