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

namespace ewApps.AppPortal.DTO {

    public class CustBASalesQuotationDTO {

        /// <summary>        
        /// It unique id of SalesQuotation table of ERP connector table.
        /// </summary>        
        public Guid ID {
            get; set;
        }


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

        /// <summary>
        /// Discount on  quote.
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Frright charges.
        /// </summary>
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Tax on quote.
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Total payment due.
        /// </summary>
        public decimal TotalPaymentDue {
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

        public string ERPDocNum {
            get; set;
        }

        /// <summary>
        /// sales order item list.
        /// </summary>
        [NotMapped]
        public List<BASalesQuotationItemSyncDTO> ItemList {
            get; set;
        }

        public Guid CustomerId
        {
          get; set;
        }

    ///// <summary>
    ///// Map model object to entity object and return it.
    ///// </summary>
    ///// <returns></returns>
    //public static BASalesQuotation MapToEntity(BASalesQuotationSyncDTO model) {
    //    BASalesQuotation entity = new BASalesQuotation();
    //    entity.ERPConnectorKey = model.ERPConnectorKey;
    //    entity.ERPSalesQuotationKey = model.ERPSalesQuotationKey;
    //    entity.ERPCustomerKey = model.ERPCustomerKey;
    //    entity.ValidUntil = model.ValidUntil;
    //    entity.CustomerName = model.CustomerName;
    //    entity.CustomerRefNo = model.CustomerRefNo;
    //    entity.ContactPerson = model.ContactPerson;
    //    entity.Discount = model.Discount;
    //    entity.DocumentDate = model.DocumentDate;
    //    entity.Freight = model.Freight;
    //    entity.Tax = model.Tax;
    //    entity.TotalBeforeDiscount = model.TotalBeforeDiscount;
    //    entity.TotalPaymentDue = model.TotalPaymentDue;
    //    entity.LocalCurrency = model.LocalCurrency;
    //    entity.Owner = model.Owner;
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
