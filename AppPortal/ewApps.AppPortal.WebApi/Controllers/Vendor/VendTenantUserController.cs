/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:  Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 10 February 2020
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Vend Tenant User Controller class contains all add/update/delete/get methods for Tenant User On Vendor.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VendTenantUserController:ControllerBase {


        #region Local Variables

        IVendTenantUserDS _vendTenantUserDS;
        
        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initilize local variables. 
        /// </summary>
        public VendTenantUserController(IVendTenantUserDS vendTenantUserDS) {
            _vendTenantUserDS = vendTenantUserDS;
           
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get User Detail By User Id.
        /// </summary>
        /// <returns></returns>
        [Route("getuserinfo/{id}")]
        [HttpGet]
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync([FromRoute]Guid id) {
            return await _vendTenantUserDS.GetUserInfoByIdAsync(id);
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
            await _vendTenantUserDS.UpdateTenantUserProfile(dto);
            return new ResponseModelDTO() {
                Id = dto.ID,
                IsSuccess = true,
                Message = "Tenant User profile updated sucessfully"
            };
        }
    #endregion

    #region VendorSetUP App 

    #region Get

    [HttpGet]
    [Route("vendsetuplist/{businesspartnertenantid:Guid}/{deleted}")]
    public async Task<List<TenantUserSetupListDTO>> GetAllVendSetupVendorUsersAsync([FromRoute]Guid businessPartnerTenantId, [FromRoute]bool deleted) {
      return await _vendTenantUserDS.GetAllVendSetupVendorUsersAsync(businessPartnerTenantId, deleted);
    }

    [HttpGet]
    [Route("detail/{tenantuserid:Guid}/{businesspartnertenantid:Guid}/{deleted}")]
    public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails([FromRoute] Guid tenantuserid, [FromRoute] Guid businesspartnertenantid, [FromRoute] bool deleted) {
      return await _vendTenantUserDS.GetTenantUserAndAppDetails(tenantuserid, businesspartnertenantid, deleted);
    }

    #endregion Get

    [HttpPut]
    [Route("forgot/password")]
    public async Task<ResponseModelDTO> ForgotPassword([FromBody]ForgotPasswordDTO forgotPasswordDTO)
    {
      await _vendTenantUserDS.ForgotPasswordAsync(forgotPasswordDTO);
      return new ResponseModelDTO
      {
        IsSuccess = true,
        Message = "Forgot Password mail sent successfully"
      };
    }

    #endregion VendorSetUP App  

  }
}