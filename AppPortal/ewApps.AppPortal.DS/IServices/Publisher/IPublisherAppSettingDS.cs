/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 January 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Class provide supportive methods for publisher.
    /// </summary>
    public interface IPublisherAppSettingDS:IBaseDS<PublisherAppSetting> {

        /// <summary>
        /// Updates the publisher application setting from application asynchronous.
        /// </summary>
        /// <param name="appAndServiceDTO">The application and service dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task UpdatePublisherAppSettingFromAppAsync(AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Updates the application asynchronous.
        /// </summary>
        /// <param name="appAndServiceDTO">The application and service dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task UpdateAppAsync(AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the by application identifier and publisher tenant identifier.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="pubTenantId">The pub tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns PublisherAppSetting record that matches provided app and publisher tenant id.</returns>
        Task<PublisherAppSetting> GetByAppIdAndPublisherTenantIdAsync(Guid appId, Guid pubTenantId, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Gets the business application name list by publisher identifier.
        /// </summary>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns business application name list that matches the provided publisher id.</returns>
        Task<IEnumerable<string>> GetAppNameListByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken));
    }
}