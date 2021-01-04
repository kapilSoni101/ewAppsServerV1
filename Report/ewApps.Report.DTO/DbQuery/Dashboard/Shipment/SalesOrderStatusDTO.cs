//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 17 April 2019
 * 
 * Contributor/s: Sanjeev khanna 
 * Last Updated On: 17 April 2019
 */

using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="SalesOrderStatusDTO"/> .
    /// </summary>
    public class SalesOrderStatusDTO:BaseDTO {

        /// <summary>
        /// total no of open or unpaid orders.
        /// </summary>
        public int TotalOpenOrders {
            get; set;
        }

        /// <summary>
        /// total no of partial delivered order.
        /// </summary>
        public int PartialDeliveredOrders {
            get; set;
        }

        /// <summary>
        /// total no of completed orders.
        /// </summary>
        public int CompletedOrders {
            get; set;
        }

        /// <summary>
        /// total no of sales.
        /// </summary>
        public decimal? TotalSales {
            get; set;
        }

        /// <summary>
        /// percentage of partial delivered .
        /// </summary>
        [NotMapped]
        public float PartialDeliveredOrdersPercentage {
            get; set;
        }

        /// <summary>
        /// percentage of paid invoices.
        /// </summary>
        [NotMapped]
        public float CompletedOrdersPercentage {
            get; set;
        }

        /// <summary>
        /// percentage of not deliver orders.
        /// </summary>
        [NotMapped]
        public float OpenOrdersPercentage {
            get; set;
        }
    }
}

