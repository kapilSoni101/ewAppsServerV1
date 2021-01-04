using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO
{
  public class BusVendorDetailDTO
  {
    /// <summary>
    /// 
    /// </summary>
    public BusVendorDTO Vendor
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<BusVendorContactDTO> VendorContactList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<BusVendorAddressDTO> BillToAddressList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<BusVendorAddressDTO> ShipToAddressList
    {
      get; set;
    }

    public List<BusinessAddressModelDTO> listBusinessAddress
    {
      get; set;
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //public CustomeAccDetailDTO VendorAcctDetail
    //{
    //  get; set;
    //}
  }
}
