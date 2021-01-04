using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// Contains invoice name and Invoice number.
    /// </summary>
    public class InvoiceInfoDTO {
        /// <summary>
        /// ERPARInvoiceKey.
        /// </summary>
        public string ERPARInvoiceKey {
            get; set;
        }

        public string ERPDocNum {
            get;set;
        }

        /// <summary>
        /// Tracking number.
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// Invoice DueDate.
        /// </summary>
        public DateTime? DueDate {
            get; set;
        }
        /// <summary>
        /// Invoice Paid Amount.
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Invoice Paid Amount.
        /// </summary>
        public decimal AmountPaidFC {
            get; set;
        }

        /// <summary>
        /// Conatins the partial amount paid by invoice.
        /// </summary>
        public decimal ReceiptPaidAmount {
            get; set;
        }

        /// <summary>
        /// Conatins the partial amount paid by invoice.
        /// </summary>
        public decimal ReceiptPaidAmountFC {
            get; set;
        }

        /// <summary>
        /// Invoice TotalAmount.
        /// </summary>
        public decimal BalanceDue {
            get; set;
        }

        public decimal BalanceDueFC {
            get; set;
        }

        public decimal TotalPaymentDue {
            get; set;
        }

        public decimal TotalPaymentDueFC {
            get; set;
        }

        public decimal TotalBeforeDiscount {
            get; set;
        }

        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        [NotMapped]
        public decimal FinalConversionRate {
            get;set;
        }

    }
}
