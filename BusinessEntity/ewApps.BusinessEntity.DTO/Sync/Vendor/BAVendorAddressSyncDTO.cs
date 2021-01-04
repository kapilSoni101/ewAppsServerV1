using ewApps.BusinessEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
 public  class BAVendorAddressSyncDTO
  {
    /// <summary>
    /// 
    /// </summary>
    public string ERPConnectorKey
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
    /// 
    /// </summary>
    public Guid VendorId
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Label
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int ObjectType
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string ObjectTypeText
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string AddressName
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Line1
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Line2
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Line3
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Street
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string StreetNo
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string City
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string ZipCode
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string State
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Country
    {
      get; set;
    }
    /// <summary>
    /// Maps model properties to entity.
    /// </summary>
    /// <param name="model">model with all required properties.</param>
    /// <returns>Customer entity</returns>
    public static BAVendorAddress MapToEntity(BAVendorAddressSyncDTO model)
    {

      BAVendorAddress entity = new BAVendorAddress()
      {

        Line1 = model.Street,
        Line2 = model.StreetNo,
        City = model.City,
        Country = model.Country,
        Line3 = model.Line3,
        AddressName = model.AddressName,
        Label = model.Label,
        ObjectType = model.ObjectType,
        ObjectTypeText = model.ObjectTypeText,
        VendorId = model.VendorId,
        ERPConnectorKey = model.ERPConnectorKey,
        ERPVendorKey = model.ERPVendorKey,
        State = model.State,
        StreetNo = model.StreetNo,
        Street = model.Street,
        ZipCode = model.ZipCode,

      };

      return entity;
    }
  }
}
