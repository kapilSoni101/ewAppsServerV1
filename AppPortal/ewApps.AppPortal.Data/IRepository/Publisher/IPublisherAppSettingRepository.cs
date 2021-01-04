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

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// 
    /// </summary>
    public interface IPublisherAppSettingRepository:IBaseRepository<PublisherAppSetting> {

        /// <summary>
        /// Get appdetail bu publisher tenant and appid.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="pubTenantId"></param>
        /// <returns></returns>
        // Task<AppDetailDQ> GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(Guid appId, Guid pubTenantId, CancellationToken token = default(CancellationToken));

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
        /// <returns>Returns business application name list that matches the provided publisher id.</returns>
        IEnumerable<string> GetAppNameListByPublisherId(Guid publisherId);

        }
    }
