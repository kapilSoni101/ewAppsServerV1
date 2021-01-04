using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Payment.DTO {

    public class PaymentDQ {

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Ewapps Businees Entity ID
        /// </summary>
        public Guid BusinessId {
            get; set;
        }

        /// <summary>
        /// EwApps Partner Entity Id
        /// </summary>
        public Guid PartnerId {
            get; set;
        }

        /// <summary>
        /// Invoice Id
        /// </summary>
        public Guid InvoiceId {
            get; set;
        }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal TotalAmount {
            get; set;
        }

        /// <summary>
        /// Amount Paid by this Payment
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Type of the Payment mode
        /// </summary>
        // 'credit' or 'debit'
        public string PaymentType {
            get; set;
        }

        [NotMapped]
        // blank right now
        public string PaymentEntryType {
            get; set;
        }        

        public string Note {
            get; set;
        }

        public DateTime OriginationDate {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public string Status {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string CustomerRefNo {
            get; set;
        }

        public string CustomerAccountNumber {
            get; set;
        }

        [NotMapped]
        public string VCTransactionId {
            get; set;
        }

        [NotMapped]
        public string AuthCode {
            get; set;
        }

        [NotMapped]
        public string CardType {
            get; set;
        }
    }
}
