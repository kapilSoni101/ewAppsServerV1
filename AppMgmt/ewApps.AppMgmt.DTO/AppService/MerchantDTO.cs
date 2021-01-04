using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class MerchantDTO {
        public string MID {
            get; set;
        }
        public string UserId {
            get; set;
        }
        public string Password {
            get; set;
        }
        public string TransactionKey {
            get; set;
        }
        public string DeveloperId {
            get; set;
        }
        public string DeviceId {
            get; set;
        }
        public bool Active {
            get; set;
        }
    }
}
