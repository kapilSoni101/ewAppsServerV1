using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class UserApplicationModel {

        public Guid TenantId {
            get; set;
        }
        public string UserId {
            get; set;
        }
        public string ClientAppType {
            get; set;
        }
    }
}
