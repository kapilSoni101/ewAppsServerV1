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
    /// This class is a DTO with consolidate information of <see cref="RecentSalesOrdersDTO"/> .
    /// </summary>
    public class RecentSalesOrdersDTO {

        /// <summary>
        /// Unique id of SalesOrder.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Unique sales order referenc ekey for ERP.
        /// </summary>
        public string ERPSalesOrderKey {
            get; set;
        }


        /// <summary>
        /// Unique customer referenc ekey for ERP.
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Unique id of customer
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// SalesOrder customer name
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// SalesQuotation Posting date.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// SalesOrder End date
        /// </summary>
        public new DateTime? DocumentDate {
            get; set;
        }

        /// <summary>
        /// Total  amount of invoice
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// SalesOrder status
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// SalesOrder status Text
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Unique Number of Entity
        /// </summary>
        public string ERPDocNum {
            get; set;
        }

    }
}
