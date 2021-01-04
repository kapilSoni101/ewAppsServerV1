/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 07 July 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BASalesQuotationSyncDTO {

        /// <summary>
        /// ERPSalesQuotationKey will be not null if comingfrom ERP connector.
        /// It unique id of SalesQuotation table of ERP connector table.
        /// </summary>        
        public string ERPSalesQuotationKey {
            get; set;
        }



        /// <summary>
        /// ERP connector key.        
        /// </summary>        
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// ERPCustomerKey will be not null if coming from ERP connector.
        /// It unique id of Customer table of ERP connector table.
        /// </summary>
        
        public string ERPCustomerKey {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        /// <summary>
        /// Customer name.
        /// </summary>

        public string CustomerName {
            get; set;
        }

        
        /// <summary>
        /// Name of Contact person.
        /// </summary>
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Reference id of customer.
        /// </summary>
        
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Local currency.
        /// </summary>
        
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Current status of SalesQuotation.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Status display text.
        /// </summary>
        
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Posting date.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// SalesQuote Valid untill.
        /// </summary>
        public DateTime ValidUntil {
            get; set;
        }

        /// <summary>
        /// DocumentDate
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Sales Employees.
        /// </summary>
        
        public string SalesEmployee {
            get; set;
        }

        /// <summary>
        /// Owner
        /// </summary>
        
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Total amount before discount.
        /// </summary>
        public decimal TotalBeforeDiscount {
            get; set;
        }

        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        /// <summary>
        /// Discount on  quote.
        /// </summary>
        public decimal Discount {
            get; set;
        }

        public decimal DiscountFC {
            get; set;
        }

        /// <summary>
        /// Frright charges.
        /// </summary>
        public decimal Freight {
            get; set;
        }

        public decimal FreightFC {
            get; set;
        }

        /// <summary>
        /// Tax on quote.
        /// </summary>
        public decimal Tax {
            get; set;
        }

        public decimal TaxFC {
            get; set;
        }

        /// <summary>
        /// Total payment due.
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Comment on notes.
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// ERPShipToAddressKey will be not null if coming from ERP connector.
        /// It unique id of address table of ERP connector table.
        /// </summary>        
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// ShipFromAddress 
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// ShipFromAddressKey will be not null if coming from ERP connector.
        /// </summary>        
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// ERPBillToAddressKey will be not null if coming from ERP connector.
        /// It unique id of address.
        /// </summary>
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// TYpe of shipping.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// shipping text.
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }
        /// <summary>
        /// shipping text.
        /// </summary>
        public string OpType {
            get; set;
        }
        /// <summary>
        /// sales order item list.
        /// </summary>
        public List<BASalesQuotationItemSyncDTO> ItemList {
            get; set;
        }

        /// <summary>
        /// sales order AttachmentList list.
        /// </summary>
        public List<BASalesQuotationAttachmentSyncDTO> Attachments {
            get; set;
        }

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BASalesQuotation MapToEntity(BASalesQuotationSyncDTO model) {
            BASalesQuotation entity = new BASalesQuotation();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPSalesQuotationKey = model.ERPSalesQuotationKey;
            entity.ERPDocNum = model.ERPDocNum;
            entity.ERPCustomerKey = model.ERPCustomerKey;
            entity.CustomerId = model.CustomerId;
            entity.ValidUntil = model.ValidUntil;
            entity.CustomerName = model.CustomerName;
            entity.CustomerRefNo = model.CustomerRefNo;
            entity.ContactPerson = model.ContactPerson;
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
            entity.PostingDate = model.PostingDate;
            entity.Remarks = model.Remarks;
            entity.SalesEmployee = model.SalesEmployee;
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
    public static BASalesQuotation MapToEntity(BASalesQuotationSyncDTO model, BASalesQuotation entity)
    {
     
      entity.ValidUntil = model.ValidUntil;
      entity.CustomerName = model.CustomerName;
      entity.CustomerRefNo = model.CustomerRefNo;
      entity.ContactPerson = model.ContactPerson;
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
      entity.PostingDate = model.PostingDate;
      entity.Remarks = model.Remarks;
      entity.SalesEmployee = model.SalesEmployee;
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
    public static BASalesQuotationSyncDTO MapFromCustBASalesQuotationAddDTO(CustBASalesQuotationAddDTO model)
    {
      BASalesQuotationSyncDTO dto = new BASalesQuotationSyncDTO();

      dto.ERPConnectorKey = model.ERPConnectorKey;
      dto.ERPSalesQuotationKey = model.ERPSalesQuotationKey;
      dto.ERPDocNum = "0";//model.ERPDocNum;
      dto.ERPCustomerKey = model.ERPCustomerKey;
      dto.CustomerId = model.CustomerId;
      dto.CustomerName = model.CustomerName;
      dto.CustomerRefNo = model.CustomerRefNo;
      dto.ContactPerson = model.ContactPerson;
     // dto.DeliveryDate = model.DeliveryDate;
      dto.Discount = model.Discount;
      dto.DiscountFC = model.Discount;
      dto.DocumentDate = model.DocumentDate;
      dto.Freight = model.Freight;
      dto.FreightFC = model.Freight;
      dto.Tax = model.Tax;
      dto.TaxFC = model.Tax;
      dto.TotalBeforeDiscount = model.TotalBeforeDiscount;
      dto.TotalBeforeDiscountFC = model.TotalBeforeDiscount;
      dto.TotalPaymentDue = model.TotalPaymentDue;
      dto.TotalPaymentDueFC = model.TotalPaymentDue;
      dto.LocalCurrency = model.LocalCurrency;
      dto.Owner = model.Owner;
     // dto.PickAndPackRemarks = model.PickAndPackRemarks;
      dto.PostingDate = model.PostingDate;
      dto.Remarks = model.Remarks;
      dto.SalesEmployee = model.SalesEmployee;
      dto.ShippingType = model.ShippingType;
      dto.ShippingTypeText = model.ShippingTypeText;
      dto.Status = model.Status;
      dto.StatusText = model.StatusText;
      dto.ERPShipToAddressKey = model.ERPShipToAddressKey;
      dto.ShipToAddress = model.ShipToAddress;
      dto.ERPBillToAddressKey = model.ERPBillToAddressKey;
      dto.BillToAddress = model.BillToAddress;
      dto.ShipFromAddressKey = model.ShipFromAddressKey;
      dto.ShipFromAddress = model.ShipFromAddress;

      BASalesQuotationItemSyncDTO syncItemDTO = new BASalesQuotationItemSyncDTO();
      List<BASalesQuotationItemSyncDTO> syncItemDTOList = new List<BASalesQuotationItemSyncDTO>();

      foreach (CustBASalesQuotationItemDTO itemDTO in model.SalesQuotationItemList)
      {
        syncItemDTO = BASalesQuotationItemSyncDTO.MapToSyncItemDTO(itemDTO);
        syncItemDTOList.Add(syncItemDTO);
      }

      dto.ItemList = syncItemDTOList;

      return dto;
    }
  }
}
