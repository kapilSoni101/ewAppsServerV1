using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains payment detail.
    /// Contains all payment history with respact to invoice.
    /// </summary>
    public class VendorPaymentDetailDTO {

        public Guid Id {
            get; set;
        }

        public Guid PaymentId {
            get; set;
        }

        public decimal AmountPaid {
            get; set;
        }

        public decimal AmountPaidFC {
            get; set;
        }

        public string Status {
            get; set;
        }

        public DateTime? PaymentDate {
            get; set;
        }

        public string Note {
            get; set;
        }

        /// <summary>
        /// Unique payment identity number.
        /// </summary>
        public string PaymentIdentityNo {
            get; set;
        }

        /// <summary>
        /// Number of invoice paid for a payment.
        /// </summary>
        public int TotalInvoice {
            get; set;
        }

        public string VendorName {
            get; set;
        }

        public string VendorId {
            get; set;
        }

        /// <summary>
        /// Account number.
        /// </summary>
        public string CustomerAccountNumber {
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
        public string CustomerCurrency {
            get; set;
        }

        public string PayServiceName {
            get; set;
        }

        public string PayServiceAttributeName {
            get; set;
        }

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get; set;
        }

        /// <summary>
        /// Payment can be Advance, Invoice
        /// </summary>
        public int PaymentType {
            get; set;
        }

        public string PayeeName {
            get;set;
        }

    }
}
