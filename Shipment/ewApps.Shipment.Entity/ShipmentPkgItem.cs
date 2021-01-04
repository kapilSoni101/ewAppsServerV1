/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 09 April 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 09 April 2019
 */

using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Shipment.Entity {

  /// <summary>
  /// ShipmentPkgDetail table represting all Shipment Package Detail Information.
  /// </summary>
  [Table("ShipmentPkgItem", Schema = "ship")]
    public class ShipmentPkgItem:BaseEntity {

    /// <summary>
    /// Identity number to Identify Shipment  Detail.
    /// </summary>
    [Required]
    public Guid ShipmentId {
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
    /// Identity number to Identify Shipment Item Detail.
    /// </summary>
    [Required]
    public Guid ShipmentItemId {
      get; set;
    }

    /// <summary>
    /// Added Quantity in Shipment
    /// </summary>
    [Required]
    public int AddedQuantity {
      get; set;
    }

  }

}
