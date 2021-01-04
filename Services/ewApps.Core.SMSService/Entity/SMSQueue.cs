﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 April 2019
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.SMSService {

    [Table("SMSQueue", Schema = "core")]
    public class SMSQueue {

        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// SMS delivery type i.e. SMS
        /// </summary>
        [Required]
        public int DeliveryType {
            get; set;
        }

        /// <summary>
        /// The type of the delivery sub-type i.e. Text or Voice SMS.
        /// </summary>
        public int DeliverySubType {
            get; set;
        }

        /// <summary>
        /// The recipient of SMS.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Recipient {
            get; set;
        }

        /// <summary>
        /// Part one of message.
        /// </summary>  
        [MaxLength(4000)]
        public string MessagePart1 {
            get; set;
        }

        /// <summary>
        /// Part two of message.
        /// </summary>    
        [Required]
        [MaxLength(4000)]
        public string MessagePart2 {
            get; set;
        }

        /// <summary>
        /// SMS delivery time (in UTC).
        /// </summary>
        [Required]
        public DateTime DeliveryTime {
            get; set;
        }

        /// <summary>
        /// The current state of SMS delivery.
        /// </summary>
        [Required]
        public int State {
            get; set;
        }

        public Guid ApplicationId {
            get; set;
        }

        public Guid NotificationId {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        [Required]
        public DateTime CreatedOn {
            get; set;
        }

        public Guid UpdatedBy {
            get; set;
        }

        [Required]
        public DateTime UpdatedOn {
            get; set;
        }

        public bool Deleted {
            get; set;
        } = false;

    }
}
