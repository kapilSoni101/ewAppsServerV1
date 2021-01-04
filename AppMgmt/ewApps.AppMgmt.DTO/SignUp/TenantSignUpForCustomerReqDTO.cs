using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {

  public class TenantSignUpForCustomerReqDTO {
        public Guid TenantId {
            get;
            set;
        }
        public Guid BusinesPartnerTenantId {
            get;
            set;
        }
        public Guid BusinesPrimaryUserId {
            get;
            set;
        }
        public string CutomerName {
            get;
            set;
        }
        public string Currency {
            get;
            set;
        }
    }
}
