/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.Entity {

    /// <summary>
    /// Contains payment table property.
    /// </summary>
    [Table("RecurringPaymentLog", Schema = "pay")]
    public class RecurringPaymentLog :BaseEntity {


        /// <summary>
        /// ewApps recurring payment primary Id 
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// recurring payment id 
        /// </summary>
        public Guid RecurringPaymentId {
            get; set;
        }       

        /// <summary>
        /// scheduled date 
        /// </summary>
        public DateTime ScheduledDate {
            get; set;
        }

        /// <summary>
        /// processing date 
        /// </summary>
        public DateTime ProcessingDate {
            get; set;
        }

        /// <summary>
        /// status
        /// </summary>
        public int Status {
            get; set;
        }


        /// <summary>
        /// created date 
        /// </summary>
        public DateTime CreatedDate {
            get; set;
        }

        /// <summary>
        /// modified date 
        /// </summary>
        public DateTime ModifiedDate {
            get; set;
        }

    }
}
