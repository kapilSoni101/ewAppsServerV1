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
using ewApps.Core.BaseService;
using ewApps.AppPortal.Entity;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to Role entity.
  /// </summary>
  public interface IRoleDS:IBaseDS<Role> {

    /// <summary>
    /// Get admin rol by application id.
    /// </summary>
    /// <param name="appId">Unique application id.</param>
    /// <param name="userType">UserType</param>
    /// <param name="token">Async token</param>
    /// <returns></returns>
    Task<Role> GetAdminRoleIdByAppIdAndUserTypeAsync(Guid appId, int userType, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Creates the role if not exists else retuen the existinf roleId.
    /// </summary>
    /// <param name="userPermissionbitMask">permission bit mask of the user</param>
    /// <param name="appId">appliation identitfier of the user</param>
    /// <param name="userType">User tyep of the user</param>
    /// <param name="loginTenantUserId">Login user tenantUser id to identitify the user</param>
    /// <param name="token">canclation token for the async methods</param>
    /// <returns></returns>
    Task<Guid> GetOrCreateRoleAsync(long userPermissionbitMask, Guid appId, int userType, Guid loginTenantUserId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Gets the role entity by appid and userid.
    /// </summary>
    /// <returns>Role entity.</returns>
    Role GetEntityByAppUserAndAppId();
  }
}
