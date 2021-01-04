using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class TenantUserSignUpResponseDTO {

        public TenantUserDTO TenantUserDTO {
            get; set;
        }

        public List<UserAppRelationDTO> UserAppRelationDTOs {
            get; set;
        }

        public Tuple<string, bool> NewUser {
            get; set;
        }
    }
}
