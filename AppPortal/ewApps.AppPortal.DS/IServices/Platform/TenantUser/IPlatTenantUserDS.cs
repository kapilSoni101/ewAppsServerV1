/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 31 October 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implement business logic for app user
    /// </summary>
    public interface IPlatTenantUserDS {


        /// <summary>
        /// this method is used to user profile detail
        /// </summary>
        /// <param name="tenantUserId"></param>
        /// <returns></returns>
        Task<TenantUserProfileDTO> GetUserInfoByIdAsync(Guid tenantUserId);

        /// <summary>
        /// Update Tenant User Profile 
        /// </summary>
        /// <param name="tenantUserProfileDTO"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> UpdateTenantUserProfile(TenantUserProfileDTO tenantUserProfileDTO);


        Task<List<TenantUserDetailsDTO>> GetPlatformTenantUsers(Guid appId, bool deleted);

        Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId);

        Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO);

        Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);

        Task<TenantUserAndPermissionViewDTO> GetTenantUserAndPermissionDetails(Guid tenantUserId, bool deleted);

    }
}