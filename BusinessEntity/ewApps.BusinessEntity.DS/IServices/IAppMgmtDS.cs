using System;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This interface provides method to manage app management operations.
    /// </summary>
    public interface IAppMgmtDS {

        /// <summary>
        /// get tanant primary user .
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Guid> GetTenantPrimaryUserAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
