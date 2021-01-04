using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {
    
    [Table("BAPurchaseEnquiryItem", Schema = "be")]
    public class BAPurchaseEnquiryItem : BaseEntity {


    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "BAPurchaseEnquiryItem";

    /// <summary>
    /// Represents the ERP delivery item key.
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPPurchaseEnquiryItemKey {
      get; set;
    }

    /// <summary>
    /// Represents the ERP Connector Key.
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPConnectorKey {
      get; set;
    }

    /// <summary>
    /// Represents the ERP contract key
    /// </summary>
    [Required]
    public Guid PurchaseEnquiryID {
      get; set;
    }

    /// <summary>
    /// Represents the ERP Purchase Inquiry Key
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPPurchaseInquiryKey {
      get; set;
    }

    /// <summary>
    /// Represents the ERP item Key
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string ERPItemKey {
      get; set;
    }

    /// <summary>
    /// Represents the ERP item id
    /// </summary>
    [Required]
    public Guid ItemID {
      get; set;
    }

    /// <summary>
    /// Represents the ERP   item name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ItemName {
      get; set;
    }

    /// <summary>
    /// Represents the item type.
    /// </summary>
    [Required]
    public int ItemType {
      get; set;
    }

    /// <summary>
    /// Represents the item type text.
    /// </summary>
    [Required]
    [MaxLength(20)]
    public int ItemTypeText {
      get; set;
    }

    /// <summary>
    /// Represents the  Item Type Text
    /// </summary>
    [Required]
    [MaxLength(100)]
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
    /// Represents the   delivery item unit.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal Unit {
      get; set;
    }

    /// <summary>
    /// Represents the   DiscountPercent.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal (18,5)")]
    public decimal DiscountPercent {
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
    /// Represents the Blanket Agreement No
    /// </summary>
    public string BlanketAgreementNo {
      get; set;
    }

    /// <summary>
    /// Represents the PriceSource
    /// </summary>
    public string PriceSource {
      get; set;
    }

    /// <summary>
    /// Represents the ERPShipToAddressKey
    /// </summary>
    [Required]
    public string ERPShipToAddressKey {
      get; set;
    }

    /// <summary>
    /// Represents the ShipToAdress
    /// </summary>
    [Required]
    public string ShipToAdress {
      get; set;
    }

    /// <summary>
    /// Represents the ERP Pay To AddressKey
    /// </summary>
    public string ERPPayToAddressKey {
      get; set;
    }

    /// <summary>
    /// Represents the Pay To Address
    /// </summary>
    public decimal PayToAddress {
      get; set;
    }
  }
}
