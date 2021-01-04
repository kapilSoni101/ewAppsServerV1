using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class CaptureResponseDTO {
        public CaptureResponse CaptureResponse {
            get; set;
        }
    }

    public class CaptureResponse {
        public string status {
            get; set;
        }
        public string responseCode {
            get; set;
        }
        public string responseMessage {
            get; set;
        }
        public string authCode {
            get; set;
        }
        public string cardType {
            get; set;
        }
        public string taskID {
            get; set;
        }
        public string transactionID {
            get; set;
        }
        public DateTime transactionTimestamp {
            get; set;
        }
        public decimal transactionAmount {
            get; set;
        }
        public string customerReceipt {
            get; set;
        }
        public string merchantReceipt {
            get; set;
        }
    }
}
