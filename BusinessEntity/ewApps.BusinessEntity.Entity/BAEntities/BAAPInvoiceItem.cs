using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {
  [Table("BAAPInvoiceItem", Schema = "be")]
  /// <summary>
  /// InvoiceItem table storing all the invoice item data.
  /// </summary>

  public class BAAPInvoiceItem : BaseEntity
  {

    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "BAAPInvoiceItem";

    [MaxLength(100)]
    public string ERPAPInvoiceItemKey
    {
      get; set;
    }

    public Guid APInvoiceID
    {
      get; set;
    }

    [MaxLength(100)]
    public string ERPAPInvoiceKey
    {
      get; set;
    }

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

    [MaxLength(100)]
    public string SerialOrBatchNo
    {
      get; set;
    }

    public Guid ItemId
    {
      get; set;
    }

    [MaxLength(100)]
    public string ItemName
    {
      get; set;
    }

    [MaxLength(100)]
    public string ItemType
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal Quantity
    {
      get; set;
    }

    [MaxLength(100)]
    public string QuantityUnit
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal UnitPrice
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal UnitPriceFC
    {
      get; set;
    }

    [MaxLength(100)]
    public string Unit
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal DiscountPercent
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal DiscountAmount
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal DiscountAmountFC
    {
      get; set;
    }

    [MaxLength(100)]
    public string TaxCode
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal TaxPercent
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal TotalLC
    {
      get; set;
    }

    [Column(TypeName = "decimal(18, 5)")]
    public decimal TotalLCFC
    {
      get; set;
    }

    [MaxLength(100)]
    public string ERPShipToAddressKey
    {
      get; set;
    }

    [MaxLength(4000)]
    public string ShipFromAddress
    {
      get; set;
    }

    [MaxLength(100)]
    public string ShipFromAddressKey
    {
      get; set;
    }

    [MaxLength(4000)]
    public string ShipToAddress
    {
      get; set;
    }

    [MaxLength(100)]
    public string ERPBillToAddressKey
    {
      get; set;
    }

    [MaxLength(4000)]
    public string BillToAddress
    {
      get; set;
    }

    #region IValidator<App> Members

    /// <inheritdoc />
    public bool Validate(out IList<EwpErrorData> brokenRules)
    {
      brokenRules = BrokenRules(this).ToList<EwpErrorData>();
      return brokenRules.Count > 0;
    }

    /// <inheritdoc />
    public IEnumerable<EwpErrorData> BrokenRules(BAAPInvoiceItem entity)
    {
      //Check for application name is required.
      if (entity.APInvoiceID == Guid.Empty)
      {
        yield return new EwpErrorData()
        {
          ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
          Data = "InvoiceId",
          Message = "Invoiceid is required for invoice item."
        };
      }

    }

    #endregion

  }
}