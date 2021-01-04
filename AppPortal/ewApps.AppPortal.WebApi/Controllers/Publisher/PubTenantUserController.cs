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
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.SignalRService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    /// <summary>
    /// Pub Tenant User Controller class contains all add/update/delete/get methods for Tenant User On Publisher.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PubTenantUserController:ControllerBase {

        #region Local Variables

        IPubTenantUserDS _pubTenantUserDS;
        IPubTenantUserSignUpDS _pubTenantUserSignUpDS;
        IPubTenantUserDeleteDS _pubTenantUserDeleteDS;
        IPubTenantUserUpdateDS _pubTenantUserUpdateDS;
        IRealTimeUpdate _realtimeUpdate;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initilize local variables. 
        /// </summary>
        /// <param name="pubTenantUserDS"></param>
        public PubTenantUserController(IPubTenantUserDS pubTenantUserDS, IPubTenantUserSignUpDS pubTenantUserSignUpDS,
            IPubTenantUserDeleteDS pubTenantUserDeleteDS, IPubTenantUserUpdateDS pubTenantUserUpdateDS, IRealTimeUpdate realtimeUpdate) {
            _pubTenantUserDS = pubTenantUserDS;
            _pubTenantUserSignUpDS = pubTenantUserSignUpDS;
            _pubTenantUserDeleteDS = pubTenantUserDeleteDS;
            _pubTenantUserUpdateDS = pubTenantUserUpdateDS;
            _realtimeUpdate = realtimeUpdate;
        }

        #endregion Constructor

        #region Get

        [HttpGet]
        [Route("list/{appid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllPublisherUsersAsync([FromRoute]Guid appId, [FromRoute]bool deleted) {
            return await _pubTenantUserDS.GetAllPublisherUsersAsync(appId, deleted);
        }

        [HttpGet]
        [Route("detail/{tenantuserid:Guid}/{deleted}")]
        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails([FromRoute] Guid tenantuserid, [FromRoute] bool deleted) {
            return await _pubTenantUserDS.GetTenantUserAndPermissionDetails(tenantuserid, deleted);
        }

        /// <summary>
        /// Get User Detail By User Id.
        /// </summary>
        /// <returns></returns>
        [Route("getuserinfo/{id}")]
        [HttpGet]
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync([FromRoute]Guid id) {
            return await _pubTenantUserDS.GetUserInfoByIdAsync(id);
        }

        #endregion Get

        #region Add 

        [HttpPost]
        [Route("signupuser")]
        public async Task<TenantUserSignUpResponseDTO> SignUpPublisherTenantUserAsync([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _pubTenantUserSignUpDS.SignUpUserAsync(tenantUserSignUpDTO);
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
            return await _pubTenantUserDeleteDS.DeleteTenantUser(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #region Update

        [HttpPut]
        [Route("update/{appid:Guid}")]
        public async Task<UpdateTenantUserResponseDTO> UpdateTenantUserAsync([FromBody]TenantUserDTO tenantUserDTO, [FromRoute] Guid appId) {

            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await _pubTenantUserUpdateDS.UpdateTenantUserAsync(tenantUserDTO, appId);
            // send signalR when user permission change and user staus change
            if(updateTenantUserResponseDTO.PermissionsChanged || updateTenantUserResponseDTO.StatusChanged) {

                  MessagePayload payload = new MessagePayload(PublisherLiveUpdateConstants.UserPermissionChangeEvent, tenantUserDTO.TenantUserId);
                string groupName = "";
                await _realtimeUpdate.SendMessageAsync(PublisherLiveUpdateConstants.PublisherLiveUpdateHandler, payload, groupName);

            }

            return updateTenantUserResponseDTO;
        }

        [HttpPut]
        [Route("reinvite/{tenantuserid:Guid}/{appid:Guid}")]
        public async Task<ResponseModelDTO> ReInviteUser([FromRoute] Guid tenantUserId, [FromRoute] Guid appId) {
            await _pubTenantUserDS.ReInviteTenantUserAsync(tenantUserId, appId);
            return new ResponseModelDTO() {
                Id = tenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

        [HttpPut]
        [Route("cancelinvite")]
        public async Task<ResponseModelDTO> CancelInviteForTenantUser([FromBody] TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            await _pubTenantUserDS.CancelTenantUserInvitation(tenantUserIdentificationDTO);
            return new ResponseModelDTO() {
                Id = tenantUserIdentificationDTO.TenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

        [HttpPut]
        [Route("forgot/password")]
        public async Task<ResponseModelDTO> ForgotPassword([FromBody]ForgotPasswordDTO forgotPasswordDTO) {
            await _pubTenantUserDS.ForgotPasswordAsync(forgotPasswordDTO);
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
            await _pubTenantUserDS.UpdateTenantUserProfile(dto);
            return new ResponseModelDTO() {
                Id = dto.ID,
                IsSuccess = true,
                Message = "Tenant User profile updated sucessfully"
            };
        }

        #endregion Update

    }
}