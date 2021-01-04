using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// Invoice  details Data trasnfer object.
    /// </summary>
    public class AddBAARInvoiceItemDTO {

        public Guid ID {
            get; set;
        }

        public string LotNo {
            get; set;
        }

        public Guid ARInvoiceID {
            get; set;
        }

        public string SerialOrBatchNo {
            get; set;
        }

        public Guid ItemId {
            get; set;
        }

        public string ItemName {
            get; set;
        }

        public string ERPARInvoiceItemKey {
            get; set;
        }

        public string ERPItemKey {
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

        public decimal UnitPriceFC {
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

        public decimal DiscountAmountFC {
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
        public decimal TotalLCFC {
            get; set;
        }

       
        #region Address

        /// <summary>
        /// Ship from address
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        #endregion Address

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns>BAARInvoiceItem</returns>
        public static BAARInvoiceItem MapToEntity(AddBAARInvoiceItemDTO model, string erpARInvoiceKey) {
            BAARInvoiceItem entity = new BAARInvoiceItem() {
                ERPARInvoiceItemKey = model.ERPARInvoiceItemKey,
                ERPARInvoiceKey = erpARInvoiceKey,
                ERPItemKey = model.ERPItemKey,
                ARInvoiceID = model.ARInvoiceID,
                DiscountAmount = model.DiscountAmount,
                DiscountAmountFC = model.DiscountAmountFC,
                DiscountPercent = model.DiscountPercent,
                ItemId = model.ItemId,
                ItemName = model.ItemName,
                ItemType = model.ItemType,
                Quantity = model.Quantity,
                QuantityUnit = model.QuantityUnit,
                SerialOrBatchNo = model.SerialOrBatchNo,
                TaxCode = model.TaxCode,
                TaxPercent = model.TaxPercent,
                TotalLC = model.TotalLC,
                TotalLCFC = model.TotalLCFC,
                Unit = model.Unit,
                LotNo = model.LotNo,
                UnitPrice = model.UnitPrice,
                UnitPriceFC = model.UnitPriceFC,
                //ERPShipToAddressKey = model.ERPShipToAddressKey,
                ShipToAddress = model.ShipToAddress,
                //ERPBillToAddressKey = model.ERPBillToAddressKey,
                BillToAddress = model.BillToAddress,
                //ShipFromAddressKey = model.ShipFromAddressKey,
                ShipFromAddress = model.ShipFromAddress
            };
            return entity;
        }

    }
}
