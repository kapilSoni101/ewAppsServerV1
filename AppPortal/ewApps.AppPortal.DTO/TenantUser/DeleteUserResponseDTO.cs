using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class DeleteUserResponseDTO {

        public Guid TenantId {
            get; set;
        }

        public bool UserDeleted {
            get; set;
        }
    }
}
