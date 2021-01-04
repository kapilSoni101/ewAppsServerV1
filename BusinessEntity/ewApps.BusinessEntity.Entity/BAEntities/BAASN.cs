using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity
{

  /// <summary>
  /// Represents properties for ASN entity.
  /// </summary>
  [Table("BAASN", Schema = "be")]
  public class BAASN : BaseEntity
  {

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(50)]
    [Required]
    public string ERPConnectorKey
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(50)]
    [Required]
    public string ERPCustomerKey
    {
      get; set;
    }


    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid CustomerId
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    [Required]
    public string CustomerName
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(50)]
    [Required]
    public string ERPASNKey
    {
      get; set;
    }

    /// <summary>
    /// Unique number of ASN .
    /// </summary>
    [MaxLength(100)]
    public string ERPDocNum
    {
      get; set;
    }

    public string DeliveryNo
    {
      get; set;
    }

    public DateTime ShipDate
    {
      get; set;
    }

    public DateTime ExpectedDate
    {
      get; set;
    }

    public string TrackingNo
    {
      get; set;
    }


    public int ShipmentType
    {
      get; set;
    }

    public string ShipmentTypeText
    {
      get; set;
    }

    public string ShipmentPlan
    {
      get; set;
    }


    public string PackagingSlipNo
    {
      get; set;
    }
    public string LocalCurrency
    {
      get; set;
    }
    [Column(TypeName = "decimal (18,5)")]
    public decimal? TotalAmount
    {
      get; set;
    }

    [Column(TypeName = "decimal (18,5)")]
    public decimal? Discount
    {
      get; set;
    }

    [Column(TypeName = "decimal (18,5)")]
    public decimal? Freight
    {
      get; set;
    }

    [Column(TypeName = "decimal (18,5)")]
    public decimal? Tax
    {
      get; set;
    }
  }
}
