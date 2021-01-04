/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 02 February 2020
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 February 2020
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="VendRecentPurchaseOrder"/> .
    /// </summary>
    public class VendRecentPurchaseOrder {

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Identity Number 
        /// </summary>
        public string ERPPurchaseOrderKey {
            get; set;
        }

        /// <summary>
        /// Name of Vendor 
        /// </summary>
        public string VendorName {
            get; set;
        }


        /// <summary>
        /// Date Of Posting 
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// Date Of Document 
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Status
        /// </summary>
        public int Status {
            get; set;
        }

    }
}
