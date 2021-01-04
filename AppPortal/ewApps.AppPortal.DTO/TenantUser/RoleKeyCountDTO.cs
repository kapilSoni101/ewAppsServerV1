using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class RoleKeyCountDTO {
        public string RoleKey {
            get; set;
        }
        public Guid TenantUserId {
            get; set;
        }
    }
}
