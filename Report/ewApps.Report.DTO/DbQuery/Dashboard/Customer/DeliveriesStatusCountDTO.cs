/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 15 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {
    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="DeliveriesStatusCountDTO"/> .
    /// </summary>
    public class DeliveriesStatusCountDTO {

        /// <summary>
        /// total no of outgoing Deliveries generated this week 
        /// </summary>
        public int OutgoingDeliveries {
            get; set;
        }

        /// <summary>
        /// Sales Deliveries AMount
        /// </summary>
        public Decimal OutgoingDeliveriesAmount {
            get; set;
        }
    }
}
