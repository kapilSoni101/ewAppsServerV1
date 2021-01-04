using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
   public interface IBusTenantUserDeleteDS {

        Task<DeleteUserResponseDTO> DeleteTenantUser(TenantUserIdentificationDTO tenantUserIdentificationDTO);
    }
}
