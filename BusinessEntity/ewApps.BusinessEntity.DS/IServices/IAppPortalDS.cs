using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.BusinessEntity.DS {
    public interface IAppPortalDS {
        /// <summary>
        /// get tanantsubscribed application key .
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<string>> GetTenantSuscribedApplicationKeyAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
