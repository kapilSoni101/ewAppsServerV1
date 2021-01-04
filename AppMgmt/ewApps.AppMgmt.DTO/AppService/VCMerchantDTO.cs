using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO{
    public class VCMerchantDTO {

        public string VCId {
            get; set;
        }

        public Guid BusinessId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

    }
}
