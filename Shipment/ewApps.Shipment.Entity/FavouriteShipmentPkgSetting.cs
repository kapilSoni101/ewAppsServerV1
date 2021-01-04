/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 02 July 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 02 July 2019
 */

using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Shipment.Entity {

    /// <summary>
    /// FavourateShipmentPkgSetting table represting all FavourateShipmentPkgSetting Detail Information.
    /// </summary>
    [Table("FavouriteShipmentPkgSetting", Schema = "ship")]
    public class FavouriteShipmentPkgSetting:BaseEntity {

        /// <summary>
        /// Identity number to Identify Customer Detail.
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Identity number to Identify Business Detail.
        /// </summary>
        [Required]
        public Guid BusinessId {
            get; set;
        }

        /// <summary>
        /// Identity number to Identify Package Detail.
        /// </summary>
        public Guid PackageId {
            get; set;
        }

        /// <summary>
        /// Identity number to Identify Tenant Detail.
        /// </summary>
        [Required]
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// Identity number to Identify Items Detail.
        /// </summary>
        public string ItemIds {
            get; set;
        }

        public int Type {
            get; set;
        }
    }
}

