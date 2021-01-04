using ewApps.BusinessEntity.Entity;
using System;

namespace ewApps.BusinessEntity.DTO
{
  public class VendorContactDTO
  {
    /// <summary>
    /// Unique id of customer.
    /// </summary>
    public new Guid ID
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ERPContactKey
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
    public string FirstName
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string LastName
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Title
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Position
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Address
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Telephone
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
    /// Maps model properties to entity.
    /// </summary>
    /// <param name="model">model with all required properties.</param>
    /// <returns>Customer entity</returns>
    public static BAVendorContact MapToEntity(VendorContactDTO model, string ERPVendorKey, string ERPConnectorKey)
    {

      BAVendorContact entity = new BAVendorContact()
      {

        Address = model.Address,
        Email = model.Email,
        Position = model.Position,
        ERPContactKey = model.ERPContactKey,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Telephone = model.Telephone,
        Title = model.Title,
        VendorId = model.VendorId,
        ERPConnectorKey = ERPConnectorKey,
        ERPVendorKey = ERPVendorKey,

      };

      return entity;
    }

    public static BAVendorContact CopyToEntity(VendorContactDTO model, BAVendorContact entity, string ERPVendorKey, string ERPConnectorKey)
    {


      entity.Address = model.Address;
      entity.Email = model.Email;
      entity.Position = model.Position;
      entity.ERPContactKey = model.ERPContactKey;
      entity.FirstName = model.FirstName;
      entity.LastName = model.LastName;
      entity.Telephone = model.Telephone;
      entity.Title = model.Title;
      entity.VendorId = model.VendorId;
      entity.ERPConnectorKey = ERPConnectorKey;
      entity.ERPVendorKey = ERPVendorKey;


      return entity;
    }
  }
}
