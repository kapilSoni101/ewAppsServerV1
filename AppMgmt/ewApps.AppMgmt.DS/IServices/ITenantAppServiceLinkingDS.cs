/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Entity;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface ITenantAppServiceLinkingDS:IBaseDS<TenantAppServiceLinking> {       

        #region Delete

        ///// <summary>
        ///// Delete appservice linking and attribute.
        ///// </summary>
        ///// <param name="appId"></param>
        ///// <param name="appServiceId"></param>
        ///// <param name="attrbuteId"></param>
        ///// <param name="tenantId"></param>
        ///// <returns></returns>
        //Task DeleteAppServiceLinkingByAppServiceAndAttributeAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid tenantId, CancellationToken token = default(CancellationToken));

        ///// <summary>
        ///// Delete appservice linking and attribute.
        ///// </summary>
        ///// <param name="appId"></param>
        ///// <param name="appServiceId"></param>        
        ///// <param name="tenantId"></param>
        ///// <returns></returns>
        //Task DeleteAppServiceLinkingByAppServiceAndAttributeAsync(Guid appId, Guid appServiceId, Guid tenantId, CancellationToken token = default(CancellationToken));

        #endregion Delete

        /// <summary>
        /// Get application subscribed services by TenantId and application id.
        /// </summary>
        /// <param name="businessId">Unique business id.</param>
        /// <param name="appId">Application id</param>
        /// <param name="token"></param>
        /// <returns>return list of TenantAppService.</returns>
        Task<List<TenantAppServiceLinking>> GetAppServiceByTenantIdAndAppIdAsync(Guid businessId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application sub services subscribed by a Tenant applications.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="entityTypeEnum">Entity type</param>
        /// <param name="token"></param>
        /// <returns>return Guid of application sub services and Application id.</returns>
        Task<List<TenantAppServiceDQ>> GetAppServiceByBusinessIdAsync(Guid businessId, int entityTypeEnum, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Entity by App and Tenant id 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        Task<List<TenantAppServiceLinking>> GetEntityByAppAndTenantId(Guid appId, Guid tenantId, bool includeDeleted);

        }
}
