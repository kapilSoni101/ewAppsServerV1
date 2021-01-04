using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DTO
{
  /// <summary>
  /// 
  /// </summary>
  public class BusVendorDTO : BaseDTO
  {

    /// <summary>
    /// Unique id of customer.
    /// </summary>
    public new Guid ID
    {
      get; set;
    }
    /// <summary>
    /// Unique id of Busiess.
    /// </summary>
    public new Guid TenantId
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string VendorName
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string ERPVendorKey
    {
      get; set;
    }
    /// <summary>
    /// customer tenant id .
    /// </summary>
    public Guid BusinessPartnerTenantId
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Group
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string FederalTaxID
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Tel1
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Tel2
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Website
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string MobilePhone
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Email
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Currency
    {
      get; set;
    }

    [NotMapped]
    public string CurrencyCode
    {
      get; set;
    }

    public int Status
    {
      get; set;
    }

  }
}
