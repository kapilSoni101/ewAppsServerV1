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
  /// SalesOrderPkgDetail table represting all Sales Order Package Detail Information.
  /// </summary>
  [Table("SalesOrderPkgItem", Schema = "ship")]
    public class SalesOrderPkgItem : BaseEntity{

    /// <summary>
    /// UniqueId of SalesOrderPackage.
    /// </summary>
    [Required]
    public Guid SalesOrderPackageId
    {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Sales Order Detail.
    /// </summary>
    [Required]
    public Guid SalesOrderId
    {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Package Detail.
    /// </summary>
    [Required]
    public Guid PackageId
    {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Sales Order Item Detail.
    /// </summary>
    [Required]
    public Guid SalesOrderItemId
    {
      get; set;
    }

    /// <summary>
    /// Added Quantity in Sales Order Package
    /// </summary>
    [Required]
    public int AddedQuantity
    {
      get; set;
    }
  }
}
