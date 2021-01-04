/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 9 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 Aug 2019
 */

using System;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {

    /// <summary>
    /// This interface responsible for exposing all the methods that are intrecting with the DB for retriving the data related to Role entity.
    /// </summary>
    public interface IRoleRepository:IBaseRepository<Role> {

        ///// <summary>
        ///// Get admin rol by application id.
        ///// </summary>
        ///// <param name="appId">Unique application id.</param>
        ///// <param name="userType">UserType</param>
        ///// <param name="token">Async token</param>
        ///// <returns></returns>
        //Task<Role> GetAdminRoleIdByAppIdAndUserTypeAsync(Guid appId, int userType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get the role on the basis of bit mask.
        /// </summary>
        /// <param name="maskingBit"></param>
        /// <param name="appId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<Guid?> GetRoleAsync(long maskingBit, Guid? appId, int userType);

        ///// <summary>
        ///// Gets the role entity by appid and userid.
        ///// </summary>
        ///// <param name="appId">AppId</param>
        ///// <param name="tenantUserId">tenant user id</param>
        ///// <returns>Role entity.</returns>
        //Role GetEntityByAppUserAndAppId(Guid appId, Guid tenantUserId);

        ///// <summary>
        ///// Get admin role.
        ///// </summary>
        ///// <returns></returns>
        //Role GetAdminRoleIdAsync(Guid appId, int userType);

        ///// <summary>
        ///// Finds the role of logedin user on the basis of currentappId and appuserId.
        ///// </summary>
        ///// <param name="appId"></param>
        ///// <param name="appUserId"></param>
        ///// <returns></returns>
        //Guid GetRoleByAppUserAndAppIdAsync(Guid appId, Guid appUserId);

        /////<summary>Gets the role entity by givwn appUserId and appId.</summary>
        /////<param name = "appId" > appId </param >
        /////<param name="appUserId">app userid</param>
        /////<returns>Role entity</returns>
        //Role GetEntityByAppUserAndAppId(Guid appId, Guid appUserId);

        ///// <summary>
        ///// Gets the user role by application and tenant and user identifier.
        ///// </summary>
        ///// <param name="applicationId">The application id to get application specific data.</param>
        ///// <param name="tenantId">The tenant id to get tenant specific data.</param>
        ///// <param name="userId">The user id to get user role.</param>
        ///// <returns>Return <see cref="Role"/> instance that matches given application id, tenant id and user id.</returns>
        //Role GetRoleByApplicationAndTenantAndUserId(Guid applicationId, Guid tenantId, Guid userId);

        //long GetPermissionBitMaskForUserAppAndTenant(Guid tenantUserId, Guid tenantId, Guid appId);

    }
}
