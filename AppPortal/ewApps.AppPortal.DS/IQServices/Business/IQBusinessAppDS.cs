using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQBusinessAppDS {

        Task<List<AppInfoDTO>> GetAllApplicationForTenantAsync(Guid tenantId);

        Task<List<BusCustomerApplicationDTO>> GetAppForCustomer(Guid tenantId, Guid businessPartnerTenantId);

    Task<List<BusVendorApplicationDTO>> GetAppForVendor(Guid tenantId, Guid businessPartnerTenantId);

        Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, string appKey, bool deleted);

        Task<BusCustomerApplicationCountDTO> GetAppCountForCustomerAsync(Guid businessPartnerTenantId);
    Task<BusVendorApplicationCountDTO> GetAppCountForVendorAsync(Guid businessPartnerTenantId);
    Task<List<AppInfoDTO>> GetVendorAsignedAppInfoAsync(Guid businessPartnerTenantId);

        Task<List<AppInfoDTO>> GetCustomerAsignedAppInfoAsync(Guid businessPartnerTenantId);

        Task<AppShortInfoDTO> GetAppShortInfoByAppId(Guid appId, Guid tenantId);
    }
}
