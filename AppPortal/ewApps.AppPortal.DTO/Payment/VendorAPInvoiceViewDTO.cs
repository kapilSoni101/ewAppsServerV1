using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains invoice detail.
    /// </summary>
    public class VendorAPInvoiceViewDTO:BaseDQ {

        public new Guid ID {
            get; set;
        }

        public Guid VendorId {
            get; set;
        }

        public string VendorName {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public string SalesEmployee {
            get; set;
        }

        public string Owner {
            get; set;
        }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        #region Amount

        public decimal TotalBeforeDiscount {
            get; set;
        }

        public decimal TotalBeforeDiscountFC {
            get; set;
        }

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

        public decimal BalanceDue {
            get; set;
        }

        public decimal BalanceDueFC {
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

        public string CustomerCurrency {
            get; set;
        }

        public string Remarks {
            get; set;
        }

        #endregion Amount

        public string ERPVendorKey {
            get; set;
        }

        public string ERPAPInvoiceKey {
            get; set;
        }

        public int Status {
            get; set;
        }

        public string StatusText {
            get; set;
        }

        public new DateTime? CreatedOn {
            get; set;
        }

        public DateTime? DueDate {
            get; set;
        }

        public DateTime? DocumentDate {
            get; set;
        }

        public DateTime? PostingDate {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        /// <summary>
        /// List of invoice items.
        /// </summary>
        public List<BAARInvoiceItemViewDTO> invoiceItems {
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


        [NotMapped]
        public List<NotesViewDTO> NotesList {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }


    }
}
