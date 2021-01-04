using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
  public  interface IQCustomerAppDS {

        Task<List<AppInfoDTO>> GetAllCustomerApplicationsAsync(Guid businessPartnertenantId);
        Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey, bool deleted);
    }
}
