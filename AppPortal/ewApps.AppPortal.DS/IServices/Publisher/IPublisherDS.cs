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
    public interface IPublisherDS:IBaseDS<Entity.Publisher> {

        /// <summary>
        /// Get publisher branding details by Tenantid and AppId.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PublisherBrandingDQ> GetPublisherBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Publisher branding
        /// </summary>
        /// <param name="publisherBrandingDQ"></param>
        Task UpdatePublisherBranding(PublisherBrandingDQ publisherBrandingDQ);


        /// <summary>
        /// Get Theme name theme key 
        /// </summary>
        /// <returns></returns>
        Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey();


        /// <summary>
        /// Method return configuration detail
        /// </summary>
        Task<ConfigurationDTO> GetConfigurationDetailAsync(CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Configuration details
        /// </summary>
        /// <param name="configurationDQ"></param>
        /// <returns></returns>
        Task UpdateConfigurationDetailAsync(ConfigurationDTO configurationDQ);

        string GetNextMaxNo(Guid tenantId, int entityType);

        /// <summary>
        /// Get publisher by publisher tenantid.
        /// </summary>
        /// <param name="pubTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Publisher> GetPublisherByPublisherTenantIdAsync(Guid pubTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Connector list
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<ConnectorDQ>> GetPublisherConnectorListAsync(CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Sync data time log from connector to application
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusBASyncTimeLogDTO>> SyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
