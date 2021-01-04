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

namespace ewApps.AppPortal.DTO {

    public class CustBASalesOrderDTO {
        
        public string ERPSalesOrderKey {
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

        public decimal Discount {
            get; set;
        }

        public decimal Freight {
            get; set;
        }

        public decimal Tax {
            get; set;
        }

        public decimal TotalPaymentDue {
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

        public Guid ID  {
          get; set;
        }

        public Guid CustomerId
        {
          get; set;
        }

        public string ERPDocNum {
            get; set;
        }


        /// <summary>
        /// sales order item list.
        /// </summary>
        [NotMapped]
        public List<BASalesOrderItemSyncDTO> SalesOrderItemList {
            get;set;
        }

        ///// <summary>
        ///// Map model object to entity object and return it.
        ///// </summary>
        ///// <returns></returns>
        //public static BASalesOrder MapToEntity(BASalesOrderSyncDTO model) {

        //    BASalesOrder entity = new BASalesOrder();
        //    entity.ERPConnectorKey = model.ERPConnectorKey;
        //    entity.ERPSalesOrderKey = model.ERPSalesOrderKey;
        //    entity.ERPCustomerKey = model.ERPCustomerKey;
        //    entity.CustomerName = model.CustomerName;
        //    entity.CustomerRefNo = model.CustomerRefNo;
        //    entity.ContactPerson = model.ContactPerson;
        //    entity.DeliveryDate = model.DeliveryDate;
        //    entity.Discount = model.Discount;
        //    entity.DocumentDate = model.DocumentDate;
        //    entity.Freight = model.Freight;
        //    entity.Tax = model.Tax;
        //    entity.TotalBeforeDiscount = model.TotalBeforeDiscount;
        //    entity.TotalPaymentDue = model.TotalPaymentDue;
        //    entity.LocalCurrency = model.LocalCurrency;
        //    entity.Owner = model.Owner;
        //    entity.PickAndPackRemarks = model.PickAndPackRemarks;
        //    entity.PostingDate = model.PostingDate;
        //    entity.Remarks = model.Remarks;
        //    entity.SalesEmployee = model.SalesEmployee;
        //    entity.ShippingType = model.ShippingType;
        //    entity.ShippingTypeText = model.ShippingTypeText;
        //    entity.Status = model.Status;
        //    entity.StatusText = model.StatusText;
        //    entity.ERPShipToAddressKey = model.ERPShipToAddressKey;
        //    entity.ShipToAddress = model.ShipToAddress;
        //    entity.ERPBillToAddressKey = model.ERPBillToAddressKey;
        //    entity.BillToAddress = model.BillToAddress;
        //    entity.ShipFromAddressKey = model.ShipFromAddressKey;
        //    entity.ShipFromAddress = model.ShipFromAddress;

        //    return entity;
        //}

    }
}
