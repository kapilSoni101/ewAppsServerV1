/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Amit Mundra
 * Last Updated On: 26 December 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// invoice item data.
    /// </summary>
    public class BusBAARInvoiceItemDTO {

        public string ERPARInvoiceItemKey {
            get; set;
        }

        public string ERPARInvoiceKey {
            get; set;
        }

        public Guid ARInvoiceID {
            get; set;
        }
        public Guid ItemId {
            get; set;
        }
        public string ERPItemKey {
            get; set;
        }

        public string SerialOrBatchNo {
            get; set;
        }

        public string ItemName {
            get; set;
        }

        public string ItemType {
            get; set;
        }

        public decimal Quantity {
            get; set;
        }

        public string QuantityUnit {
            get; set;
        }

        public decimal UnitPrice {
            get; set;
        }

        public string Unit {
            get; set;
        }

        public decimal DiscountPercent {
            get; set;
        }

        public decimal DiscountAmount {
            get; set;
        }

        public string TaxCode {
            get; set;
        }

        public decimal TaxPercent {
            get; set;
        }

        public decimal TotalLC {
            get; set;
        }

        public string ERPShipToAddressKey {
            get; set;
        }

        public string ShipFromAddress {
            get; set;
        }

        public string ShipFromAddressKey {
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

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns>BAARInvoiceItem</returns>
        public static BAARInvoiceItem MapToEntity(BAARInvoiceItemSyncDTO model) {
            BAARInvoiceItem entity = new BAARInvoiceItem() {
                ERPARInvoiceItemKey = model.ERPARInvoiceItemKey,
                ERPARInvoiceKey = model.ERPARInvoiceKey,
                ERPItemKey = model.ERPItemKey,
                ARInvoiceID = model.ARInvoiceID,
                DiscountAmount = model.DiscountAmount,
                DiscountPercent = model.DiscountPercent,
                ItemName = model.ItemName,
                ItemType = model.ItemType,
                Quantity = model.Quantity,
                QuantityUnit = model.QuantityUnit,
                SerialOrBatchNo = model.SerialOrBatchNo,
                TaxCode = model.TaxCode,
                TaxPercent = model.TaxPercent,
                TotalLC = model.TotalLC,
                Unit = model.Unit,
                UnitPrice = model.UnitPrice,
                ERPShipToAddressKey = model.ERPShipToAddressKey,
                ShipToAddress = model.ShipToAddress,
                ERPBillToAddressKey = model.ERPBillToAddressKey,
                BillToAddress = model.BillToAddress,
                ShipFromAddressKey = model.ShipFromAddressKey,
                ShipFromAddress = model.ShipFromAddress
            };
            return entity;
        }
    }
}
