using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public class QCustomerAppDS:IQCustomerAppDS {

        IQCustomerAppRepository _qCustomerAppRepository;

        /// <summary>
        /// Initializing local variables.
        /// </summary>
        public QCustomerAppDS(IQCustomerAppRepository qCustomerAppRepository) {
            _qCustomerAppRepository = qCustomerAppRepository;
        }

        ///<inheritdoc/>
        public async Task<List<AppInfoDTO>> GetAllCustomerApplicationsAsync(Guid businessPartnertenantId) {
            return await _qCustomerAppRepository.GetAllCustomerApplicationsAsync(businessPartnertenantId, AppKeyEnum.custsetup.ToString());
        }

        public async Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey, bool deleted) {
            return await _qCustomerAppRepository.GetTenantUserAppAndPermissionDetailsAsync(tenantUserId, tenantId, businessPartnerTenantId, appKey, deleted);
        }
    
  }
}
