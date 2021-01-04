/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 18 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 18 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="BizCustSalesQuotationsReportDTO"/> .
    /// </summary>
    public class BizCustSalesQuotationsReportDTO {

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// ERPSalesQuotationKey will be not null if comingfrom ERP connector.
        /// It unique id of SalesQuotation table of ERP connector table.
        /// </summary>        
        public string ERPSalesQuotationKey {
            get; set;
        }

        /// <summary>
        /// ERPCustomerKey will be not null if coming from ERP connector.
        /// It unique id of Customer table of ERP connector table.
        /// </summary>

        public string ERPCustomerKey {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Customer name.
        /// </summary>

        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Represents the delivery status.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Represents the status text.
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Represents the posting date of delivery.
        /// </summary>
        public DateTime? PostingDate {
            get; set;
        }

        /// <summary>
        /// Represents the ValidUntil
        /// </summary>
        public DateTime? ValidUntil {
            get; set;
        }

        /// <summary>
        /// Represents the DocumentDate
        /// </summary>
        public DateTime? DocumentDate {
            get; set;
        }

        /// <summary>
        /// Represents the Total PaymentDue
        /// </summary>
        public decimal? TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Represents the Remarks
        /// </summary>
        public string Remarks {
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
