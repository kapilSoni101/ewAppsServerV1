// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO
{

  /// <summary>
  /// Invoice table storing all the invoice data.
  /// </summary>
  public class BAAPInvoiceSyncDTO
  {

    /// <summary>
    /// Unique key of invoice (Invoice generated from ERP).
    /// </summary>
    public string ERPAPInvoiceKey
    {
      get; set;
    }

    public string ERPDocNum
    {
      get; set;
    }

    /// <summary>
    /// Connector key of invoice (Invoice generated from ERP connector name).
    /// </summary>
    public string ERPConnectorKey
    {
      get; set;
    }

    /// <summary>
    /// Unique key of Vendor (Vendor generated from ERP portal).
    /// </summary>

    public string ERPVendorKey
    {
      get; set;
    }

    /// <summary>
    /// Vendor table unique id.
    /// </summary>

    public Guid VendorId
    {
      get; set;
    }
    /// <summary>
    /// Name of Vendor.
    /// </summary>

    public string VendorName
    {
      get; set;
    }

    /// <summary>
    /// Name of conatct person.
    /// </summary>

    public string ContactPerson
    {
      get; set;
    }

    /// <summary>
    /// Vendor referenceid is coming from ERP portal, If invoice generated from ERP.
    /// </summary>

    public string VendorRefNo
    {
      get; set;
    }

    /// <summary>
    /// Local currency from where invoice generated.
    /// </summary>

    public string LocalCurrency
    {
      get; set;
    }

    /// <summary>
    /// Status of invoice.
    /// </summary>
    public int Status
    {
      get; set;
    }

    /// <summary>
    /// Currenct status test.
    /// </summary>

    public string StatusText
    {
      get; set;
    }

    /// <summary>
    /// Invoice posting date. 
    /// </summary>
    public DateTime PostingDate
    {
      get; set;
    }

    /// <summary>
    /// Document date.
    /// </summary>
    public DateTime DocumentDate
    {
      get; set;
    }

    /// <summary>
    /// Sales Employees.
    /// </summary>

    public string SalesEmployee
    {
      get; set;
    }

    /// <summary>
    /// Owner of invoice.
    /// </summary>

    public string Owner
    {
      get; set;
    }

    /// <summary>
    /// Invoice actual amount without discount.
    /// </summary>
    public decimal TotalBeforeDiscount
    {
      get; set;
    }

    /// <summary>
    /// Invoice actual amount without discount for Vendor.
    /// </summary>
    public decimal TotalBeforeDiscountFC
    {
      get; set;
    }

    /// <summary>
    /// Discount apply on Invoice.
    /// </summary>
    public decimal Discount
    {
      get; set;
    }

    /// <summary>
    /// Discount apply on Invoice For Vendor.
    /// </summary>
    public decimal DiscountFC
    {
      get; set;
    }

    /// <summary>
    /// Freight charges apply on Invoice.
    /// </summary>
    public decimal Freight
    {
      get; set;
    }


    /// <summary>
    /// Freight charges apply on Invoice For Vendor.
    /// </summary>
    public decimal FreightFC
    {
      get; set;
    }

    /// <summary>
    /// Tax charges apply on Invoice.
    /// </summary>
    public decimal Tax
    {
      get; set;
    }


    /// <summary>
    /// Tax charges apply on Invoice For Vendor .
    /// </summary>
    public decimal TaxFC
    {
      get; set;
    }

    /// <summary>
    /// Due amount on Invoice.
    /// </summary>
    public decimal TotalPaymentDue
    {
      get; set;
    }

    /// <summary>
    /// Due amount on Invoice For Vendor
    /// </summary>
    public decimal TotalPaymentDueFC
    {
      get; set;
    }

    /// <summary>
    /// Appled amount.
    /// </summary>
    public decimal AppliedAmount
    {
      get; set;
    }


    /// <summary>
    /// Appled amount For Vendor .
    /// </summary>
    public decimal AppliedAmountFC
    {
      get; set;
    }

    /// <summary>
    /// Balanace due on invoice.
    /// </summary>
    public decimal BalanceDue
    {
      get; set;
    }


    /// <summary>
    /// Balanace due on invoice For Vendor.
    /// </summary>
    public decimal BalanceDueFC
    {
      get; set;
    }

    /// <summary>
    /// Note for invoice.
    /// </summary>
    public string Remarks
    {
      get; set;
    }

    /// <summary>
    /// Ship to address.
    /// </summary>
    public string ShipFromAddress
    {
      get; set;
    }

    /// <summary>
    /// Ship from address key, coming from ERP connector.
    /// </summary>        
    public string ShipFromAddressKey
    {
      get; set;
    }

    /// <summary>
    /// Ship to address.
    /// </summary>
    public string ShipToAddress
    {
      get; set;
    }

    /// <summary>
    /// Ship to address key, coming from ERP connector.
    /// </summary>        
    public string ERPShipToAddressKey
    {
      get; set;
    }

    /// <summary>
    /// Bill to address key.
    /// </summary>        
    public string ERPBillToAddressKey
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
    /// Shipping Type.
    /// </summary>
    public int ShippingType
    {
      get; set;
    }

    /// <summary>
    /// Shipping type text.
    /// </summary>        
    public string ShippingTypeText
    {
      get; set;
    }

    /// <summary>
    /// Invoice tracking number.
    /// </summary>

    public string TrackingNo
    {
      get; set;
    }

    /// <summary>
    /// Invoice tracking number.
    /// </summary>

    public string OpType
    {
      get; set;
    }
    /// <summary>
    /// Due date.
    /// </summary>
    public DateTime? DueDate
    {
      get; set;
    }
    /// <summary>
    /// invoice item list.
    /// </summary>
    public List<BAAPInvoiceItemSyncDTO> InvoiceItemList
    {
      get; set;
    }

    /// <summary>
    /// sales order AttachmentList list.
    /// </summary>
    public List<BAAPInvoiceAttachmentSyncDTO> Attachments
    {
      get; set;
    }


    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns>BAAPInvoice</returns>
    public static BAAPInvoice MapToEntity(BAAPInvoiceSyncDTO model)
    {

      BAAPInvoice inv = new BAAPInvoice();
      inv.ERPConnectorKey = model.ERPConnectorKey;
      inv.ERPAPInvoiceKey = model.ERPAPInvoiceKey;
      inv.ERPDocNum = model.ERPDocNum;
      inv.ContactPerson = model.ContactPerson;
      inv.ERPVendorKey = model.ERPVendorKey;
      inv.DueDate = model.DueDate;
      inv.VendorId = model.VendorId;
      inv.VendorName = model.VendorName;
      inv.VendorRefNo = model.VendorRefNo;
      inv.DocumentDate = model.DocumentDate;
      inv.Owner = model.Owner;
      inv.PostingDate = model.PostingDate;
      inv.Remarks = model.Remarks;
      inv.SalesEmployee = model.SalesEmployee;
      inv.ShippingType = model.ShippingType;
      inv.ShippingTypeText = model.ShippingTypeText;
      inv.Status = model.Status;
      inv.StatusText = model.StatusText;
      inv.TotalBeforeDiscount = model.TotalBeforeDiscount;
      inv.TotalBeforeDiscountFC = model.TotalBeforeDiscountFC;
      inv.Discount = model.Discount;
      inv.DiscountFC = model.DiscountFC;
      inv.Tax = model.Tax;
      inv.TaxFC = model.TaxFC;
      inv.TotalPaymentDue = model.TotalPaymentDue;
      inv.TotalPaymentDueFC = model.TotalPaymentDueFC;
      inv.Freight = model.Freight;
      inv.FreightFC = model.FreightFC;
      inv.AppliedAmount = model.AppliedAmount;
      inv.AppliedAmountFC = model.AppliedAmountFC;
      inv.BalanceDue = model.BalanceDue;
      inv.BalanceDueFC = model.BalanceDueFC;
      inv.LocalCurrency = model.LocalCurrency;
      inv.TrackingNo = model.TrackingNo;

      inv.ERPShipToAddressKey = model.ERPShipToAddressKey;
      inv.ShipToAddress = model.ShipToAddress;
      inv.ERPBillToAddressKey = model.ERPBillToAddressKey;
      inv.BillToAddress = model.BillToAddress;
      inv.ShipFromAddressKey = model.ShipFromAddressKey;
      inv.ShipFromAddress = model.ShipFromAddress;

      return inv;
    }    /// <summary>
         /// Map model object to entity object and return it.
         /// </summary>
         /// <returns>BAAPInvoice</returns>
    public static BAAPInvoice MapToEntity(BAAPInvoiceSyncDTO model, BAAPInvoice inv)
    {

      //BAAPInvoice inv = new BAAPInvoice();
      //inv.ERPConnectorKey = model.ERPConnectorKey;
      //inv.ERPAPInvoiceKey = model.ERPAPInvoiceKey;
      //inv.ERPDocNum = model.ERPDocNum;
      inv.ContactPerson = model.ContactPerson;
      //inv.ERPVendorKey = model.ERPVendorKey;
      inv.DueDate = model.DueDate;
      inv.DocumentDate = model.DocumentDate;
      inv.Owner = model.Owner;
      inv.PostingDate = model.PostingDate;
      inv.Remarks = model.Remarks;
      inv.SalesEmployee = model.SalesEmployee;
      inv.ShippingType = model.ShippingType;
      inv.ShippingTypeText = model.ShippingTypeText;
      inv.Status = model.Status;
      inv.StatusText = model.StatusText;
      inv.TotalBeforeDiscount = model.TotalBeforeDiscount;
      inv.TotalBeforeDiscountFC = model.TotalBeforeDiscountFC;
      inv.Discount = model.Discount;
      inv.DiscountFC = model.DiscountFC;
      inv.Tax = model.Tax;
      inv.TaxFC = model.TaxFC;
      inv.TotalPaymentDue = model.TotalPaymentDue;
      inv.TotalPaymentDueFC = model.TotalPaymentDueFC;
      inv.Freight = model.Freight;
      inv.FreightFC = model.FreightFC;
      inv.AppliedAmount = model.AppliedAmount;
      inv.AppliedAmountFC = model.AppliedAmountFC;
      inv.BalanceDue = model.BalanceDue;
      inv.BalanceDueFC = model.BalanceDueFC;
      inv.LocalCurrency = model.LocalCurrency;
      inv.TrackingNo = model.TrackingNo;

      inv.ERPShipToAddressKey = model.ERPShipToAddressKey;
      inv.ShipToAddress = model.ShipToAddress;
      inv.ERPBillToAddressKey = model.ERPBillToAddressKey;
      inv.BillToAddress = model.BillToAddress;
      inv.ShipFromAddressKey = model.ShipFromAddressKey;
      inv.ShipFromAddress = model.ShipFromAddress;

      return inv;
    }

  }
}