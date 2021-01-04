using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DTO;

namespace ewApps.Payment.DS {
    public interface ICustUserAppManagmentDS {

        Task AppAsignAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO);

        Task AppDeAssignAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO);

        Task<RoleUpdateResponseDTO> UpdateTenantUserRoleAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken));
    }
}
