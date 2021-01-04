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
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BASalesOrderSyncDTO:BABaseSyncDTO {
        
        public string ERPSalesOrderKey {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }
        
        public string ERPCustomerKey {
            get; set;
        }
        
        public string CustomerName {
            get; set;
        }
        public Guid CustomerID {
            get; set;
        }

        public string ContactPerson {
            get; set;
        }

        
        public string CustomerRefNo {
            get; set;
        }

        
        public string LocalCurrency {
            get; set;
        }

        public int Status {
            get; set;
        }

        
        public string StatusText {
            get; set;
        }

        public DateTime PostingDate {
            get; set;
        }

        public DateTime DeliveryDate {
            get; set;
        }

        public DateTime DocumentDate {
            get; set;
        }

        public string PickAndPackRemarks {
            get; set;
        }
        
        public string SalesEmployee {
            get; set;
        }

        
        public string Owner {
            get; set;
        }

        public decimal TotalBeforeDiscount {
            get; set;
        }

        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        public decimal Discount {
            get; set;
        }

        public decimal DiscountFC {
            get; set;
        }

        public decimal Freight {
            get; set;
        }

        public decimal FreightFC {
            get; set;
        }

        public decimal Tax {
            get; set;
        }

        public decimal TaxFC {
            get; set;
        }

        public decimal TotalPaymentDue {
            get; set;
        }

        public decimal TotalPaymentDueFC {
            get; set;
        }

        public string Remarks {
            get; set;
        }       

        public string ShipFromAddress {
            get; set;
        }
        
        public string ShipFromAddressKey {
            get; set;
        }
        
        public string ERPShipToAddressKey {
            get; set;
        }

        public string ShipToAddress {
            get; set;
        }
        
        public string ERPBillToAddressKey {
            get; set;
        }

        public string BillToAddress {
            get; set;
        }

        public int ShippingType {
            get; set;
        }
        
        public string ShippingTypeText {
            get; set;
        }

        public string OpType {
            get; set;
        }

        /// <summary>
        /// sales order item list.
        /// </summary>
        public List<BASalesOrderItemSyncDTO> ItemList {
            get;set;
        }
        /// <summary>
        /// sales order AttachmentList list.
        /// </summary>
        public List<BASalesOrderAttachmentSyncDTO> Attachments {
            get; set;
        }
        

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BASalesOrder MapToEntity(BASalesOrderSyncDTO model) {

            BASalesOrder entity = new BASalesOrder();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPSalesOrderKey = model.ERPSalesOrderKey;
            entity.ERPDocNum = model.ERPDocNum;
            entity.ERPCustomerKey = model.ERPCustomerKey;
            entity.CustomerID = model.CustomerID;
            entity.CustomerName = model.CustomerName;
            entity.CustomerRefNo = model.CustomerRefNo;
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
    public static BASalesOrder MapToEntity(BASalesOrderSyncDTO model, BASalesOrder entity)
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
    public static BASalesOrderSyncDTO MapFromCustBASalesORderAddDTO(CustBASalesOrderAddDTO model) {
          BASalesOrderSyncDTO dto = new BASalesOrderSyncDTO();

          dto.ERPConnectorKey = model.ERPConnectorKey;
          dto.ERPSalesOrderKey = model.ERPSalesOrderKey;
          dto.ERPDocNum =   "0" ;//model.ERPDocNum;
          dto.ERPCustomerKey = model.ERPCustomerKey;
          dto.CustomerID = model.CustomerId;
          dto.CustomerName = model.CustomerName;
          dto.CustomerRefNo = model.CustomerRefNo;
          dto.ContactPerson = model.ContactPerson;
          dto.DeliveryDate = model.DeliveryDate;
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
          dto.PickAndPackRemarks = model.PickAndPackRemarks;
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

          BASalesOrderItemSyncDTO syncItemDTO = new BASalesOrderItemSyncDTO();
          List<BASalesOrderItemSyncDTO> syncItemDTOList = new List<BASalesOrderItemSyncDTO>();

          foreach (CustBASalesOrderItemDTO itemDTO in model.SalesOrderItemList) {
              syncItemDTO = BASalesOrderItemSyncDTO.MapToSyncItemDTO(itemDTO);
              syncItemDTOList.Add(syncItemDTO);
          }

          dto.ItemList = syncItemDTOList;

          return dto;
        }

  }
}
