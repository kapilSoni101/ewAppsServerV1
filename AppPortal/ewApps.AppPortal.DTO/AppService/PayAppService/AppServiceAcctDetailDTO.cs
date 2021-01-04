using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class AppServiceAcctDetailDTO {

        public Guid ID {
            get; set;
        }

        public string AccountJson {
            get; set;
        }

        public int AccountType {
            get; set;
        }

        public Guid? PreAuthPaymentID {
            get;set;
        }

    }
}
