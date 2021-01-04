/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 24 September 2018

 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;

namespace ewApps.AppPortal.DS {

    public interface IQPlatformAndUserDS {

        /// <summary>
        /// Get Platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PlatformBrandingDQ> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Method return all the apps with its service count
        /// </summary>
        /// <returns></returns>
        Task<List<AppDetailDTO>> GetAppDetailsAsync();

        /// <summary>
        /// Gets the application details by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<AppDetailDTO> GetAppDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        Task<List<TenantUserDetailsDTO>> GetAllPlatfromUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted);

        Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId, Guid? businessPartnerTenantId);

        Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey, bool deleted);

    }
}
