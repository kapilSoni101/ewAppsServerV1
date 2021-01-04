using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUserStatusDS {

        Task<bool> UpdateTenantUserLoginJoinedStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO);
    }
}
