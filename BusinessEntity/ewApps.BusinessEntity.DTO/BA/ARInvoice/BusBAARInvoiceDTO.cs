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

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// Invoice table storing all the invoice data.
    /// </summary>
    public class BusBAARInvoiceDTO {

        /// <summary>
        /// Unique key of invoice (Invoice generated from ERP).
        /// </summary>
        public string ERPARInvoiceKey {
            get; set;
        }

        /// <summary>
        /// Connector key of invoice (Invoice generated from ERP connector name).
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Unique key of customer (customer generated from ERP portal).
        /// </summary>

        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Customer table unique id.
        /// </summary>
        
        public Guid CustomerId {
            get; set;
        }
        /// <summary>
        /// Name of customer.
        /// </summary>

        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Name of conatct person.
        /// </summary>

        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Customer referenceid is coming from ERP portal, If invoice generated from ERP.
        /// </summary>

        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Local currency from where invoice generated.
        /// </summary>

        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Status of invoice.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Currenct status test.
        /// </summary>

        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Invoice posting date. 
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// Document date.
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
        /// Owner of invoice.
        /// </summary>

        public string Owner {
            get; set;
        }

        /// <summary>
        /// Invoice actual amount without discount.
        /// </summary>
        public decimal TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Discount apply on Invoice.
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Freight charges apply on Invoice.
        /// </summary>
        public decimal Freight {
            get; set;
        }
        /// <summary>
        /// Tax charges apply on Invoice.
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Due amount on Invoice.
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Appled amount.
        /// </summary>
        public decimal AppliedAmount {
            get; set;
        }

        /// <summary>
        /// Balanace due on invoice.
        /// </summary>
        public decimal BalanceDue {
            get; set;
        }

        /// <summary>
        /// Note for invoice.
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Ship from address key, coming from ERP connector.
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
        /// Ship to address key, coming from ERP connector.
        /// </summary>        
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address key.
        /// </summary>        
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Shipping Type.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Shipping type text.
        /// </summary>        
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// Invoice tracking number.
        /// </summary>

        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// invoice item list.
        /// </summary>
        public List<BAARInvoiceItemSyncDTO> InvoiceItemList {
            get; set;
        }

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns>BAARInvoice</returns>
        public static BAARInvoice MapToEntity(BAARInvoiceSyncDTO model) {

            BAARInvoice inv = new BAARInvoice();
            inv.ERPConnectorKey = model.ERPConnectorKey;
            inv.ERPARInvoiceKey = model.ERPARInvoiceKey;
            inv.ContactPerson = model.ContactPerson;
            inv.ERPCustomerKey = model.ERPCustomerKey;
            inv.CustomerId = model.CustomerId;
            inv.CustomerName = model.CustomerName;
            inv.CustomerRefNo = model.CustomerRefNo;
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
            inv.Discount = model.Discount;
            inv.Tax = model.Tax;
            inv.TotalPaymentDue = model.TotalPaymentDue;
            inv.Freight = model.Freight;
            inv.AppliedAmount = model.AppliedAmount;
            inv.BalanceDue = model.BalanceDue;
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