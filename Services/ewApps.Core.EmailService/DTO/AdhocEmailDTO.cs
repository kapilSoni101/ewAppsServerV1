using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.EmailService {
    public class AdhocEmailDTO {

        public string MessagePart1 {
            get; set;
        }

        public string MessagePart2 {
            get; set;
        }

        public string EmailAddress {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public int DeliveryType {
            get; set;
        }

    }
}
