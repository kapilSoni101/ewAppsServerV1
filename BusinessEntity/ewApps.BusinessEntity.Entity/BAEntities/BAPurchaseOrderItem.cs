using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    [Table("BAPurchaseOrderItem", Schema = "be")]
    public class BAPurchaseOrderItem:BaseEntity {

        public const string EntityName = "BAPurchaseOrderItem";

    /// <summary>
    /// Unique key of sales order item key coming from ERP connector.
    /// </summary>
    [MaxLength(100)]
    public string ERPPurchaseOrderItemKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of ERP connector.
    /// </summary>
    [MaxLength(100)]
    public string ERPConnectorKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of sales order table.
    /// </summary>
    [Required]
    public Guid PurchaseOrderId
    {
      get; set;
    }

    /// <summary>
    /// Unique key of sales order table coming from ERP connector.
    /// </summary>
    public string ERPPurchaseOrderKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of item coming from ERP connector.
    /// </summary>
    [MaxLength(100)]
    public string ERPItemKey
    {
      get; set;
    }

    [MaxLength(20)]
    public string LotNo
    {
      get; set;
    }

    /// <summary>
    /// Unique key of ItemMaster table.
    /// </summary>
    [Required]
    public Guid ItemId
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string SerialOrBatchNo
    {
      get; set;
    }

    /// <summary>
    /// Item name.
    /// </summary>
    [MaxLength(100)]
    public string ItemName
    {
      get; set;
    }

    /// <summary>
    /// # quanity of item.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal Quantity
    {
      get; set;
    }

    /// <summary>
    /// Unit of item.
    /// </summary>
    [MaxLength(100)]
    public string QuantityUnit
    {
      get; set;
    }

    /// <summary>
    /// Price of item.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal UnitPrice
    {
      get; set;
    }

    /// <summary>
    /// Price of item.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal UnitPriceFC
    {
      get; set;
    }

    /// <summary>
    /// Price of unit. It may be like per item/dozen/gram etc.
    /// </summary>
    [MaxLength(100)]
    public string Unit
    {
      get; set;
    }

    /// <summary>
    /// Discount percent in invoice.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal DiscountPercent
    {
      get; set;
    }

    /// <summary>
    /// Discount amount after applied discount on actual amount.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal DiscountAmount
    {
      get; set;
    }

    /// <summary>
    /// Discount amount after applied discount on actual amount.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal DiscountAmountFC
    {
      get; set;
    }

    /// <summary>
    ///  Tax code.
    /// </summary>
    [MaxLength(100)]
    public string TaxCode
    {
      get; set;
    }

    /// <summary>
    /// Tax persent appled on item.
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal TaxPercent
    {
      get; set;
    }

    /// <summary>
    /// TotalLC
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal TotalLC
    {
      get; set;
    }

    /// <summary>
    /// TotalLC
    /// </summary>
    [Column(TypeName = "decimal(18, 5)")]
    public decimal TotalLCFC
    {
      get; set;
    }

    [MaxLength(100)]
    public string Whse
    {
      get; set;
    }

    /// <summary>
    /// BlanketAgreementNo
    /// </summary>
    [MaxLength(100)]
    public string BlanketAgreementNo
    {
      get; set;
    }

    /// <summary>
    /// Ship from address.
    /// </summary>
    [MaxLength(4000)]
    public string ShipFromAddress
    {
      get; set;
    }

    /// <summary>
    /// ShipFromAddressKey will be not null if coming from ERP connector.
    /// </summary>
    [MaxLength(100)]
    public string ShipFromAddressKey
    {
      get; set;
    }

    /// <summary>
    /// ShipToAddress
    /// </summary>
    [MaxLength(4000)]
    public string ShipToAddress
    {
      get; set;
    }

    /// <summary>
    /// ShipTOAddressKey will be not null if coming from ERP connector.
    /// </summary>
    [MaxLength(100)]
    public string ERPShipToAddressKey
    {
      get; set;
    }

    /// <summary>
    /// Bill to address.
    /// </summary>
    [MaxLength(4000)]
    public string BillToAddress
    {
      get; set;
    }

    /// <summary>
    /// Bill to addressKey will be not null if coming from ERP connector.
    /// </summary>
    [MaxLength(100)]
    public string ERPBillToAddressKey
    {
      get; set;
    }

  }
}
//    ID
//ERPPurchaseOrderItemKey
//ERPConnectorKey
//PurchaseOrderId
//ERPPurchaseOrderKey
//ERPItemKey
//SerialOrBatchNo
//ItemID
//ItemName
//* ItemType
//Quantity
//QuantityUnit
//UnitPrice
//Unit
//DiscountPercent
//DiscountAmount
//TaxCode
//TaxPercent
//TotalLC
//Whse
//* BlanketAgreementNo
//ShipFromAddress
//ERPShipToAddressKey
//ShipToAddress
//ERPPayToAddressKey
//PayToAddress
//TenantId
//CreatedBy
//CreatedOn
//ModifiedBy
//ModifiedOn
//Deleted