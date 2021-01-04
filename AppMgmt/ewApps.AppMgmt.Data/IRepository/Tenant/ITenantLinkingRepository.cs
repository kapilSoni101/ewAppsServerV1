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
using ewApps.AppMgmt.Entity;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Data {
    public interface ITenantLinkingRepository:IBaseRepository<TenantLinking> {

        #region Get

        /// <summary>
        ///  Get tenant linking by subdomain name and tenanttype.
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
        Task<TenantLinking> GetTenantLinkingByTenantIdAsync(Guid tenantId, TenantType tenantType, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
