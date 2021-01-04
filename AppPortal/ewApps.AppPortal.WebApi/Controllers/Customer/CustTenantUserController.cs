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
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.SignalRService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Cust Tenant User Controller class contains all add/update/delete/get methods for Tenant User On Customer.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustTenantUserController:ControllerBase {

        #region Local Variable

        ICustTenantUserDS _custTenantUserDS;
        ICustTenantUserSignUpDS _custTenantUserSignUpDS;
        ICustTenantUserUpdateDS _custTenantUserUpdateDS;
        ICustTenantUserDeleteDS _custTenantUserDeleteDS;
        IRealTimeUpdate _realtimeUpdate;

        #endregion

        #region Constructor

        public CustTenantUserController(ICustTenantUserDS custTenantUserDS, ICustTenantUserSignUpDS custTenantUserSignUpDS,
            ICustTenantUserDeleteDS custTenantUserDeleteDS, ICustTenantUserUpdateDS custTenantUserUpdateDS, IRealTimeUpdate realtimeUpdate) {
            _custTenantUserDS = custTenantUserDS;
            _custTenantUserSignUpDS = custTenantUserSignUpDS;
            _custTenantUserUpdateDS = custTenantUserUpdateDS;
            _custTenantUserDeleteDS = custTenantUserDeleteDS;
            _realtimeUpdate = realtimeUpdate;
        }

        #endregion

        #region SetupApp

        #region Get

        /// <summary>
        /// ApI for getting the list of all the users of the customer we show all the customer users on setup application.
        /// </summary>
        /// <param name="businessPartnerTenantId">Customer unique identifier.</param>
        /// <param name="deleted">deleted flag to get deleted or not deleted users.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("custsetuplist/{businesspartnertenantid:Guid}/{deleted}")]
        public async Task<List<TenantUserSetupListDTO>> GetAllCustSetupCustomerUsersAsync([FromRoute]Guid businessPartnerTenantId, [FromRoute]bool deleted) {
            return await _custTenantUserDS.GetAllCustSetupCustomerUsersAsync(businessPartnerTenantId, deleted);
        }

        [HttpGet]
        [Route("detail/{tenantuserid:Guid}/{businesspartnertenantid:Guid}/{deleted}")]
        public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails([FromRoute] Guid tenantuserid, [FromRoute] Guid businesspartnertenantid, [FromRoute] bool deleted) {
            return await _custTenantUserDS.GetTenantUserAndAppDetails(tenantuserid, businesspartnertenantid, deleted);
        }

        /// <summary>
        /// Gets the users asigned applications.
        /// </summary>
        /// <param name="tenantuserid">User unique identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("userapplicationnames/{tenantuserid:Guid}")]
        public async Task<List<AppShortInfoDTO>> GetUserApplicationNamesForUserAsync([FromRoute] Guid tenantuserid) {
            return await _custTenantUserDS.GetApplicationForCustomerUserAsync(tenantuserid);
        }

        [HttpGet]
        [Route("applist/{businesspartnertenantid:Guid}")]
        public async Task<List<AppInfoDTO>> GetAllCustomerAppAsync([FromRoute] Guid businessPartnerTenantId) {
            return await _custTenantUserDS.GetAllCustomerApplicationsAsync(businessPartnerTenantId);
        }

        #endregion Get

        #region Add

        [HttpPost]
        [Route("signupuser")]
        public async Task<TenantUserSignUpResponseDTO> SignUpPlatformTenantUserAsync([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _custTenantUserSignUpDS.SignUpUserAsync(tenantUserSignUpDTO);
        }

        #endregion Add

        #region Delete

        /// <summary>
        /// Method fot deleting the platfrom user.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("delete")]
        public async Task<DeleteUserResponseDTO> DeleteTenantUser([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            return await _custTenantUserDeleteDS.DeleteTenantUser(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #region Update

        [HttpPut]
        [Route("updateuser")]
        public async Task<UpdateTenantUserResponseDTO> UpdateCustomerTenantUser([FromBody]TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO) {
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await _custTenantUserUpdateDS.UpdateTenantUserAsync(tenantUserUpdateRequestDTO);
            // send signalR when user permission change and user staus change
            if(updateTenantUserResponseDTO.PermissionsChanged || updateTenantUserResponseDTO.StatusChanged) {

                MessagePayload payload = new MessagePayload(CustomerLiveUpdateConstants.UserPermissionChangeEvent, tenantUserUpdateRequestDTO.TenantUserId);
                string groupName = "";
                await _realtimeUpdate.SendMessageAsync(CustomerLiveUpdateConstants.CustomerLiveUpdateHandler, payload, groupName);

            }

            return updateTenantUserResponseDTO;
        }

        [HttpPut]
        [Route("reinvite/{tenantuserid:Guid}/{appid:Guid}")]
        public async Task<ResponseModelDTO> ReInviteUser([FromRoute] Guid tenantUserId, [FromRoute] Guid appId) {
            await _custTenantUserDS.ReInviteTenantUserAsync(tenantUserId, appId);
            return new ResponseModelDTO() {
                Id = tenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

    [HttpPut]
    [Route("reinviteprimaryuser/{tenantuserid:Guid}")]
    public async Task<ResponseModelDTO> ReInviteUser([FromRoute] Guid tenantUserId) {
      await _custTenantUserDS.ReInviteTenantUserAsync(tenantUserId, Guid.Empty);
      return new ResponseModelDTO() {
        Id = tenantUserId,
        IsSuccess = true,
        Message = "Tenant User reinvited sucessfully"
      };
    }

    [HttpPut]
        [Route("cancelinvite")]
        public async Task<ResponseModelDTO> CancelInviteForTenantUser([FromBody] TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            await _custTenantUserDS.CancelTenantUserInvitation(tenantUserIdentificationDTO);
            return new ResponseModelDTO() {
                Id = tenantUserIdentificationDTO.TenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

        #endregion Update

        #endregion SetupApp

        #region Payment 

        #region Get

        [HttpGet]
        [Route("paylist/{appid:Guid}/{businesspartnertenantid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllCustomerPaymentUsersAsync([FromRoute]Guid appId, [FromRoute]Guid businessPartnerTenantId, [FromRoute]bool deleted) {
            return await _custTenantUserDS.GetPaymentAppCustomerUsers(appId, businessPartnerTenantId, deleted);
        }

        #endregion Get

        #endregion Payment 

        #region Customer App 

        #region Get

        [HttpGet]
        [Route("custlist/{appid:Guid}/{businesspartnertenantid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllCustomerCustomerAppUsersAsync([FromRoute]Guid appId, [FromRoute]Guid businessPartnerTenantId, [FromRoute]bool deleted) {
            return await _custTenantUserDS.GetCustomerAppCustomerUsers(appId, businessPartnerTenantId, deleted);
        }

        #endregion Get

        #endregion Customer App  

        #region Common Methods

        #region Get

        /// <summary>
        /// Get User Detail By User Id.
        /// </summary>
        /// <returns></returns>
        [Route("getuserinfo/{id}")]
        [HttpGet]
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync([FromRoute]Guid id) {
            return await _custTenantUserDS.GetUserInfoByIdAsync(id);
        }

        #endregion Get

        #region Update

        [HttpPut]
        [Route("forgot/password")]
        public async Task<ResponseModelDTO> ForgotPassword([FromBody]ForgotPasswordDTO forgotPasswordDTO) {
            await _custTenantUserDS.ForgotPasswordAsync(forgotPasswordDTO);
            return new ResponseModelDTO {
                IsSuccess = true,
                Message = "Forgot Password mail sent successfully"
            };
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <returns></returns>
        [Route("profileupdate")]
        [HttpPut]
        public async Task<ResponseModelDTO> Update(TenantUserProfileDTO dto) {
            await _custTenantUserDS.UpdateTenantUserProfile(dto);
            return new ResponseModelDTO() {
                Id = dto.ID,
                IsSuccess = true,
                Message = "Tenant User profile updated sucessfully"
            };
        }

        #endregion Update

        #endregion Common Methods

    }
}