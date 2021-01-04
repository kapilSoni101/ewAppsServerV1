using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
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

    }
}
