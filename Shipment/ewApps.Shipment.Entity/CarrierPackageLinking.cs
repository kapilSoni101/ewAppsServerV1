/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 May 2019
 * 
 * Contributor/s: Amit Mundra
 * Last Updated On: 15 May 2019
 */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Shipment.Entity {

    /// <summary>
    /// CarrierPackageDetail table represting all Shipment CarrierPackageDetail Data Information.
    /// </summary>
    [Table("CarrierPackageLinking", Schema = "ship")]
    public class CarrierPackageLinking:BaseEntity {

        /// <summary>
        /// The Code of Carrier  
        /// </summary>
        [Required]
        [StringLength(100)]
        public string CarrierCode {
            get; set;
        }

        /// <summary>
        /// The Code of Carrier Package
        /// </summary>
        [Required]
        [StringLength(100)]
        public string CarrierPackageCode {
            get; set;
        }

        /// <summary>
        /// Package conatiner type.
        /// </summary>
        [StringLength(250)]
        public string ContainerType {
            get;set;
        }

        /// <summary>
        /// Identity number to Identify Package Master Detail.
        /// </summary>
        [Required]
        public Guid PackageMasterId {
            get; set;
        }

    }
}
