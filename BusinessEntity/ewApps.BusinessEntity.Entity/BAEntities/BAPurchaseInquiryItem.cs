using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {
    
    [Table("BAPurchaseInquiryItem", Schema = "be")]
    public class BAPurchaseInquiryItem : BaseEntity {


    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "BAPurchaseInquiryItem";

    /// <summary>
    /// Represents the ERP delivery item key.
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPPurchaseEnquiryItemKey {
      get; set;
    }

    /// <summary>
    /// Represents the ERP contract key
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string PurchaseEnquiryID {
      get; set;
    }

    /// <summary>
    /// Represents the ERP delivery id
    /// </summary>
    [Required]
    public Guid ItemID {
      get; set;
    }

    /// <summary>
    /// Represents the ERP   item key.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ItemName {
      get; set;
    }

    /// <summary>
    /// Represents the  Item Type Text
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string QuantityUnit {
      get; set;
    }

    /// <summary>
    /// Represents the   delivery item Quantity.
    /// </summary>
    [Required]
    public int Quantity {
      get; set;
    }

    /// <summary>
    /// Represents the   delivery item UnitPrice.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal UnitPrice {
      get; set;
    }

    /// <summary>
    /// Represents the   delivery item DiscountPercent.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Unit {
      get; set;
    }

    /// <summary>
    /// Represents the   delivery TotalLC
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal TotalLC {
      get; set;
    }

    /// <summary>
    /// Represents the price source.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string PriceSource {
      get; set;
    }

    /// <summary>
    /// Represents the ERPShipToAddressKey
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPShipToAddressKey {
      get; set;
    }

    /// <summary>
    /// Represents the ShipToAdress
    /// </summary>
    [Required]
    [MaxLength(4000)]
    public string ShipToAddress {
      get; set;
    }

    /// <summary>
    /// Represents the ERP pay to address ker;
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPPayToAddressKey {
      get; set;
    }

    /// <summary>
    /// Represents the pay to address.
    /// </summary>
    [Required]
    [MaxLength(4000)]
    public string PayToAddress {
      get; set;
    }
  }
}
