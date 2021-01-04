using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO
{
  public class BAPurchaseOrderItemSyncDTO
  {
    /// <summary>
    /// Unique key of Purchase order item key coming from ERP connector.
    /// </summary>        
    public string ERPPurchaseOrderItemKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of ERP connector.
    /// </summary>

    public string ERPConnectorKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of Purchase order table coming from ERP connector.
    /// </summary>
    public string ERPPurchaseOrderKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of item coming from ERP connector.
    /// </summary>        
    public string ERPItemKey
    {
      get; set;
    }

    public string LotNo
    {
      get; set;
    }

    public Guid ItemId
    {
      get; set;
    }
    public Guid PurchaseOrderId
    {
      get; set;
    }
    public string SerialOrBatchNo
    {
      get; set;
    }

    public string ItemName
    {
      get; set;
    }

    public decimal Quantity
    {
      get; set;
    }

    /// <summary>
    /// Unit of item.
    /// </summary>        
    public string QuantityUnit
    {
      get; set;
    }

    /// <summary>
    /// Price of item.
    /// </summary>
    public decimal UnitPrice
    {
      get; set;
    }

    public decimal UnitPriceFC
    {
      get; set;
    }

    /// <summary>
    /// Price of unit. It may be like per item/dozen/gram etc.
    /// </summary>        
    public string Unit
    {
      get; set;
    }

    /// <summary>
    /// Discount percent in invoice.
    /// </summary>
    public decimal DiscountPercent
    {
      get; set;
    }

    /// <summary>
    /// Discount amount after applied discount on actual amount.
    /// </summary>
    public decimal DiscountAmount
    {
      get; set;
    }

    public decimal DiscountAmountFC
    {
      get; set;
    }


    /// <summary>
    ///  Tax code.
    /// </summary>        
    public string TaxCode
    {
      get; set;
    }

    /// <summary>
    /// Tax persent appled on item.
    /// </summary>
    public decimal TaxPercent
    {
      get; set;
    }

    /// <summary>
    /// TotalLC
    /// </summary>
    public decimal TotalLC
    {
      get; set;
    }

    public decimal TotalLCFC
    {
      get; set;
    }

    public string Whse
    {
      get; set;
    }

    /// <summary>
    /// BlanketAgreementNo
    /// </summary>

    public string BlanketAgreementNo
    {
      get; set;
    }

    /// <summary>
    /// Ship from address.
    /// </summary>
    public string ShipFromAddress
    {
      get; set;
    }

    /// <summary>
    /// ShipFromAddressKey will be not null if coming from ERP connector.
    /// </summary>
    public string ShipFromAddressKey
    {
      get; set;
    }

    /// <summary>
    /// ShipToAddress
    /// </summary>
    public string ShipToAddress
    {
      get; set;
    }

    /// <summary>
    /// ShipTOAddressKey will be not null if coming from ERP connector.
    /// </summary>        
    public string ERPShipToAddressKey
    {
      get; set;
    }

    /// <summary>
    /// Bill to address.
    /// </summary>
    public string BillToAddress
    {
      get; set;
    }

    /// <summary>
    /// Bill to addressKey will be not null if coming from ERP connector.
    /// </summary>

    public string ERPBillToAddressKey
    {
      get; set;
    }

    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAPurchaseOrderItem MapToEntity(BAPurchaseOrderItemSyncDTO model)
    {
      BAPurchaseOrderItem entity = new BAPurchaseOrderItem();
      entity.ERPConnectorKey = model.ERPConnectorKey;
      entity.ERPPurchaseOrderKey = model.ERPPurchaseOrderKey;
      entity.ERPItemKey = model.ERPItemKey;
      entity.PurchaseOrderId = model.PurchaseOrderId;
      entity.ItemId = model.ItemId;
      entity.ERPPurchaseOrderItemKey = model.ERPPurchaseOrderItemKey;
      entity.BlanketAgreementNo = model.BlanketAgreementNo;
      entity.DiscountAmount = model.DiscountAmount;
      //entity.DiscountAmountFC = model.DiscountAmountFC;
      entity.DiscountPercent = model.DiscountPercent;
      entity.ItemName = model.ItemName;
      entity.Quantity = model.Quantity;
      entity.QuantityUnit = model.QuantityUnit;
      entity.SerialOrBatchNo = model.SerialOrBatchNo;
      entity.TaxCode = model.TaxCode;
      entity.TaxPercent = model.TaxPercent;
      entity.TotalLC = model.TotalLC;
      entity.LotNo = model.LotNo;
      entity.TotalLCFC = model.TotalLCFC;
      entity.Unit = model.Unit;
      entity.UnitPrice = model.UnitPrice;
      entity.UnitPriceFC = model.UnitPriceFC;
      entity.Whse = model.Whse;

      entity.ERPShipToAddressKey = model.ERPShipToAddressKey;
      entity.ShipToAddress = model.ShipToAddress;
      entity.ERPBillToAddressKey = model.ERPBillToAddressKey;
      entity.BillToAddress = model.BillToAddress;
      entity.ShipFromAddressKey = model.ShipFromAddressKey;
      entity.ShipFromAddress = model.ShipFromAddress;

      return entity;
    }

    
  }
}
