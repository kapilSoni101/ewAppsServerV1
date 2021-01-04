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
  /// ShipmentItem table represting all Shipment Item Detail Information.
  /// </summary>
  [Table("ShipmentItem", Schema = "ship")]
    public class ShipmentItem : BaseEntity {
 
    /// <summary>
    /// Identity number to Identify Shipment  Detail.
    /// </summary>
    [Required]
    public Guid ShipmentId
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
    /// Identity number to Identify Item Detail.
    /// </summary>
    [Required]
    public Guid ItemId
    {
      get; set;
    }

    /// <summary>
    /// Item Quantity in Shipment
    /// </summary>
    [Required]
    public int ItemQuantity
    {
      get; set;
    }

    /// <summary>
    /// The Unit Price of Item  
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal ItemUnitPrice
    {
      get; set;
    }

    /// <summary>
    /// The Discount On Shipment Item  
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Discount
    {
      get; set;
    }

    /// <summary>
    /// The Tax on Shipment
    /// </summary>
    [Required]
    public bool Tax
    {
      get; set;
    }

    /// <summary>
    /// Total Item Price  
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal ItemTotatPrice
    {
      get; set;
    }

    /// <summary>
    /// Name of item
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ItemName
    {
      get; set;
    }

    /// <summary>
    /// Item code value of item
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ItemCode
    {
      get; set;
    }

    /// <summary>
    /// The Weight of Item  
    /// </summary>   
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Weight
    {
      get; set;
    }

    /// <summary>
    /// The Weight of Unit  
    /// </summary>   
    [Required]
    [MaxLength(20)]
    public string WeightUnit
    {
      get; set;
    }

    /// <summary>
    /// The Height of Package  
    /// </summary>    
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Height
    {
      get; set;
    }

    /// <summary>
    /// The Width of Package  
    /// </summary>    
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Width
    {
      get; set;
    }

    /// <summary>
    /// The Length of Package  
    /// </summary>    
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Length
    {
      get; set;
    }

    /// <summary>
    /// The Size unit
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string SizeUnit
    {
      get; set;
    }


  }
}
