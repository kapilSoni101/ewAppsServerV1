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
    /// Cust Tenant User class contains all add/update/delete/get methods for Tenant User On Customer.    
    /// </summary>
    public interface ICustTenantUserDS {

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

        Task<List<TenantUserSetupListDTO>> GetAllCustSetupCustomerUsersAsync(Guid businesspartnertenantid, bool deleted);
        Task<List<TenantUserDetailsDTO>> GetPaymentAppCustomerUsers(Guid appId, Guid businesspartnertenantid, bool deleted);
        Task<List<AppInfoDTO>> GetAllCustomerApplicationsAsync(Guid businessPartnerTenantId);
        Task<List<AppShortInfoDTO>> GetApplicationForCustomerUserAsync(Guid tenantUserId);
        Task<TenantUserAndAppViewDTO> GetTenantUserAndAppDetails(Guid tenantUserId, Guid businessPartnerTenantId, bool deleted);
        Task<List<TenantUserDetailsDTO>> GetCustomerAppCustomerUsers(Guid appId, Guid businesspartnertenantid, bool deleted);
        Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
        Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId);
        Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO);
    }
}
