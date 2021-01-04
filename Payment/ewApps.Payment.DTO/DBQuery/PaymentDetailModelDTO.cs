using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// Contains all payment information as well as billing address information.
    /// </summary>    
    public class PaymentDetailModelDTO {

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
        public decimal TotalPaymentDue {
            get; set;
        }

        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Amount Paid by this Payment
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Amount Paid by this Payment
        /// </summary>
        public decimal AmountPaidFC {
            get; set;
        }

        public string AuthCode {
            get; set;
        }

        public string TransactionId {
            get; set;
        }

        /// <summary>
        /// Currency set for business.
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Currency set for customer.
        /// </summary>
        public string CustomerCurrency {
            get; set;
        }
        
        public string ShipToAddress {
            get; set;
        }

        public string ShipFromAddress {
            get; set;
        }

        public string BillToAddress {
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

        public string BankAccountName {
            get; set;
        }

        public string CustomerRefId {
            get; set;
        }

        public string CustomerAccountNumber {
            get; set;
        }

        public string PayeeName {
            get; set;
        }

        public string PayeeId {
            get; set;
        }

        /// <summary>
        /// Its calculate amount with exchange rate.
        /// </summary>
        [NotMapped]
        public decimal CalculatetTotalAmount {
            get; set;
        }

        /// <summary>
        /// Its calculate amount with exchange rate.
        /// </summary>
        [NotMapped]
        public decimal CalculateAmount {
            get; set;
        }

        public string PayServiceName {
            get; set;
        }

        public string PayServiceAttributeName {
            get; set;
        }

        ///// <summary>
        ///// Business address.
        ///// </summary>
        //[NotMapped]
        //public AddressDTO businessAddress {
        //    get; set;
        //}

    }
}
