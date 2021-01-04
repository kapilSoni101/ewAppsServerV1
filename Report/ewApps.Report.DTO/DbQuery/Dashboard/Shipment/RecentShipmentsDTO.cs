/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 17 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 17 May 2019
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="RecentShipmentsDTO"/> .
    /// </summary>
    public class RecentShipmentsDTO:BaseDTO {

        /// <summary>
        /// system generated shipment Id .
        /// </summary>
        public new Guid ID {
            get; set;
        }       

        ///// <summary>
        ///// system generated customer ref Id  .
        ///// </summary>
        //public string CustomerRefId {
        //    get; set;
        //}

        /// <summary>
        /// Name of Customer .
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// system generated shipment ref Id  .
        /// </summary>
        public string ShipmentRefId {
            get; set;
        }

        /// <summary>
        /// No of Shipping  .
        /// </summary>       
        public string ShippingNo {
            get; set;
        }

        /// <summary>
        /// Shipment posted date and time (in UTC).
        /// </summary>
        public DateTime PostedOn {
            get; set;
        }        

    }
}
