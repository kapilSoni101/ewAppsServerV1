using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
    public interface IPublisherRepository:IBaseRepository<Entity.Publisher> {

        /// <summary>
        /// Get publisher by publisher tenantid.
        /// </summary>
        /// <param name="pubTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Publisher> GetPublisherByPublisherTenantIdAsync(Guid pubTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the publisher list by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IEnumerable<StringDTO>> GetPublisherListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

    }
}
