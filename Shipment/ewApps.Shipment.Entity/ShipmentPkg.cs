/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 21 May 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 21 May 2019
 */

using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Shipment.Entity {

    /// <summary>
    /// ShipmentPackage table represting all ShipmentPackage Detail Information.
    /// </summary>
    [Table("ShipmentPkg",Schema = "ship") ]
    public class ShipmentPkg:BaseEntity {

        /// <summary>
        /// Identity number to Identify Sales Order Detail.
        /// </summary>
        [Required]
        public Guid SalesOrderId {
            get; set;
        }

        /// <summary>
        /// Identity number to Identify Package Detail.
        /// </summary>
        [Required]
        public Guid PackageId {
            get; set;
        }

        /// <summary>
        /// Total items in a package
        /// </summary>
        [Required]
        public int TotalItems {
                get; set;
            }

        /// <summary>
        /// shipment id 
        /// </summary>
        public Guid ShipmentId
        {
          get; set;
        }

  }
}

