/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 22 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 May 2019
 */

using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class contain shipmentservice count 
    /// </summary>
    public class ShipmentServiceNameAndCountDTO:BaseDTO {

        /// <summary>
        /// No of Shipment in AppService 
        /// </summary>
        public int NoOfShipment {
            get; set;
        }
        /// <summary>
        /// Name of the Service
        /// </summary>
        public string ServiceName {
            get; set;
        }
        /// <summary>
        /// Percentage of Shipment In Services
        /// </summary>
        [NotMapped]
        public float PercentageOfShipment {
            get; set;
        }

    }
}
