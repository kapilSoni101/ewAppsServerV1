using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Invoice list details Data trasnfer object.
    /// </summary>
    public class BAAPInvoiceDTO:BaseDTO {
        /// <summary>
        /// Unique id of invoice.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Unique Vendor referenc ekey for ERP.
        /// </summary>
        public string ERPVendorKey {
            get; set;
        }

        /// <summary>
        /// Unique id of Vendor
        /// </summary>
        public Guid VendorId {
            get; set;
        }

        /// <summary>
        /// invoice key
        /// </summary>
        public string ERPAPInvoiceKey {
            get; set;
        }

        /// <summary>
        /// invoice Vendor name
        /// </summary>
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// TrackingNo number
        /// </summary>
        public string TrackingNo {
            get; set;
        }       

        /// <summary>
        /// Total  amount of invoice
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Invoice total payment due
        /// </summary>
        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Balance due to pay. It is changable as per amount pay.
        /// </summary>
        public decimal BalanceDue {
            get;set;
        }

        /// <summary>
        /// Balance in customer currency to pay. It is changable as per amount pay.
        /// </summary>
        public decimal BalanceDueFC {
            get; set;
        }

        /// <summary>
        /// Amount paid.
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Amount paid in customer currency.
        /// </summary>
        public decimal AmountPaidFC {
            get; set;
        }

        /// <summary>
        /// Business currency.
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// customer currency
        /// </summary>
        public string VendorCurrency {
            get; set;
        }

        /// <summary>
        /// Invoice status
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Invoice status
        /// </summary>
        public string StatusText {
            get; set;
        }

        ///// <summary>
        ///// There will be 3 type of payment status
        ///// 1 means: Due
        ///// 2 means: Partial
        ///// 3 means: Paid
        ///// </summary>
        //public int PaymentStatus {
        //    get; set;
        //}

        /// <summary>
        /// Invoice created date
        /// </summary>
        public new DateTime? CreatedOn {
            get; set;
        }        

        /// <summary>
        /// Updated date of invoice
        /// </summary>
        public new DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// invoice creation date.
        /// </summary>
        public DateTime PostingDate {
            get;set;
        }

        public DateTime DocumentDate {
            get;set;
        }

        public DateTime? DueDate {
            get; set;
        }

        /// <summary>
        /// Update Full Name
        /// </summary>
        public string UpdatedByFullName {
            get; set;

        }

        /// <summary>
        /// Notes on invoice.
        /// </summary>
        public string Remarks {
            get;set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public string ERPDocNum {
            get;set;
        }


        [NotMapped]
        public int TotalLineItems {
            get; set;
        }

    }

}
