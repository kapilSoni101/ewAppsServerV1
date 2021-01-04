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
    /// Bus Tenant User Controller class contains all add/update/delete/get methods for Tenant User On Business.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BusTenantUserController:ControllerBase {

        #region Local Variables

        IBusTenantUserDS _busTenantUserDS;
        IBusTenantUserSignUpDS _busTenantUserSignUpDS;
        IBusTenantUserUpdateDS _busTenantUserUpdateDS;
        IRealTimeUpdate _realtimeUpdate;
        IBusTenantUserDeleteDS _busTenantUserDeleteDS;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initilize local variables. 
        /// </summary>
        public BusTenantUserController(IBusTenantUserDS busTenantUserDS, IBusTenantUserSignUpDS busTenantUserSignUpDS,
            IBusTenantUserUpdateDS busTenantUserUpdateDS, IBusTenantUserDeleteDS busTenantUserDeleteDS, IRealTimeUpdate realtimeUpdate) {
            _busTenantUserDS = busTenantUserDS;
            _busTenantUserSignUpDS = busTenantUserSignUpDS;
            _busTenantUserUpdateDS = busTenantUserUpdateDS;
            _busTenantUserDeleteDS = busTenantUserDeleteDS;
            _realtimeUpdate = realtimeUpdate;
        }

        #endregion Constructor

        #region SetupApp

        #region Get

        [HttpGet]
        [Route("bizsetuplist/{deleted}")]
        public async Task<List<TenantUserSetupListDTO>> GetAllBusinessUsersAsync([FromRoute]bool deleted) {
            return await _busTenantUserDS.GetAllBusinessUsersAsync(deleted);
        }

        [HttpGet]
        [Route("applist/{tenantid:Guid}")]
        public async Task<List<AppInfoDTO>> GetAllTenantAppAsync([FromRoute] Guid tenantId) {
            return await _busTenantUserDS.GetAllApplicationForTenantAsync(tenantId);
        }

        [HttpGet]
        [Route("userapplicationnames/{tenantuserid:Guid}")]
        public async Task<List<AppShortInfoDTO>> GetUserApplicationNamesForUserAsync([FromRoute] Guid tenantuserid) {
            return await _busTenantUserDS.GetApplicationForUserAsync(tenantuserid);
        }

        [HttpGet]
        [Route("detail/{tenantuserid:Guid}/{deleted}")]
        public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails([FromRoute] Guid tenantuserid, [FromRoute] bool deleted) {
            return await _busTenantUserDS.GetTenantUserAndAppDetails(tenantuserid, deleted);
        }

        #endregion Get

        #region Add

        [HttpPost]
        [Route("signupuser")]
        public async Task<TenantUserSignUpResponseDTO> SignUpPlatformTenantUserAsync([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _busTenantUserSignUpDS.SignUpUserAsync(tenantUserSignUpDTO);
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
            return await _busTenantUserDeleteDS.DeleteTenantUser(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #region Update

        [HttpPut]
        [Route("updateuser")]
        public async Task<UpdateTenantUserResponseDTO> UpdateBusinessTenantUser([FromBody]TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO) {

            // Update User
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await _busTenantUserUpdateDS.UpdateTenantUserAsync(tenantUserUpdateRequestDTO);

            // send signalR when user permission change and user staus change
            if(updateTenantUserResponseDTO.PermissionsChanged || updateTenantUserResponseDTO.StatusChanged) {
                MessagePayload payload = new MessagePayload(BusinessLiveUpdateConstants.UserPermissionChangeEvent, tenantUserUpdateRequestDTO.TenantUserId);
                string groupName = "";
                await _realtimeUpdate.SendMessageAsync(BusinessLiveUpdateConstants.BusinessLiveUpdateHandler, payload, groupName);
            }

            return updateTenantUserResponseDTO;
        }

        [HttpPut]
        [Route("reinvite/{tenantuserid:Guid}/{appid:Guid}")]
        public async Task<ResponseModelDTO> ReInviteUser([FromRoute] Guid tenantUserId, [FromRoute] Guid appId) {
            await _busTenantUserDS.ReInviteTenantUserAsync(tenantUserId, appId);
            return new ResponseModelDTO() {
                Id = tenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

        [HttpPut]
        [Route("cancelinvite")]
        public async Task<ResponseModelDTO> CancelInviteForTenantUser([FromBody] TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            await _busTenantUserDS.CancelTenantUserInvitation(tenantUserIdentificationDTO);
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
        [Route("paylist/{appid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllPaymentBusinessUsersAsync([FromRoute]Guid appId, [FromRoute]bool deleted) {
            return await _busTenantUserDS.GetPaymentAllBusinessUsersAsync(appId, deleted);
        }

        #endregion Get

        #endregion Payment 

        #region Shipment 

        #region Get

        [HttpGet]
        [Route("shiplist/{appid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllShipmnetBusinessUsersAsync([FromRoute]Guid appId, [FromRoute]bool deleted) {
            return await _busTenantUserDS.GetShipmentAllBusinessUsersAsync(appId, deleted);
        }

        #endregion Get

        #endregion Shipment 

        #region Customer App 

        #region Get

        [HttpGet]
        [Route("custapplist/{appid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllCustomerAppBusinessUsersAsync([FromRoute]Guid appId, [FromRoute]bool deleted) {
            return await _busTenantUserDS.GetCustomerAppAllBusinessUsersAsync(appId, deleted);
        }

        #endregion Get

        #endregion Customer App 

        #region Vendor App

        #region Get

        [HttpGet]
        [Route("vendorapplist/{appid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllVendorAppBusinessUsersAsync([FromRoute]Guid appId, [FromRoute]bool deleted) {
            return await _busTenantUserDS.GetVendorAppAllBusinessUsersAsync(appId, deleted);
        }

        #endregion Get

        #endregion Vendor App

        #region Common

        #region Get

        /// <summary>
        /// Get User Detail By User Id.
        /// </summary>
        /// <returns></returns>
        [Route("getuserinfo/{id}")]
        [HttpGet]
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync([FromRoute]Guid id) {
            return await _busTenantUserDS.GetUserInfoByIdAsync(id);
        }

        #endregion Get

        #region Update

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <returns></returns>
        [Route("profileupdate")]
        [HttpPut]
        public async Task<ResponseModelDTO> Update(TenantUserProfileDTO dto) {
            await _busTenantUserDS.UpdateTenantUserProfile(dto);
            return new ResponseModelDTO() {
                Id = dto.ID,
                IsSuccess = true,
                Message = "Tenant User profile updated sucessfully"
            };
        }

        [HttpPut]
        [Route("forgot/password")]
        public async Task<ResponseModelDTO> ForgotPassword([FromBody]ForgotPasswordDTO forgotPasswordDTO) {
            await _busTenantUserDS.ForgotPasswordAsync(forgotPasswordDTO);
            return new ResponseModelDTO {
                IsSuccess = true,
                Message = "Forgot Password mail sent successfully"
            };
        }

        #endregion Update

        #endregion Common
    }
}