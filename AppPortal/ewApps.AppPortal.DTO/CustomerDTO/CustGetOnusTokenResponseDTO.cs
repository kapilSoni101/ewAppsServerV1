using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class CustGetOnusTokenResponseDTO {

        public string Status {
            get; set;
        }

        public string ResponseCode {
            get; set;
        }

        public string ResponseMessage {
            get; set;
        }

        public string Token {
            get; set;
        }

        public string ExpirationDate {
            get; set;
        }

        public string CardType {
            get; set;
        }

        public string MaskedCardNumber {
            get; set;
        }

        public string CardTransactionIdentifier {
            get; set;
        }

        public string TaskID {
            get; set;
        }
        public string TransactionID {
            get; set;
        }

        public DateTime TransactionTimestamp {
            get; set;
        }


    }
}
