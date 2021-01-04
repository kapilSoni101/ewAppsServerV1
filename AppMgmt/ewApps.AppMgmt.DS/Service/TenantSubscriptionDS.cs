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
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    public class TenantSubscriptionDS:BaseDS<TenantSubscription>, ITenantSubscriptionDS {

        ITenantSubscriptionRepository _tenantSubscriptionRepository;
        
        ITenantLinkingDS _tenantLinkDS;

        #region Constructor
        public TenantSubscriptionDS(ITenantSubscriptionRepository tenantSubscriptionRepository
            ,  ITenantLinkingDS tenantLinkDS) : base(tenantSubscriptionRepository) {
            _tenantSubscriptionRepository = tenantSubscriptionRepository;
            _tenantLinkDS = tenantLinkDS;
        }
        #endregion

        #region Get

        /// <summary>
        /// Get primary user of a application by tenantid and appid.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId"></param>
        /// <param name="uType"></param>
        /// <param name="token"></param>
        /// <returns>return usershort info.</returns>
        public async Task<UserShortInfoDQ> GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(Guid tenantId, Guid appId, UserTypeEnum uType, CancellationToken token = default(CancellationToken)) {
            return await _tenantSubscriptionRepository.GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(tenantId, appId, uType, token);
        }


        /// <summary>
        /// return tenant application subscription list from tenant id.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>return list of tenant subscription.</returns>
        public async Task<List<TenantSubscription>> GetTenantSubscriptionListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
           return await _tenantSubscriptionRepository.GetTenantSubscriptionListByTenantIdAsync(tenantId, token);
        }

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="token"></param>
        /// <returns>return list of application.</returns>
        public async Task<List<UserShortInfoDQ>> GetBusinessPrimaryUsersByBusinessIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {
            return await _tenantSubscriptionRepository.GetBusinessPrimaryUsersByBusinessIdAsync(businessId, token);
        }

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="publisherTenantId">Publisher tenantId.</param>
        /// <returns>return list of application.</returns>
        public async Task<List<TenantAppSubscriptionDQ>> GetBusinessSubscriptionByBusinessIdAsync(Guid businessId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantSubscriptionRepository.GetBusinessSubscriptionByBusinessIdAsync(businessId, publisherTenantId, token);
        }

        #endregion Get

        /// <summary>
        /// Mark application as deleted.
        /// </summary>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="appId">Application id</param>
        /// <returns></returns>
        public async Task DeleteApplicationAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            await _tenantSubscriptionRepository.DeleteApplicationAsync(tenantId, appId, token);
        }
        
    }
}
