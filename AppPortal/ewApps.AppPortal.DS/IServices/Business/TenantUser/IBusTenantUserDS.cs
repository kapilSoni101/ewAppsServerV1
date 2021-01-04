/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 9 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 September 2019
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for Business TenantUser entity.
    /// </summary>
    public interface IBusTenantUserDS {

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

        Task<List<TenantUserSetupListDTO>> GetAllBusinessUsersAsync(bool deleted);

        Task<List<AppInfoDTO>> GetAllApplicationForTenantAsync(Guid tenantId);

        Task<TenantUserAndAppViewDTO> GetTenantUserAndAppDetails(Guid tenantUserId, bool deleted);

        Task<List<TenantUserDetailsDTO>> GetPaymentAllBusinessUsersAsync(Guid appId, bool deleted);

        Task<List<AppShortInfoDTO>> GetApplicationForUserAsync(Guid tenantUserId);

        Task<List<TenantUserDetailsDTO>> GetShipmentAllBusinessUsersAsync(Guid appId, bool deleted);
        Task<List<TenantUserDetailsDTO>> GetCustomerAppAllBusinessUsersAsync(Guid appId, bool deleted);
        Task<List<TenantUserDetailsDTO>> GetVendorAppAllBusinessUsersAsync(Guid appId, bool deleted);
        Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);

        Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO);
        Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId);
    }
}
