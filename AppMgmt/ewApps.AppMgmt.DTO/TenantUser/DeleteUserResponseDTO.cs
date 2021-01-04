using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class DeleteUserResponseDTO {

        public Guid TenantId {
            get; set;
        }

        public bool UserDeleted {
            get; set;
        }
    }
}
