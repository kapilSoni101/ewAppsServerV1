/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
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
    /// Methods to implement business logic for app user
    /// </summary>
    public interface ITenantUserDS:IBaseDS<TenantUser> {

        /// <summary>
        /// Get user by email and tenantId.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<TenantUser> GetUserbyEmailAndTenantIdAsync(string email, Guid tenantId);

        /// <summary>
        /// Adds the tenant user and updates the system field along with generate identitynumber.
        /// </summary>
        /// <param name="tenantUser">tenant user entity</param>
        /// <returns></returns>
        Task<TenantUser> AddTenantUserAsync(TenantUser tenantUser);

        ///// <summary>
        ///// Get publisher primary user and get userid from email.
        ///// </summary>
        ///// <param name="reqDto">Publisher request model.</param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task<PublisherTenantInfoDTO> GetPublisherAndUserAsync(PublisherRequestDTO reqDto, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the tenant user by email asynchronous.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>Returns tenant user information that matches given email.</returns>
        Task<TenantUserDTO> GetTenantUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default(CancellationToken));


        Task<UserShortInfoDQ> GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(Guid tenantId, Guid appId, UserTypeEnum uType, CancellationToken token = default(CancellationToken));

        Task<Tuple<string, bool>> UserAlreadyJoinedAnyApplication(Guid tenantUserId);

        void ValidateTenantUser(TenantUser entity, OperationType operationType);

        Task DeleteUserDependencyAsync(Guid tenantUserId, Guid tenantId, Guid appId, Guid? businessAppId);
    }
}