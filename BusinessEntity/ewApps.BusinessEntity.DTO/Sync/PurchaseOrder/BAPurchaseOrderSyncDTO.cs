using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO
{
  public class BAPurchaseOrderSyncDTO :BABaseSyncDTO {
        
        public string ERPPurchaseOrderKey
    {
      get; set;
    }

    public string ERPDocNum
    {
      get; set;
    }

    public string ERPConnectorKey
    {
      get; set;
    }

    public string ERPVendorKey
    {
      get; set;
    }

    public string VendorName
    {
      get; set;
    }
    public Guid VendorId
    {
      get; set;
    }

    public string ContactPerson
    {
      get; set;
    }


    public string VendorRefNo
    {
      get; set;
    }


    public string LocalCurrency
    {
      get; set;
    }

    public int Status
    {
      get; set;
    }


    public string StatusText
    {
      get; set;
    }

    public DateTime PostingDate
    {
      get; set;
    }

    public DateTime DeliveryDate
    {
      get; set;
    }

    public DateTime DocumentDate
    {
      get; set;
    }

    public string PickAndPackRemarks
    {
      get; set;
    }

    public string PurchaseEmployee
    {
      get; set;
    }


    public string Owner
    {
      get; set;
    }

    public decimal TotalBeforeDiscount
    {
      get; set;
    }

    public decimal TotalBeforeDiscountFC
    {
      get; set;
    }

    public decimal Discount
    {
      get; set;
    }

    public decimal DiscountFC
    {
      get; set;
    }

    public decimal Freight
    {
      get; set;
    }

    public decimal FreightFC
    {
      get; set;
    }

    public decimal Tax
    {
      get; set;
    }

    public decimal TaxFC
    {
      get; set;
    }

    public decimal TotalPaymentDue
    {
      get; set;
    }

    public decimal TotalPaymentDueFC
    {
      get; set;
    }

    public string Remarks
    {
      get; set;
    }

    public string ShipFromAddress
    {
      get; set;
    }

    public string ShipFromAddressKey
    {
      get; set;
    }

    public string ERPShipToAddressKey
    {
      get; set;
    }

    public string ShipToAddress
    {
      get; set;
    }

    public string ERPBillToAddressKey
    {
      get; set;
    }

    public string BillToAddress
    {
      get; set;
    }

    public int ShippingType
    {
      get; set;
    }

    public string ShippingTypeText
    {
      get; set;
    }

    public string OpType
    {
      get; set;
    }

    /// <summary>
    /// Purchase order item list.
    /// </summary>
    public List<BAPurchaseOrderItemSyncDTO> ItemList
    {
      get; set;
    }
    /// <summary>
    /// Purchase order AttachmentList list.
    /// </summary>
    public List<BAPurchaseOrderAttachmentSyncDTO> Attachments
    {
      get; set;
    }


    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAPurchaseOrder MapToEntity(BAPurchaseOrderSyncDTO model)
    {

      BAPurchaseOrder entity = new BAPurchaseOrder();
      entity.ERPConnectorKey = model.ERPConnectorKey;
      entity.ERPPurchaseOrderKey = model.ERPPurchaseOrderKey;
      entity.ERPDocNum = model.ERPDocNum;
      entity.ERPVendorKey = model.ERPVendorKey;
      entity.VendorId = model.VendorId;
      entity.VendorName = model.VendorName;
      entity.VendorRefNo = model.VendorRefNo;
      entity.ContactPerson = model.ContactPerson;
      entity.DeliveryDate = model.DeliveryDate;
      entity.Discount = model.Discount;
      entity.DiscountFC = model.DiscountFC;
      entity.DocumentDate = model.DocumentDate;
      entity.Freight = model.Freight;
      entity.FreightFC = model.FreightFC;
      entity.Tax = model.Tax;
      entity.TaxFC = model.TaxFC;
      entity.TotalBeforeDiscount = model.TotalBeforeDiscount;
      entity.TotalBeforeDiscountFC = model.TotalBeforeDiscountFC;
      entity.TotalPaymentDue = model.TotalPaymentDue;
      entity.TotalPaymentDueFC = model.TotalPaymentDueFC;
      entity.LocalCurrency = model.LocalCurrency;
      entity.Owner = model.Owner;
      entity.PickAndPackRemarks = model.PickAndPackRemarks;
      entity.PostingDate = model.PostingDate;
      entity.Remarks = model.Remarks;
     // entity.PurchaseEmployee = model.PurchaseEmployee;
      entity.ShippingType = model.ShippingType;
      entity.ShippingTypeText = model.ShippingTypeText;
      entity.Status = model.Status;
      entity.StatusText = model.StatusText;
      entity.ERPShipToAddressKey = model.ERPShipToAddressKey;
      entity.ShipToAddress = model.ShipToAddress;
      entity.ERPBillToAddressKey = model.ERPBillToAddressKey;
      entity.BillToAddress = model.BillToAddress;
      entity.ShipFromAddressKey = model.ShipFromAddressKey;
      entity.ShipFromAddress = model.ShipFromAddress;

      return entity;
    }

    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAPurchaseOrder MapToEntity(BAPurchaseOrderSyncDTO model, BAPurchaseOrder entity)
    {
      entity.ContactPerson = model.ContactPerson;
      entity.DeliveryDate = model.DeliveryDate;
      entity.Discount = model.Discount;
      entity.DiscountFC = model.DiscountFC;
      entity.DocumentDate = model.DocumentDate;
      entity.Freight = model.Freight;
      entity.FreightFC = model.FreightFC;
      entity.Tax = model.Tax;
      entity.TaxFC = model.TaxFC;
      entity.TotalBeforeDiscount = model.TotalBeforeDiscount;
      entity.TotalBeforeDiscountFC = model.TotalBeforeDiscountFC;
      entity.TotalPaymentDue = model.TotalPaymentDue;
      entity.TotalPaymentDueFC = model.TotalPaymentDueFC;
      entity.LocalCurrency = model.LocalCurrency;
      entity.Owner = model.Owner;
      entity.PickAndPackRemarks = model.PickAndPackRemarks;
      entity.PostingDate = model.PostingDate;
      entity.Remarks = model.Remarks;
      //entity.PurchaseEmployee = model.PurchaseEmployee;
      entity.ShippingType = model.ShippingType;
      entity.ShippingTypeText = model.ShippingTypeText;
      entity.Status = model.Status;
      entity.StatusText = model.StatusText;
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
