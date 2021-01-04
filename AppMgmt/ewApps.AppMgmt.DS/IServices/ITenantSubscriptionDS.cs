/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    public interface ITenantSubscriptionDS:IBaseDS<TenantSubscription> {

        #region Get

        /// <summary>
        /// Get primary user of a application by tenantid and appid.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId"></param>
        /// <param name="uType"></param>
        /// <param name="token"></param>
        /// <returns>return usershort info.</returns>
        Task<UserShortInfoDQ> GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(Guid tenantId, Guid appId, UserTypeEnum uType, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// return tenant application subscription list from tenant id.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>return list of tenant subscription.</returns>
        Task<List<TenantSubscription>> GetTenantSubscriptionListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="token"></param>
        /// <returns>return list of application.</returns>
        Task<List<UserShortInfoDQ>> GetBusinessPrimaryUsersByBusinessIdAsync(Guid businessId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="publisherTenantId">Publisher tenantId.</param>
        /// <returns>return list of application.</returns>
        Task<List<TenantAppSubscriptionDQ>> GetBusinessSubscriptionByBusinessIdAsync(Guid businessId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        #endregion Get

        #region CRUD

        /// <summary>
        /// Mark application as deleted.
        /// </summary>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="appId">Application id</param>
        /// <returns></returns>
        Task DeleteApplicationAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

        #endregion CRUD

    }
}
