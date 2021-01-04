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
using ewApps.Platform.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Plat Tenant User Controller class contains all add/update/delete/get methods for Tenant User On platfrom.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatTenantUserController:ControllerBase {

        #region Local variables

        IPlatTenantUserDS _platTenantUserDS;
        IPlatTenantUserSignUpDS _platTenantUserSignUpDS;
        IPlatTenantUserDeleteDS _platTenantUserDeleteDS;
        IPlatTenantUserUpdateDS _platTenantUserUpdateDS;
        IRealTimeUpdate _realtimeUpdate;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables. 
        /// </summary>
        public PlatTenantUserController(IPlatTenantUserDS platTenantUserDS, IPlatTenantUserSignUpDS platTenantUserSignUpDS, IPlatTenantUserUpdateDS platTenantUserUpdateDS,
            IPlatTenantUserDeleteDS platTenantUserDeleteDS, IRealTimeUpdate realtimeUpdate) {
            _platTenantUserDS = platTenantUserDS;
            _platTenantUserSignUpDS = platTenantUserSignUpDS;
            _platTenantUserDeleteDS = platTenantUserDeleteDS;
            _platTenantUserUpdateDS = platTenantUserUpdateDS;
            _realtimeUpdate = realtimeUpdate;
        }

        #endregion Constructor

        #region Add 

        [HttpPost]
        [Route("signupuser")]
        public async Task<TenantUserSignUpResponseDTO> SignUpPlatformTenantUserAsync([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _platTenantUserSignUpDS.SignUpUserAsync(tenantUserSignUpDTO);
        }

        #endregion Add 

        #region Get

        [HttpGet]
        [Route("list/{appid:Guid}/{deleted}")]
        public async Task<List<TenantUserDetailsDTO>> GetAllPlatfromUsersAsync([FromRoute]Guid appId, [FromRoute]bool deleted) {
            return await _platTenantUserDS.GetPlatformTenantUsers(appId, deleted);
        }

        /// <summary>
        /// Get User Detail By User Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getuserinfo/{id}")]
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync([FromRoute]Guid id) {
            return await _platTenantUserDS.GetUserInfoByIdAsync(id);
        }

        [HttpGet]
        [Route("detail/{tenantuserid:Guid}/{deleted}")]
        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails([FromRoute] Guid tenantuserid, [FromRoute] bool deleted) {
            return await _platTenantUserDS.GetTenantUserAndPermissionDetails(tenantuserid, deleted);
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
            await _platTenantUserDS.UpdateTenantUserProfile(dto);
            return new ResponseModelDTO() {
                Id = dto.ID,
                IsSuccess = true,
                Message = "Tenant User profile updated sucessfully"
            };
        }

        [HttpPut]
        [Route("update/{appid:Guid}")]
        public async Task<UpdateTenantUserResponseDTO> UpdateTenantUserAsync([FromBody]TenantUserDTO tenantUserDTO, [FromRoute] Guid appId) {
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await _platTenantUserUpdateDS.UpdateTenantUserAsync(tenantUserDTO, appId);
            // send signalR when user permission change and user staus change
            if(updateTenantUserResponseDTO.PermissionsChanged || updateTenantUserResponseDTO.StatusChanged) {

                MessagePayload payload = new MessagePayload(PlatformLiveUpdateConstants.UserPermissionChangeEvent, tenantUserDTO.TenantUserId);
                string groupName = "";
                await _realtimeUpdate.SendMessageAsync(PlatformLiveUpdateConstants.PlatformLiveUpdateHandler, payload, groupName);

            }

            return updateTenantUserResponseDTO;
        }

        [HttpPut]
        [Route("reinvite/{tenantuserid:Guid}/{appid:Guid}")]
        public async Task<ResponseModelDTO> ReInviteUser([FromRoute] Guid tenantUserId, [FromRoute] Guid appId) {
            await _platTenantUserDS.ReInviteTenantUserAsync(tenantUserId, appId);
            return new ResponseModelDTO() {
                Id = tenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

        [HttpPut]
        [Route("cancelinvite")]
        public async Task<ResponseModelDTO> CancelInviteForTenantUser([FromBody] TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            await _platTenantUserDS.CancelTenantUserInvitation(tenantUserIdentificationDTO);
            return new ResponseModelDTO() {
                Id = tenantUserIdentificationDTO.TenantUserId,
                IsSuccess = true,
                Message = "Tenant User reinvited sucessfully"
            };
        }

        [HttpPut]
        [Route("forgot/password")]
        public async Task<ResponseModelDTO> ForgotPassword([FromBody]ForgotPasswordDTO forgotPasswordDTO) {
            await _platTenantUserDS.ForgotPasswordAsync(forgotPasswordDTO);
            return new ResponseModelDTO {
                IsSuccess = true,
                Message = "Forgot Password mail sent successfully"
            };
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Method fot deleting the platfrom user.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("delete")]
        public async Task<DeleteUserResponseDTO> DeleteTenantUser([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            return await _platTenantUserDeleteDS.DeleteTenantUser(tenantUserIdentificationDTO);
        }

        #endregion Delete
    }
}