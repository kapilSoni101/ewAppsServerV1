/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.DTO {

    /// <summary>
    /// Keppes Shipper account related information
    /// </summary>
    public class ShipperAccountDTO {

        /// <summary>
        /// Primarykey
        /// </summary>
        public Guid Id {
            get; set;
        }

        /// <summary>
        /// ShipperId/TenantId, given by application , Composite key
        /// </summary>
        public string ShipperId {
            get; set;
        }
        /// <summary>
        /// Carrier code , for which this account setting should be used , Composite key
        /// </summary>
        public string CarrierCode {
            get; set;
        }
        /// <summary>
        /// FedEx - Developer Key
        /// UPS - User 
        /// </summary>
        public string UserName {
            get; set;
        }
        /// <summary>
        /// FedEx - Developer Password
        /// UPS -Password of the account
        /// </summary>
        public string Password {
            get; set;
        }
        /// <summary>
        /// FedEx - Account Number
        /// UPS -Account NUmber
        /// </summary>
        public string AccountNumber {
            get; set;
        }

        /// <summary>
        /// UPS - Access Key
        /// FEDEX - not used
        /// </summary>
        public string AccessKey {
            get; set;
        }

        /// <summary>
        /// Account Key, for which this account setting should be used , Composite key, it should be unique for a Shipper and CarrierCode
        /// </summary>
        public string AccountKey {
            get; set;
        }
        /// <summary>
        /// UPS - Not Used
        /// FedEX - Meter Number
        /// </summary>
        public string MeterNumber {
            get; set;
        }
        /// <summary>
        /// Record creation time stamp
        /// </summary>
        public DateTime CreatedDate {
            get; set;
        }

    }
}
