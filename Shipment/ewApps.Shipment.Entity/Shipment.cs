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
  /// Shipment table represting all Shipment Package Detail Information.
  /// </summary>
  [Table("Shipment", Schema = "ship")]
    public class Shipment:BaseEntity {

    /// <summary>
    /// The entity name Shipment.
    /// </summary>
    public const string EntityName = "Shipment";

    /// <summary>
    /// Represents shipment ref id.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ShipmentRefId {
      get; set;
    }

    /// <summary>
    /// The Type of Shipment
    /// </summary>
    [Required]
    public int ShipmentType {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Sales Order  Detail.
    /// </summary>
    [Required]
    public Guid SalesOrderId {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Customer Detail.
    /// </summary>
    [Required]
    public Guid CustomerId {
      get; set;
    }

    /// <summary>
    /// The Data and Time of Shipment Posting.
    /// </summary>
    [Required]
    public DateTime PostingOn {
      get; set;
    }

    /// <summary>
    /// The Shipment Delivery Date.
    /// </summary>
    [Required]
    public DateTime DeliveryDate {
      get; set;
    }

    /// <summary>
    /// The Amount of Shipment 
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Amount {
      get; set;
    }

    /// <summary>
    /// The Description of Shipment  
    /// </summary>
    [MaxLength(4000)]
    public string Description {
      get; set;
    }

    /// <summary>
    /// Shipment Tax Rate 
    /// </summary>
    [Column(TypeName = "decimal (18,5)")]
    public decimal TaxRate {
      get; set;
    }


    /// <summary>
    /// The SubTotal of Shipment 
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal SubTotal {
      get; set;
    }

    /// <summary>
    /// The Discount of Shipment
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Discount {
      get; set;
    }

    /// <summary>
    /// Freight shipment
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Freight {
      get; set;
    }

    /// <summary>
    /// The Tax of Shipment  
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Tax {
      get; set;
    }

    /// <summary>
    /// Total Amount of Shipment
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal TotalAmount {
      get; set;
    }

    /// <summary>
    /// The Status of Shipment
    /// </summary>
    [Required]
    public int Status {
      get; set;
    }

    /// <summary>
    /// The Status of Shipment
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string StatusText
    {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Carrier Detail.
    /// </summary>
    public Guid? CarrierId {
      get; set;
    }

    /// <summary>
    /// The Carrier Plan Id 
    /// </summary>
    [MaxLength(100)]
    public string CarrierPlanId {
      get; set;
    }


    /// <summary>
    /// The CarrierPickupDate of Shipment
    /// </summary>
    public DateTime? CarrierPickupDate {
      get; set;
    }

    /// <summary>
    /// The CarrierTransitDays of Shipment
    /// </summary>
    [MaxLength(100)]
    public string CarrierTransitDays {
      get; set;
    }

    /// <summary>
    /// The Shipment CarrierExpectedDeliveryDate 
    /// </summary>
    public DateTime? CarrierExpectedDeliveryDate {
      get; set;
    }

    /// <summary>
    /// The Carrier Freight 
    /// </summary>
    [Column(TypeName = "decimal (18,5)")]
    public decimal? CarrierFreight {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Billing Address Detail.
    /// </summary>
    [Required]
    public Guid BillingAddressId {
      get; set;
    }

    /// <summary>
    /// Identity number to Identify Shipping Address Detail.
    /// </summary>
    [Required]
    public Guid ShippingAddressId {
      get; set;
    }

    [MaxLength(100)]
    public string TrackingNumber {
      get; set;
    }

    /// <summary>
    /// Carrier Account No 
    /// </summary>
    [MaxLength(100)]
    public string CarrierAccNo {
      get; set;
    }

    /// <summary>
    /// From address id field.
    /// </summary>
    [Required]
    public Guid FromAddressId {
      get; set;
    }

    /// <summary>
    /// Shipper account number. 
    /// </summary>
    [MaxLength(100)]
    public string ShipperAccountNo {
      get; set;
    }

    /// <summary>
    /// Shipper act key of customer
    /// </summary>
    [MaxLength(100)]
    public string ShipperAccountKey {
      get; set;
    }

    /// <summary>
    /// True, ofif use customer carrier acc num.
    /// </summary>
    public bool? UseCustomerCarrierAccNo {
      get; set;
    }

  }
}
