/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

  [Table("SubscriptionPlan", Schema = "am")]
  public class SubscriptionPlan:BaseEntity {

    [Required]
    [MaxLength(100)]
    public string IdentityNumber {
      get; set;
    }

    /// <summary>
    /// The name  of Plan.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string PlanName {
      get; set;
    }

    [Required]
    public Guid AppId {
      get; set;
    }

    [Required]
    public int Term {
      get; set;
    }

    [Required]
    public int TermUnit {
      get; set;
    }

    /// <summary>
    /// The PriceInDollar.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,5)")]
    public decimal PriceInDollar {
      get; set;
    }

    [Required]
    public DateTime StartDate {
      get; set;
    }

    [Required]
    public DateTime EndDate {
      get; set;
    }

    [Required]
    public bool Active {
      get; set;
    }

    [Required]
    public int PaymentCycle {
      get; set;
    }

    public int? BusinessUserCount {
      get; set;
    }

    public int? CustomerUserCount {
      get; set;
    }

    [Required]
    public int TransactionCount {
      get; set;
    }

    [Required]
    public bool AllowUnlimitedTransaction {
      get; set;
    }

    [MaxLength(4000)]
    public string OtherFeatures {
      get; set;
    }

    [Required]
    public bool AutoRenewal { get; set; }

    [Required]
    public bool OneTimePlan { get; set; }

    public int? UserPerCustomerCount { get; set; }

    public int? ShipmentCount { get; set; }

    public int? ShipmentUnit { get; set; }

    [Required]
    public bool AllowUnlimitedShipment
    {
      get; set;
    }
  }
}
