using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ewApps.AppMgmt.DS{
    /// <summary>
    /// A wrapper class, contains a method to get business tenant and supprtive entities data.
    /// </summary>
    public interface ITenantForBusinessDS {

        /// <summary>
        /// Get business update model.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UpdateTenantModelDQ> GetBusinessUpdateModelAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        #region Delete

        /// <summary>
        /// Delete business by tenantid.
        /// </summary>
        /// <param name="tenantId">Tenant id of business.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteBusinessTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        #endregion Delete

    }
}
