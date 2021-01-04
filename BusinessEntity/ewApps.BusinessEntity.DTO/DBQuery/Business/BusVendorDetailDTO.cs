using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class BusVendorDetailDTO
  {
    public BusVendorDetailDTO()
    {
      BillToAddressList = new List<VendorAddressDTO>();
      ShipToAddressList = new List<VendorAddressDTO>();
    }
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
    public List<VendorContactDTO> VendorContactList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<VendorAddressDTO> BillToAddressList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<VendorAddressDTO> ShipToAddressList
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
