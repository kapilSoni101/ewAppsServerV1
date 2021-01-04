/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author : Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 05 September 2019
 * 
 * Contributor/s: 
 * Last Updated On: 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// A wrapper interfcae contain method to add/update/delete/get publisher data.
    /// </summary>
    public interface IPublisherAppDS {

        /// <summary>
        /// Get publisher subscribed application, services.
        /// </summary>
        /// <param name="sourcesubdomain"></param>
        /// <param name="includeInactive"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusinessApplicationDTO>> GetPublisherAppServicesAndSubscriptionDetailsAsync(string sourcesubdomain, bool includeInactive, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application list that matches input parameters.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active] only active applications will fetch.</param>
        /// <param name="subscriptionMode">The subscription mode to filter applications.</param>
        /// <returns>Returns application list that matches given input parameters.</returns>
        Task<List<AppInfoDTO>> GetApplicationListAsync(bool active, int subscriptionMode);

        /// <summary>
        /// Gets the application details publisher asynchronous.
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppDetailDTO>> GetAppDetailsPublisherAsync(Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application details with services publisher asynchronous.
        /// </summary>
        /// <param name="publisherAppID">The publisher application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<AppAndServiceDTO> GetAppDetailsWithServicesPublisherAsync(Guid publisherAppSettingID, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Updates the application asynchronous.
        /// </summary>
        /// <param name="appAndServiceDTO">The application and service dto.</param>
        /// <returns></returns>
        Task UpdateAppAsync(AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the business list by application identifier asynchronous publisher.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application services name by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceDTO>> GetAppServicesNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the business application name list by publisher tenant identifier.
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns business application name list that matches the provided publisher tenant id.</returns>
        Task<IEnumerable<string>> GetAppNameListByPublisheIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<BusinessApplicationDTO>> GetPublisherAppServicesAndSubscriptionDetailsOnPlatformAsync(string sourcesubdomain, bool includeInactive, Guid publisherTenantId, CancellationToken token = default(CancellationToken));
    }
}
