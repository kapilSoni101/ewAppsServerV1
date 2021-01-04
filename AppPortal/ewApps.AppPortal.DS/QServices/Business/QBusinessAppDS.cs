using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public class QBusinessAppDS:IQBusinessAppDS {

        IQBusinessAppRepository _qBusinessAppRepository;

        public QBusinessAppDS(IQBusinessAppRepository qBusinessAppRepository) {
            _qBusinessAppRepository = qBusinessAppRepository;
        }

        public async Task<List<AppInfoDTO>> GetAllApplicationForTenantAsync(Guid tenantId) {
            return await _qBusinessAppRepository.GetAllApplicationForTenantAsync(tenantId, AppKeyEnum.biz.ToString());
        }

        public async Task<List<BusCustomerApplicationDTO>> GetAppForCustomer(Guid tenantId, Guid businessPartnerTenantId) {
            return await _qBusinessAppRepository.GetAppForCustomerAsync(tenantId, businessPartnerTenantId);
        }
    public async Task<List<BusVendorApplicationDTO>> GetAppForVendor(Guid tenantId, Guid businessPartnerTenantId)
    {
      return await _qBusinessAppRepository.GetAppForVendorAsync(tenantId, businessPartnerTenantId);
    }

    public async Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, string appKey, bool deleted) {
            return await _qBusinessAppRepository.GetTenantUserAppAndPermissionDetailsAsync(tenantUserId, tenantId, appKey, deleted);
        }

        public async Task<BusCustomerApplicationCountDTO> GetAppCountForCustomerAsync(Guid businessPartnerTenantId) {
            return await _qBusinessAppRepository.GetAppCountForCustomerAsync( businessPartnerTenantId);
        }
    public async Task<BusVendorApplicationCountDTO> GetAppCountForVendorAsync(Guid businessPartnerTenantId)
    {
      return await _qBusinessAppRepository.GetAppCountForVendorAsync(businessPartnerTenantId);
    }
    public async Task<List<AppInfoDTO>> GetCustomerAsignedAppInfoAsync(Guid businessPartnerTenantId) {
            return await _qBusinessAppRepository.GetCustomerAsignedAppInfoAsync(businessPartnerTenantId);
        }
    public async Task<List<AppInfoDTO>> GetVendorAsignedAppInfoAsync(Guid businessPartnerTenantId)
    {
      return await _qBusinessAppRepository.GetVendorAsignedAppInfoAsync(businessPartnerTenantId);
    }

    public async Task<AppShortInfoDTO> GetAppShortInfoByAppId(Guid appId, Guid tenantId) {
            return await _qBusinessAppRepository.GetAppShortInfoByAppId(appId, tenantId);
        }
    }
}
