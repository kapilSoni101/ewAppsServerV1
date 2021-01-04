using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO
{
  public class BAVendorContactSyncDTO
  {
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
    public string ERPConnectorKey
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
    public string ERPVendorKey
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool Default
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
    public static BAVendorContact MapToEntity(BAVendorContactSyncDTO model)
    {

      BAVendorContact entity = new BAVendorContact()
      {

        Address = model.Address,
        Default = model.Default,
        Email = model.Email,
        Position = model.Position,
        ERPContactKey = model.ERPContactKey,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Telephone = model.Telephone,
        Title = model.Title,
        VendorId = model.VendorId,
        ERPConnectorKey = model.ERPConnectorKey,
        ERPVendorKey = model.ERPVendorKey,

      };

      return entity;
    }
  }
}
