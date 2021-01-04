using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class CreditCardRefundDTO {

        public string transactionAmount {
            get; set;
        }

        public string paymentId {
            get; set;
        }

        public string invoiceId {
            get; set;
        }

        public string transactionID {
            get;set;
        }

        public string currencyCode {
            get;set;
        }

    }
}
