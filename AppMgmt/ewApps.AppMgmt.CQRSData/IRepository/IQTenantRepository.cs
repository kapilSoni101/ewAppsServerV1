using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.QData {
    public interface IQTenantRepository {

        #region Get

        /// <summary>
        /// Get application services by business id.
        /// </summary>
        /// <param name="tenantId">Unique tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<TenantAppServiceDQ>> GetAppServiceByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        #endregion Get   

    }
}
