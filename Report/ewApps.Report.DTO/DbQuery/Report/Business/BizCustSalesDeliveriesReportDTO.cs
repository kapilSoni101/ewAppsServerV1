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
    /// This class is a DTO with consolidate information of <see cref="BizCustSalesDeliveriesReportDTO"/> .
    /// </summary>
    public class BizCustSalesDeliveriesReportDTO {

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery key.
        /// </summary>
        public string ERPDeliveryKey {
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
        /// Represents the Delivery Date
        /// </summary>
        public DateTime? DeliveryDate {
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
        /// Represents the ShippingType.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType text.
        /// </summary>
        public string ShippingTypeText {
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
