using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.CommonService;

namespace ewApps.BusinessEntity.DTO
{
  /// <summary>
  /// 
  /// </summary>
  public class BACustomerSyncDTO
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
    public string ERPCustomerKey
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerName
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
    public string Currency
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
    public string AddressLine1
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string AddressLine2
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
    public string MobilePhone
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Fax
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
    public string StatusText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Remarks
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string OpType
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<BACustomerPaymentDetailSyncDTO> CustomerPaymentDetailList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<BACustomerContactSyncDTO> CustomerContactList
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<BACustomerAddressSyncDTO> CustomerAddressList
    {
      get; set;
    }


    /// <summary>
    /// Maps model properties to entity.
    /// </summary>
    /// <param name="model">model with all required properties.</param>
    /// <returns>Customer entity</returns>
    public static BACustomer MapToEntity(BACustomerSyncDTO model)
    {
      if (model.StatusText.Equals("Y"))
      {
        model.Status = 1;
      }
      else
      {
        model.Status = 0;
      }
      BACustomer entity = new BACustomer()
      {

        AddressLine1 = model.AddressLine1,
        AddressLine2 = model.AddressLine2,
        BusinessPartnerTenantId = model.BusinessPartnerTenantId,
        City = model.City,
        Country = model.Country,
        Currency = model.Currency,
        //Currency = Convert.ToString(PicklistHelper.GetCurrencySymbolById(model.Currency)) ,
        Email = model.Email,
        ERPConnectorKey = model.ERPCustomerKey,
        Fax = model.Fax,
        FederalTaxID = model.FederalTaxID,
        Group = model.Group,
        MobilePhone = model.MobilePhone,
        CustomerName = model.CustomerName,
        ERPCustomerKey = model.ERPCustomerKey,
        State = model.State,
        Status = model.Status,
        StatusText = model.StatusText,

        StreetNo = model.StreetNo,
        Street = model.Street,
        Tel1 = model.Tel1,
        Tel2 = model.Tel2,
        Website = model.Website,
        ZipCode = model.ZipCode,
        Remarks = model.Remarks,
        ShippingType = model.ShippingType,
        ShippingTypeText = model.ShippingTypeText

      };

      return entity;
    }

    /// <summary>
    /// Maps model properties to entity.
    /// </summary>
    /// <param name="model">model with all required properties.</param>
    /// <returns>Customer entity</returns>
    public static BACustomer MapToEntity(BACustomerSyncDTO model, BACustomer entity)
    {
      if (model.StatusText.Equals("Y"))
      {
        model.Status = 1;
      }
      else
      {
        model.Status = 0;
      }

      entity.AddressLine1 = model.AddressLine1;
      entity.AddressLine2 = model.AddressLine2;
      entity.City = model.City;
      entity.Country = model.Country;
      entity.Currency = model.Currency;
      entity.Email = model.Email;
      entity.Fax = model.Fax;
      entity.FederalTaxID = model.FederalTaxID;
      entity.Group = model.Group;
      entity.MobilePhone = model.MobilePhone;
      entity.CustomerName = model.CustomerName;
      entity.State = model.State;
      entity.Status = model.Status;
      entity.StatusText = model.StatusText;
      entity.StreetNo = model.StreetNo;
      entity.Street = model.Street;
      entity.Tel1 = model.Tel1;
      entity.Tel2 = model.Tel2;
      entity.Website = model.Website;
      entity.ZipCode = model.ZipCode;
      entity.Remarks = model.Remarks;
      entity.ShippingType = model.ShippingType;
      entity.ShippingTypeText = model.ShippingTypeText;

      return entity;

    }
  }
}
