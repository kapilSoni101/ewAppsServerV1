using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class InvoicePaymentDTO {

        public string InvoiceId {
            get; set;
        }

        public string OrignalAmount {
            get; set;
        }

        public string ReceivedAmount {
            get; set;
        }

        public decimal ConversionRate {
            get;set;
        }

    }
}
