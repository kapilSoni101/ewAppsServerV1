using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity
{
  /// <summary>
  /// Represents properties for vendor entity.
  /// </summary>
  [Table("BAVendor", Schema = "be")]
  public class BAVendor : BaseEntity
  {

    /// <summary>
    /// SAP connector key .
    /// </summary>
    [MaxLength(50)]
    [Required]
    public string ERPConnectorKey
    {
      get; set;
    }

    /// <summary>
    /// SAP customer key .
    /// </summary>
    [MaxLength(50)]
    [Required]
    public string ERPVendorKey
    {
      get; set;
    }

    /// <summary>
    /// Name of customer .
    /// </summary>
    [MaxLength(100)]
    [Required]
    public string VendorName
    {
      get; set;
    }

    /// <summary>
    /// customer tenant id .
    /// </summary>
    [Required]
    public Guid BusinessPartnerTenantId
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(20)]
    public string Group
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(20)]
    public string Currency
    {
      get; set;
    }

    public int CurrencyCode
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string FederalTaxID
    {
      get; set;
    }

    /// <summary>
    /// Customer Address 1 .
    /// </summary>
    [MaxLength(100)]
    public string AddressLine1
    {
      get; set;
    }

    /// <summary>
    ///  Customer address 2 .
    /// </summary>
    [MaxLength(100)]
    public string AddressLine2
    {
      get; set;
    }

    /// <summary>
    ///  Customer Address street .
    /// </summary>
    [MaxLength(20)]
    public string Street
    {
      get; set;
    }

    /// <summary>
    /// Customer Address street number .
    /// </summary>
    [MaxLength(20)]
    public string StreetNo
    {
      get; set;
    }

    /// <summary>
    /// Customer Address city .
    /// </summary>
    [MaxLength(20)]
    public string City
    {
      get; set;
    }

    /// <summary>
    /// Customer Address zipcode .
    /// </summary>
    [MaxLength(20)]
    public string ZipCode
    {
      get; set;
    }

    /// <summary>
    /// Customer Address state .
    /// </summary>
    [MaxLength(20)]
    public string State
    {
      get; set;
    }

    /// <summary>
    /// Customer Address country .
    /// </summary>
    [MaxLength(20)]
    public string Country
    {
      get; set;
    }

    /// <summary>
    /// Customer  telephone number1 .
    /// </summary>
    [MaxLength(20)]
    public string Tel1
    {
      get; set;
    }

    /// <summary>
    /// Customer  telephone number2 .
    /// </summary>
    [MaxLength(20)]
    public string Tel2
    {
      get; set;
    }

    /// <summary>
    /// Customer  mobile number .
    /// </summary>
    [MaxLength(20)]
    public string MobilePhone
    {
      get; set;
    }

    /// <summary>
    /// Customer  fax number.
    /// </summary>
    [MaxLength(20)]
    public string Fax
    {
      get; set;
    }

    /// <summary>
    /// Customer  email .
    /// </summary>
    [MaxLength(50)]
    public string Email
    {
      get; set;
    }

    /// <summary>
    /// Customer  website .
    /// </summary>
    [MaxLength(50)]
    public string Website
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int? ShippingType
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(20)]
    public string ShippingTypeText
    {
      get; set;
    }


    /// <summary>
    /// 
    /// </summary>
    public int? Status
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(20)]
    public string StatusText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string Remarks
    {
      get; set;
    }

  }
}