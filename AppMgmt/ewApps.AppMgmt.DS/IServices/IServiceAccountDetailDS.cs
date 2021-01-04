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
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface IServiceAccountDetailDS:IBaseDS<AppServiceAccountDetail> {

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
        /// Get Entity By Entity Type and ServiceId and Attribute ID 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <param name="appServiceId"></param>
        /// <param name="appServiceAttributeId"></param>
        /// <returns></returns>
        Task<List<AppServiceAccountDetail>> GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(int entityType, Guid entityId, Guid appServiceId, Guid appServiceAttributeId);

        /// <summary>
        /// Delete Entity By Entity Type and Service Id 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <param name="appServiceId"></param>
        /// <param name="appServiceAttributeId"></param>
        /// <param name="parentOpType"></param>
        /// <returns></returns>
        Task<List<AppServiceAccDetailIdDTO>> DeleteByEntityTypeAndEntityAndServiceId(int entityType, Guid entityId, Guid appServiceId, Guid appServiceAttributeId, OperationType parentOpType);
    }
}
