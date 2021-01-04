using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
namespace ewApps.BusinessEntity.Entity {

    // <summary>
    /// Invoice table storing all the invoice data.
    /// </summary>
    [Table("BAAPInvoice", Schema = "be")]
    public class BAAPInvoice : BaseEntity
    {

      /// <summary>
      /// The entity name.
      /// </summary>
      public const string EntityName = "BAAPInvoice";

      /// <summary>
      /// Unique key of invoice (Invoice generated from ERP).
      /// </summary>
      [MaxLength(100)]
      public string ERPAPInvoiceKey
      {
        get; set;
      }

      /// <summary>
      /// Unique number of invoice (Invoice generated from ERP).
      /// </summary>
      [MaxLength(100)]
      public string ERPDocNum
      {
        get; set;
      }

      /// <summary>
      /// Connector key of invoice (Invoice generated from ERP connector name).
      /// </summary>
      [MaxLength(100)]
      public string ERPConnectorKey
      {
        get; set;
      }

      /// <summary>
      /// Unique key of customer (customer generated from ERP portal).
      /// </summary>
      [MaxLength(100)]
      public string ERPVendorKey
      {
        get; set;
      }

      /// <summary>
      /// Vendor table unique id.
      /// </summary>
      [Required]
      public Guid VendorId
      {
        get; set;
      }

      /// <summary>
      /// Name of customer.
      /// </summary>
      [MaxLength(100)]
      public string VendorName
      {
        get; set;
      }

      /// <summary>
      /// Name of conatct person.
      /// </summary>
      [MaxLength(100)]
      public string ContactPerson
      {
        get; set;
      }

      /// <summary>
      /// Vendor referenceid is coming from ERP portal, If invoice generated from ERP.
      /// </summary>
      [MaxLength(100)]
      public string VendorRefNo
      {
        get; set;
      }

      /// <summary>
      /// Local currency from where invoice generated.
      /// </summary>
      [MaxLength(100)]
      public string LocalCurrency
      {
        get; set;
      }

      /// <summary>
      /// Status of invoice.
      /// </summary>
      public int Status
      {
        get; set;
      }

      /// <summary>
      /// Currenct status test.
      /// </summary>
      [MaxLength(100)]
      public string StatusText
      {
        get; set;
      }

      /// <summary>
      /// Invoice posting date. 
      /// </summary>
      public DateTime PostingDate
      {
        get; set;
      }

      /// <summary>
      /// Document date.
      /// </summary>
      public DateTime DocumentDate
      {
        get; set;
      }

      /// <summary>
      /// Due date.
      /// </summary>
      public DateTime? DueDate
      {
        get; set;
      }

      /// <summary>
      /// Sales Employees.
      /// </summary>
      [MaxLength(100)]
      public string SalesEmployee
      {
        get; set;
      }

      /// <summary>
      /// Owner of invoice.
      /// </summary>
      [MaxLength(100)]
      public string Owner
      {
        get; set;
      }

      /// <summary>
      /// Invoice actual amount without discount.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal TotalBeforeDiscount
      {
        get; set;
      }

      /// <summary>
      /// Invoice actual amount without discount.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal TotalBeforeDiscountFC
      {
        get; set;
      }

      /// <summary>
      /// Discount apply on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal Discount
      {
        get; set;
      }

      /// <summary>
      /// Discount apply on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal DiscountFC
      {
        get; set;
      }

      /// <summary>
      /// Freight charges apply on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal Freight
      {
        get; set;
      }
      /// <summary>
      /// Freight charges apply on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal FreightFC
      {
        get; set;
      }

      /// <summary>
      /// Tax charges apply on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal Tax
      {
        get; set;
      }

      /// <summary>
      /// Tax charges apply on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal TaxFC
      {
        get; set;
      }

      /// <summary>
      /// Due amount on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal TotalPaymentDue
      {
        get; set;
      }

      /// <summary>
      /// Due amount on Invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal TotalPaymentDueFC
      {
        get; set;
      }

      /// <summary>
      /// Appled amount.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal AppliedAmount
      {
        get; set;
      }

      /// <summary>
      /// Appled amount.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal AppliedAmountFC
      {
        get; set;
      }

      /// <summary>
      /// Balanace due on invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal BalanceDue
      {
        get; set;
      }

      /// <summary>
      /// Balanace due on invoice.
      /// </summary>
      [Column(TypeName = "decimal(18, 5)")]
      public decimal BalanceDueFC
      {
        get; set;
      }

      /// <summary>
      /// Note for invoice.
      /// </summary>
      [MaxLength(4000)]
      public string Remarks
      {
        get; set;
      }

      /// <summary>
      /// Ship to address.
      /// </summary>
      [MaxLength(4000)]
      public string ShipFromAddress
      {
        get; set;
      }

      /// <summary>
      /// Ship from address key, coming from ERP connector.
      /// </summary>
      [MaxLength(100)]
      public string ShipFromAddressKey
      {
        get; set;
      }

      /// <summary>
      /// Ship to address.
      /// </summary>
      [MaxLength(4000)]
      public string ShipToAddress
      {
        get; set;
      }

      /// <summary>
      /// Ship to address key, coming from ERP connector.
      /// </summary>
      [MaxLength(100)]
      public string ERPShipToAddressKey
      {
        get; set;
      }

      /// <summary>
      /// Bill to address key.
      /// </summary>
      [MaxLength(100)]
      public string ERPBillToAddressKey
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
      /// Shipping Type.
      /// </summary>
      public int ShippingType
      {
        get; set;
      }

      /// <summary>
      /// Shipping type text.
      /// </summary>
      [MaxLength(100)]
      public string ShippingTypeText
      {
        get; set;
      }

      /// <summary>
      /// Invoice tracking number.
      /// </summary>
      [MaxLength(100)]
      public string TrackingNo
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
      public IEnumerable<EwpErrorData> BrokenRules(BAAPInvoice entity)
      {
        //Check for application name is required.
        if (entity.VendorId == Guid.Empty)
          yield return new EwpErrorData()
          {
            ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
            Data = "Vendor",
            Message = "Vendor is required."
          };
      }

      #endregion

    }
  }