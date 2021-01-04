using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.DTO {
   public  class TenantUserAppManagmentDTO {

        public Guid RoleLinkingId {
            get; set;
        }

        public Guid TenantUserId {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public int UserType {
            get; set;
        }

        public long PermissionBitMask {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }
       
        public bool Admin {
            get; set;
        }
    }
}
