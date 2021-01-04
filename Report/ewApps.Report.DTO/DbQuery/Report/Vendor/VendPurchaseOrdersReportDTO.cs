/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="VendPurchaseOrdersReportDTO"/> .
    /// </summary>
    public class VendPurchaseOrdersReportDTO {

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Status of Order.
        /// </summary>
        public string StatusText {
            get; set;
        }        

        /// <summary>
        /// Posting Date
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// vendor Id.
        /// </summary>
        public string VendorId {
            get; set;
        }

        /// <summary>
        /// Name of Vendor
        /// </summary>
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// Delivery Date 
        /// </summary>
        public DateTime DeliveryDate {
            get; set;
        }

        /// <summary>
        /// Document Date 
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// ShippingType
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Total Payment Due
        /// </summary>
        public Decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Shipming Type Text
        /// </summary>
        public string ShippingTypeText {
            get;set;
        }

        /// <summary>
        /// Erp Sales Order Key
        /// </summary>
        public string ERPPurchaseOrderKey {
            get;set;
        }
    }
}
