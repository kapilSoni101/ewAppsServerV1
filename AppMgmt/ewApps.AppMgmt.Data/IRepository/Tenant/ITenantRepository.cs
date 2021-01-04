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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.AppMgmt.DTO;
using System.Collections.Generic;

namespace ewApps.AppMgmt.Data {

    public interface ITenantRepository:IBaseRepository<Tenant> {

        /// <summary>
        /// return true if domain name already exist.
        /// </summary>
        /// <param name="subdomain">Domain name for tenant.</param>
        /// <param name="tenantId">ID of tenant.</param>
        /// <returns></returns>
        Task<bool> IsSubdomainExistAsync(string subdomain, Guid tenantId, CancellationToken token = default(CancellationToken));

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
