using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class PaymentTransectionStatusDTO {

        /// <summary>
        /// payment Id
        /// </summary>
        public Guid PaymentId {
            get; set;
        }
        /// <summary>
        /// Last known status update date/time
        /// </summary>
        public DateTime LastStatusUpdateDate {
            get; set;
        }

        /// <summary>
        /// Status
        /// </summary>
        public string Status {
            get; set;
        }
        /// <summary>
        /// Last known status reason, if any
        /// </summary>
        public string Reason {
            get; set;
        }

        public string ReturnCode {
            get; set;
        }

    }
}
