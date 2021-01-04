using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class TSysSalesDetailDTO {

        public string authCode {
            get; set;
        }

        public decimal processedAmount {
            get; set;
        }

        public string status {
            get; set;
        }

        public string responseCode {
            get; set;
        }

        public string responseMessage {
            get; set;
        }

        public string transactionID {
            get; set;
        }

        public DateTime transactionTimestamp {
            get; set;
        }

        public string maskedCardNumber {
            get; set;
        }

        public string cardType {
            get; set;
        }

        public string cardTransactionIdentifier {
            get; set;
        }

        public string customerReceipt {
            get; set;
        }

    }
}
