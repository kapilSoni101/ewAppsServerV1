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
    /// This class is a DTO with consolidate information of <see cref="VendAPInvoicesReportDTO"/> .
    /// </summary>
    public class VendOpenPurchaseLineReportDTO {

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
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
        public Guid VendorId {
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
        ///Item Id 
        /// </summary>
        public Guid ItemID  {
            get; set;
        }

        /// <summary>
        ///Name of Item
        /// </summary>
        public string ItemName {
            get; set;
        }

        /// <summary>
        ///Quantity Of Order
        /// </summary>
        public int OrderQuantity {
            get; set;
        }

        /// <summary>
        ///Quantity Of Open Orders
        /// </summary>
        public int OpenQuantity {
            get; set;
        }

        /// <summary>
        ///Purchase Order Key
        /// </summary>
        public string ERPPurchaseOrderKey {
            get;set;
        }

        /// <summary>
        ///Purchase Order Item Key
        /// </summary>
        public string ERPPurchaseOrderItemKey {
            get; set;
        }

        


    }
}
