using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService; 

 namespace ewApps.AppPortal.Entity {

  [Table("Vendor", Schema = "ap")]
  public class Vendor : BaseEntity
  {


    /// <summary>
    /// 
    /// </summary>
    public Guid BusinessPartnerTenantId
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool CanUpdateCurrency
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool Configured
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

    /// <summary>
    /// 
    /// </summary>
    public string IdentityNumber
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string DateTimeFormat
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int DecimalPrecision
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string DecimalSeperator
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string GroupSeperator
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string GroupValue
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Language
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string TimeZone
    {
      get; set;
    }

  } 

}