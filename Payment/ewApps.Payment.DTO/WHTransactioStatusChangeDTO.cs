using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class WHTransactioStatusChangeDTO {

        public Guid PaymentId {
            get; set;
        }
        public string Status {
            get; set;
        }
        public string ReturnCode {
            get; set;
        }
        public string ReturnString {
            get; set;
        }
    }
}
