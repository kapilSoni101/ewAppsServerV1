using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.DTO
{
  public class BAASNSyncDTO
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
    public Guid CustomerId
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
    /// 
    /// </summary>
    public string ERPASNKey
    {
      get; set;
    }

    public string ERPDocNum
    {
      get; set;
    }

    public string DeliveryNo
    {
      get; set;
    }

    public DateTime ShipDate
    {
      get; set;
    }

    public DateTime ExpectedDate
    {
      get; set;
    }

    public string TrackingNo
    {
      get; set;
    }


    public int ShipmentType
    {
      get; set;
    }

    public string ShipmentTypeText
    {
      get; set;
    }

    public string ShipmentPlan
    {
      get; set;
    }

    public string PackagingSlipNo
    {
      get; set;
    }
    public string LocalCurrency
    {
      get; set;
    }

    public decimal? TotalAmount
    {
      get; set;
    }


    public decimal? Discount
    {
      get; set;
    }

    public decimal? Freight
    {
      get; set;
    }

    public decimal? Tax
    {
      get; set;
    }

    public string OpType
    {
      get; set;
    }
    /// <summary>
    /// sales order item list.
    /// </summary>
    public List<BAASNItemSyncDTO> ItemList
    {
      get; set;
    }

    /// <summary>
    /// sales order AttachmentList list.
    /// </summary>
    public List<BAASNAttachmentSyncDTO> Attachments
    {
      get; set;
    }

    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAASN MapToEntity(BAASNSyncDTO model)
    {

      BAASN entity = new BAASN();
      entity.ERPConnectorKey = model.ERPConnectorKey;
      entity.ERPASNKey = model.ERPASNKey;
      entity.ERPDocNum = model.ERPDocNum;
      entity.ERPCustomerKey = model.ERPCustomerKey;
      entity.ExpectedDate = model.ExpectedDate;
      entity.LocalCurrency = model.LocalCurrency;
      entity.CustomerId = model.CustomerId;
      entity.CustomerName = model.CustomerName;
      entity.DeliveryNo = model.DeliveryNo;
      entity.Discount = model.Discount;
      entity.Freight = model.Freight;
      entity.Tax = model.Tax;
      entity.PackagingSlipNo = model.PackagingSlipNo;
      entity.ShipDate = model.ShipDate;
      entity.ShipmentPlan = model.ShipmentPlan;
      entity.ShipmentType = model.ShipmentType;
      entity.ShipmentTypeText = model.ShipmentTypeText;
      entity.TotalAmount = model.TotalAmount;
      entity.TrackingNo = model.TrackingNo;
      return entity;
    }
    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAASN MapToEntity(BAASNSyncDTO model, BAASN entity)
    {
      entity.ExpectedDate = model.ExpectedDate;
      entity.LocalCurrency = model.LocalCurrency;
      entity.DeliveryNo = model.DeliveryNo;
      entity.Discount = model.Discount;
      entity.Freight = model.Freight;
      entity.Tax = model.Tax;
      entity.PackagingSlipNo = model.PackagingSlipNo;
      entity.ShipDate = model.ShipDate;
      entity.ShipmentPlan = model.ShipmentPlan;
      entity.ShipmentType = model.ShipmentType;
      entity.ShipmentTypeText = model.ShipmentTypeText;
      entity.TotalAmount = model.TotalAmount;
      entity.TrackingNo = model.TrackingNo;
      return entity;
    }
  }
}
