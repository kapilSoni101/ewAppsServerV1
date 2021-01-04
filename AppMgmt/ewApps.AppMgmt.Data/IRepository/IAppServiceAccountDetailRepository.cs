/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 2 January 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 2 January 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible to perform CRUD operations on <see cref="AppServiceAccountDetail"/> entity.
    /// </summary>
    public interface IAppServiceAccountDetailRepository:IBaseRepository<AppServiceAccountDetail> {

        /// <summary>
        /// Update app service account detail.
        /// </summary>
        /// <param name="appId">Application id</param>
        /// <param name="appServiceId">AppService unique id like (VeriCheck, UPS/FedEx GUID)</param>
        /// <param name="attrbuteId">attribute id, like Ground, FedEx Ground etc.</param>
        /// <param name="entityId">For which entity data is being save</param>
        /// <param name="accountJson">Json object contains account information.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateAccountDetailJsonAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, string accountJson, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Delete appservice account detail.
        /// </summary>
        /// <param name="appId">Application id</param>
        /// <param name="appServiceId">AppService unique id like (VeriCheck, UPS/FedEx GUID)</param>
        /// <param name="attrbuteId">attribute id, like Ground, FedEx Ground etc.</param>
        /// <param name="entityId">For which entity data is being save</param>
        /// <param name="tenantId">unique tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteAppServiceAccountDetailAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Entity by Service and Entity Id 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <param name="appServiceId"></param>
        /// <param name="appServiceAttributeId"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        Task<List<AppServiceAccountDetail>> GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(int entityType, Guid entityId, Guid appServiceId, Guid appServiceAttributeId, bool includeDeleted);

    }
}
