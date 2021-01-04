using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains invoice detail.
    /// </summary>
    public class BAARInvoiceDetailDTO:BaseDTO {

        public new Guid ID {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

        public string CustomerRefNo {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        #region Amount

        /// <summary>
        /// Total  amount of invoice
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Total amount(Foreign currency) of invoice.
        /// </summary>
        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Amount paid for invoice.
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Amount paid for invoice.
        /// </summary>
        public decimal AmountPaidFC {
            get; set;
        }

        /// <summary>
        /// Discount.
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Discount in foreign currency.
        /// </summary>
        public decimal DiscountFC {
            get; set;
        }

        /// <summary>
        /// Tax 
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// tax in foreign currency.
        /// </summary>
        public decimal TaxFC {
            get; set;
        }

        /// <summary>
        /// Freight
        /// </summary>
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Freight in forrign currency.
        /// </summary>
        public decimal FreightFC {
            get; set;
        }

        /// <summary>
        /// LocalCurrency
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        #endregion Amount

        public int Status {
            get; set;
        }

        public string StatusText {
            get; set;
        }

        public new DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// List of invoice items.
        /// </summary>
        public List<BAARInvoiceItemDTO> invoiceItems {
            get; set;
        }

        //[NotMapped]
        //public Address Address {
        //    get; set;
        //}        

        public string ContactName {
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
