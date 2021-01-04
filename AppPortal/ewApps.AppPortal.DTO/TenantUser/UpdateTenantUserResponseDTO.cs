using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class UpdateTenantUserResponseDTO {


        public Guid TenantUserId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public bool PermissionsChanged {
            get; set;
        }

        public bool StatusChanged {
            get; set;
        }

    }
}
