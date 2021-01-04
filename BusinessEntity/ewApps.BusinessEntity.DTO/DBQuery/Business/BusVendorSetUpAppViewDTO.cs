using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class BusVendorSetUpAppViewDTO
  {
    public BusVendorSetUpAppViewDTO()
    {
      BillToAddressList = new List<VendorAddressDTO>();
      ShipToAddressList = new List<VendorAddressDTO>();
      VendorContactList = new List<VendorContactDTO>();
    }
    /// <summary>
    /// Unique id of vendor.
    /// </summary>
    public new Guid ID
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string PartnerType
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
    public string Currency
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public DateTime CreatedOn
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public Guid CreatedBy
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
    public int Status
    {
      get; set;
    }
   

    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public List<VendorContactDTO> VendorContactList
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public List<VendorAddressDTO> BillToAddressList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public List<VendorAddressDTO> ShipToAddressList
    {
      get; set;
    }
  }
}
