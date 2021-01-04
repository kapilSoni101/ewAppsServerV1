using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUserUpdateForPublisherDS {

        Task<UpdateTenantUserResponseDTO> UpdateUserAsync(TenantUserDTO tenantUserDTO, Guid tenantId, Guid appId);
    }
}
