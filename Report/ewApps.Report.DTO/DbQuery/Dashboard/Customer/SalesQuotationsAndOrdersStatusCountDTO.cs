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
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="SalesQuotationsAndOrdersStatusCountDTO"/> .
    /// </summary>
    public class SalesQuotationsAndOrdersStatusCountDTO {

        /// <summary>
        /// total no of Sales Quation generated this week 
        /// </summary>
        public int SalesQuotaions {
            get; set;
        }

        /// <summary>
        /// total no of Sales Orders generated this week 
        /// </summary>
        public int SalesOrders {
            get; set;
        }

        /// <summary>
        /// Sales Order AMount
        /// </summary>
        public Decimal SalesOrderAmount {
            get; set;
        }

    }
}
