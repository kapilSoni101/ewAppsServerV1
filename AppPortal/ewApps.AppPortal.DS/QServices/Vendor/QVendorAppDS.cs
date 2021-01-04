using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public class QVendorAppDS:IQVendorAppDS {

        IQVendorAppRepository _qVendorAppRepository;

        /// <summary>
        /// Initializing local variables.
        /// </summary>
        public QVendorAppDS(IQVendorAppRepository qVendorAppRepository) {
            _qVendorAppRepository = qVendorAppRepository;
        }

        ///<inheritdoc/>
        public async Task<List<AppInfoDTO>> GetAllVendorApplicationsAsync(Guid businessPartnertenantId) {
            return await _qVendorAppRepository.GetAllVendorApplicationsAsync(businessPartnertenantId, AppKeyEnum.vendsetup.ToString());
        }

        public async Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey, bool deleted) {
            return await _qVendorAppRepository.GetTenantUserAppAndPermissionDetailsAsync(tenantUserId, tenantId, businessPartnerTenantId, appKey, deleted);
        }
    }
}
