using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// Model contains properties to make payment through credit card.
    /// </summary>
    public class CreditCardPaymentDTO {

        /// <summary>
        /// Device Id.
        /// </summary>
        public string deviceID {
            get; set;
        }

        /// <summary>
        /// transactionKey
        /// </summary>
        public string transactionKey {
            get; set;
        }

        /// <summary>
        /// cardDataSource
        /// </summary>
        public string cardDataSource {
            get; set;
        }

        public string currencyCode {
            get; set;
        }

        /// <summary>
        /// Amount to pay.
        /// </summary>
        public string transactionAmount {
            get; set;
        }

        /// <summary>
        /// Credit card number.
        /// </summary>
        public string cardNumber {
            get; set;
        }

        /// <summary>
        /// Expiration date with month and date. Format should be mm/yyyy
        /// </summary>
        public string expirationDate {
            get; set;
        }

        /// <summary>
        /// Token.
        /// </summary>
        public string tokenRequired {
            get; set;
        }

        public string paymentId {
            get;set;
        }

        public string invoiceId {
            get; set;
        }

        public string cvvNumber {
            get;set;
        }

        // If customer is paying then set a value.
        public string saleByCustomer {
            get; set;
        }

    }
}
