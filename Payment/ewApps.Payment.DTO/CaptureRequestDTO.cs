using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// To capture the pre auth payment.
    /// </summary>
    public class CaptureRequestDTO {

        public string transactionAmount {
            get; set;
        }
        public string transactionID {
            get; set;
        }
        public string isPartialShipment {
            get; set;
        } //"Y"
        public PartialShipmentData partialShipmentData { get; set; } = new PartialShipmentData();
        public string paymentId {
            get; set;
        }
        public string invoiceId {
            get; set;
        }

    }

    public class PartialShipmentData {
        public string currentPaymentSequenceNumber {
            get; set;
        }
        public string totalPaymentCount {
            get; set;
        }
    }
}
