using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// 
    /// </summary>
    public interface IPlatformDS :IBaseDS<Entity.Platform> {

        /// <summary>
        /// Get platform Branding details by tenantId and appId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PlatformBrandingDQ> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update platform branding
        /// </summary>
        /// <param name="platformBrandingDQ"></param>
        void UpdatePlatformBranding(PlatformBrandingDQ platformBrandingDQ);

        /// <summary>
        /// Get Theme name and theme key
        /// </summary>
        /// <returns></returns>
        Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey();


        /// <summary>
        /// Get Connector list
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<ConnectorDQ>> GetPlatformConnectorListAsync(CancellationToken token = default(CancellationToken));

    }
}
