using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IBusTenantUserUpdateDS {

        Task<UpdateTenantUserResponseDTO> UpdateTenantUserAsync(TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO, CancellationToken cancellationToken = default(CancellationToken));

    }
}
