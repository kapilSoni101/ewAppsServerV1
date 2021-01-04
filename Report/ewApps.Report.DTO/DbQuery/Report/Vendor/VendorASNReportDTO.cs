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
    /// This class is a DTO with consolidate information of <see cref="VendorASNReportDTO"/> .
    /// </summary>
    public class VendorASNReportDTO {

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Refrence Number.
        /// </summary>
        public string PoRefNo {
            get; set;
        }

        /// <summary>
        /// Name of Vendor.
        /// </summary>
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// Date of Vendor Shipment.
        /// </summary>
        public DateTime VendorShipmentDate {
            get; set;
        }

        /// <summary>
        /// Date of Document.
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }


        /// <summary>
        /// Date of Delivery.
        /// </summary>
        public DateTime DeliveryDate {
            get; set;
        }

        /// <summary>
        /// Way Bill Number.
        /// </summary>
        public string WayBillNo {
            get; set;
        }

        /// <summary>
        /// SoTracking.
        /// </summary>
        public string SoTracking {
            get; set;
        }

    }
}
