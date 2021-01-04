/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Kuldeep Singh Chauhan <kchauhan@eworkplaceapps.com>
 * Date: 08 Aug 2019
 */


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;


namespace ewApps.Shipment.Entity {
    [Table("CarrierPackageDetail", Schema = "ship")]
    public class CarrierPackageDetail:BaseEntity {

        /// <summary>
        /// Carrier Code for package
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string CarrierCode {
            get; set;
        }

        /// <summary>
        /// Package Code
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string CarrierPackageCode {
            get; set;
        }

        /// <summary>
        /// Package Master ID
        /// </summary>
        [Required]
        public Guid PackageMasterId {
            get; set;
        }

       

        /// <summary>
        /// Carrier Package ContainerType 
        /// </summary>
        [MaxLength(200)]
        public string ContainerType {
            get; set;
        }
    }
}
