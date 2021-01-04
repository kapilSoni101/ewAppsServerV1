using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class TenantUserIdentificationDTO {

        public Guid TenantUserId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

    }
}
