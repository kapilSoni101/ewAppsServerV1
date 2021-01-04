using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface ICustTenantUserStatusDS {
        Task<bool> UpdateTenantUserLoginJoinedStatusAsync(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO);
    }
}
