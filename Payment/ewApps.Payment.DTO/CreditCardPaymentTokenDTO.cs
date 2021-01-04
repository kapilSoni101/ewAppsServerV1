using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    public class CreditCardPaymentTokenDTO {

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
        /// Expiration date with month and date. Format should be mm/yyyy
        /// </summary>
        public string expirationDate {
            get; set;
        }

        /// <summary>
        /// Token.
        /// </summary>
        public string Token {
            get; set;
        }

        public string paymentId {
            get; set;
        }

        public string invoiceId {
            get; set;
        }

        public string cvvNumber {
            get; set;
        }

        // If customer is paying then set a value.
        public string saleByCustomer {
            get; set;
        }

    }
}
