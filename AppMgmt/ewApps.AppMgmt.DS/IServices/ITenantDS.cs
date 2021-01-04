/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
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

    /// <summary>
    /// Interface contains all required methods for tenant.
    /// </summary>
    public interface ITenantDS:IBaseDS<Tenant> {

        /// <summary>
        /// return true if domain name already exist.
        /// </summary>
        /// <param name="subdomain">Domain name for tenant.</param>
        /// <param name="tenantId">ID of tenant.</param>
        /// <returns></returns>
        Task<bool> IsSubdomainExistAsync(string subdomain, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Added the system field genrate new identitynumber and add tenantentity.
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        Task<Tenant> AddTenantAsync(Tenant tenant);

        /// <summary>
        /// Gets the tenant information by tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="uType">Type of the u.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<TenantInfoDTO> GetTenantInfoByTenantIdAsync(Guid tenantId, int uType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the tenant information by subdomain asynchronous.
        /// </summary>
        /// <param name="subdomain">The subdomain.</param>
        /// <param name="uType">Type of the u.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<TenantInfoDTO> GetTenantInfoBySubdomainAsync(string subdomain, int uType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get tenant model by tenant id.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UpdateTenantModelDQ> GetBusinessTenantDetailModelDTOAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Tenant info by tenantId 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TenantInfoDTO> GetTenantByIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the business name by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the business name by application identifier plat asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<string>> GetBusinessNameByAppIdPlatAsync(Guid appId, CancellationToken token = default(CancellationToken));

    }
}
