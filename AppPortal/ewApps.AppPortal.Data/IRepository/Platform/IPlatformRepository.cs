using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// 
    /// </summary>
    public interface IPlatformRepository : IBaseRepository<Platform>{

        /// <summary>
        /// Get platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PlatformBrandingDTO> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

       

        }
}
