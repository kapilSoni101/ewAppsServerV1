using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.QData {
    public interface IQCustomerAppRepository {

        Task<List<AppInfoDTO>> GetAllCustomerApplicationsAsync(Guid businessPartnerTenantId, string defaultFreeAppKey);
        Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey, bool deleted);
    }
}
