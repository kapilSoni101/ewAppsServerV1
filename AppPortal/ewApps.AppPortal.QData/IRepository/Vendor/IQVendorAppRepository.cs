using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.QData {
    public interface IQVendorAppRepository {

        Task<List<AppInfoDTO>> GetAllVendorApplicationsAsync(Guid businessPartnerTenantId, string defaultFreeAppKey);
        Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey, bool deleted);
    }
}
