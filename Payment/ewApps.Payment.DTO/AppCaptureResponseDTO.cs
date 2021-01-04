using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class AppCaptureResponseDTO {

        public string Status {
            get; set;
        }
        public string ErrorMessage {
            get; set;
        }

        public CaptureResponseDTO Response {
            get; set;
        }
        public TSysTransectionDTO Transaction {
            get; set;
        }

    }
}
