/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore
 * Last Updated On: 30 January 2019
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide supportive methods.
    /// </summary>
    public interface ITenantLinkingDS:IBaseDS<TenantLinking> {

        #region Get

        /// <summary>
        /// Get tenant linking by tenanttype and subdomain.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TenantLinking> GetTenantLinkingBySubdomainAndTenantTypeAsync(string subdomain, TenantType tenantType, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///  Get tenant linking by tenantid and tenanttype.
        /// </summary>    
        /// <param name="tenantId"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TenantLinking> GetTenantLinkingByTenantIdAndTypeAsync(Guid tenantId, TenantType tenantType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Tenant by tenantid and tenantType
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TenantInfoDTO> GetTenantByTenantIdAndTypeAsync(Guid tenantId, TenantType tenantType, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
