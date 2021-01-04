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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class TenantUserController:ControllerBase {

        #region Local Member

        #region Platform
        ITenantUserSignUpForPlatformDS _tenantUserSignUpForPlatformDS;
        ITenantUserForPlatformDS _tenantUserForPlatformDS;
        ITenantUserUpdateForPlatformDS _tenantUserUpdateForPlatformDS;
        ITenantUserDeleteForPlatformDS _tenantUserDeleteForPlatformDS;
        #endregion Platform
        #region Publisher
        ITenantUserForPublisherDS _tenantUserForPublisher;
        ITenantUserSignUpForPublisherDS _tenantUserSignUpForPublisherDS;
        ITenantUserUpdateForPublisherDS _tenantUserUpdateForPublisherDS;
        ITenantUserDeleteForPublisherDS _tenantUserDeleteForPublisherDS;
        #endregion Publisher
        #region Business
        ITenantUserSignUpForBusiness _tenantUserSignUpForBusiness;
        ITenantUserUpdateForBusinessDS _tenantUserUpdateForBusinessDS;
        ITenantUserDeleteForBusinessDS _tenantUserDeleteForBusinessDS;
        #endregion Business
        #region customer
        ITenantUserSignUpForCustomer _tenantUserSignUpForCustomer;
        ITenantUpdateForCustomerDS _tenantUpdateForCustomerDS;
        ITenantUserDeleteForCustomerDS _tenantUserDeleteForCustomerDS;
        ITenantUserUpdateForCustomerDS _tenantUserUpdateForCustomerDS;
        #endregion customer
        ITenantUserExtDS _tenantUserExtDS;

    #region customer   
    ITenantUpdateForVendorDS _tenantUpdateForVendorDS;   
    #endregion customer

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initialize user realted data services.
    /// </summary>
    public TenantUserController(ITenantUserSignUpForPlatformDS tenantUserSignUpForPlatformDS, ITenantUserUpdateForPlatformDS tenantUserUpdateForPlatformDS,
             ITenantUserForPlatformDS tenantUserForPlatformDS, ITenantUserForPublisherDS tenantUserForPublisher, ITenantUserSignUpForPublisherDS tenantUserSignUpForPublisherDS,
             ITenantUserDeleteForPlatformDS tenantUserDeleteForPlatformDS, ITenantUserSignUpForBusiness tenantUserSignUpForBusiness, ITenantUserUpdateForPublisherDS tenantUserUpdateForPublisherDS,
             ITenantUserDeleteForPublisherDS tenantUserDeleteForPublisherDS, ITenantUpdateForCustomerDS tenantUpdateForCustomerDS, ITenantUserUpdateForBusinessDS tenantUserUpdateForBusinessDS,
             ITenantUserExtDS tenantUserExtDS, ITenantUserSignUpForCustomer tenantUserSignUpForCustomer, ITenantUserDeleteForBusinessDS tenantUserDeleteForBusinessDS,
             ITenantUserDeleteForCustomerDS tenantUserDeleteForCustomerDS, ITenantUserUpdateForCustomerDS tenantUserUpdateForCustomerDS, ITenantUpdateForVendorDS tenantUpdateForVendorDS) {
            _tenantUserSignUpForPlatformDS = tenantUserSignUpForPlatformDS;
            _tenantUserForPlatformDS = tenantUserForPlatformDS;
            _tenantUserForPublisher = tenantUserForPublisher;
            _tenantUserUpdateForPlatformDS = tenantUserUpdateForPlatformDS;
            _tenantUserDeleteForPlatformDS = tenantUserDeleteForPlatformDS;
            _tenantUserSignUpForBusiness = tenantUserSignUpForBusiness;
            _tenantUserSignUpForPublisherDS = tenantUserSignUpForPublisherDS;
            _tenantUserUpdateForPublisherDS = tenantUserUpdateForPublisherDS;
            _tenantUserDeleteForPublisherDS = tenantUserDeleteForPublisherDS;
            _tenantUpdateForCustomerDS = tenantUpdateForCustomerDS;
            _tenantUserUpdateForBusinessDS = tenantUserUpdateForBusinessDS;
            _tenantUserExtDS = tenantUserExtDS;
            _tenantUserSignUpForCustomer = tenantUserSignUpForCustomer;
            _tenantUserDeleteForBusinessDS = tenantUserDeleteForBusinessDS;
            _tenantUserDeleteForCustomerDS = tenantUserDeleteForCustomerDS;
            _tenantUserUpdateForCustomerDS = tenantUserUpdateForCustomerDS;
            _tenantUpdateForVendorDS = tenantUpdateForVendorDS;
        }

        #endregion Constructor

        #region Platform 

        #region Add

        /// <summary>
        /// Add platform user.
        /// </summary>
        /// <param name="tenantUserSignUpDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addplatformuser")]
        public async Task<TenantUserSignUpResponseDTO> AddPlatformUser(TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _tenantUserSignUpForPlatformDS.SignUpUserAsync(tenantUserSignUpDTO);
        }

        #endregion Add

        #region Update

        [HttpPut]
        [Route("updateplatformuser/{tenantid:Guid}/{appid:Guid}")]
        public async Task<UpdateTenantUserResponseDTO> UpdatePlatformTenantUser([FromBody]TenantUserDTO tenantUserDTO, [FromRoute] Guid tenantid, [FromRoute] Guid appId) {
            return await _tenantUserUpdateForPlatformDS.UpdateUserAsync(tenantUserDTO, tenantid, appId);
        }

        #endregion Update

        #region Delete

        [HttpPut]
        [Route("deleteplatformuser")]
        public async Task<DeleteUserResponseDTO> DeletePlatformUser([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            return await _tenantUserDeleteForPlatformDS.DeleteTenantUserAsync(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #endregion Platform

        #region Publisher

        #region Get

        [AllowAnonymous]
        /// <summary>
        /// Get publisher primary user, and also return userid by email.
        /// </summary>
        /// <param name="reqDto">Publisher get request model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("getpublisherprimaryuser")]
        public async Task<PublisherTenantInfoDTO> GetPublisherAndUserAsync([FromBody] PublisherRequestDTO reqDto, CancellationToken token = default(CancellationToken)) {
            return await _tenantUserForPublisher.GetPublisherAndUserAsync(reqDto, token);
        }

        #endregion Get

        #region Add

        /// <summary>
        /// Add platform user.
        /// </summary>
        /// <param name="tenantUserSignUpDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addpublisheruser")]
        public async Task<TenantUserSignUpResponseDTO> AddPublisherUser(TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _tenantUserSignUpForPublisherDS.SignUpUserAsync(tenantUserSignUpDTO);
        }

        #endregion Add

        #region Update

        [HttpPut]
        [Route("updatepublisheruser/{tenantid:Guid}/{appid:Guid}")]
        public async Task<UpdateTenantUserResponseDTO> UpdatePublisherTenantUser([FromBody]TenantUserDTO tenantUserDTO, [FromRoute] Guid tenantid, [FromRoute] Guid appId) {
            return await _tenantUserUpdateForPublisherDS.UpdateUserAsync(tenantUserDTO, tenantid, appId);
        }

        #endregion Update

        #region Delete

        [HttpPut]
        [Route("deletepublisheruser")]
        public async Task<DeleteUserResponseDTO> DeletePublisherUser([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            return await _tenantUserDeleteForPublisherDS.DeleteTenantUserAsync(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #endregion Publisher

        #region Business

        #region Add

        /// <summary>
        /// Add Busienss user.
        /// </summary>
        /// <param name="tenantUserSignUpDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addbusinessuser")]
        public async Task<TenantUserSignUpResponseDTO> AddBusinessUser(TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _tenantUserSignUpForBusiness.SignUpUserAsync(tenantUserSignUpDTO);
        }

        /// <summary>
        /// Add Busienss user.
        /// </summary>
        /// <param name="tenantUserSignUpDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addcustomeruser")]
        public async Task<TenantUserSignUpResponseDTO> AddCustomerUSer([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _tenantUpdateForCustomerDS.SignUpCustomerUserAsync(tenantUserSignUpDTO);
        }

    /// <summary>
    /// Add Vendor user.
    /// </summary>
    /// <param name="tenantUserSignUpDTO"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("addvendoruser")]
    public async Task<TenantUserSignUpResponseDTO> AddVendorUser(TenantUserSignUpDTO tenantUserSignUpDTO) {
      return await _tenantUpdateForVendorDS.SignUpVendorUserAsync(tenantUserSignUpDTO);
    }

    #endregion Add

    #region  Update

    [HttpPost]
        [Route("updatecustomeruser")]
        public async Task<TenantUserSignUpResponseDTO> UpdateCustomerTenantUser([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _tenantUpdateForCustomerDS.UpdateCustomerTenantUser(tenantUserSignUpDTO);
        }

    [HttpPost]
    [Route("updatevendoruser")]
    public async Task<TenantUserSignUpResponseDTO> UpdateVendorTenantUser([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
      return await _tenantUpdateForVendorDS.UpdateVendorTenantUser(tenantUserSignUpDTO);
    }

    [HttpPut]
        [Route("updatebusinessuser")]
        public async Task<UpdateTenantUserResponseDTO> UpdateBusinessTenantUser([FromBody]TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO) {
            return await _tenantUserUpdateForBusinessDS.UpdateUserAsync(tenantUserUpdateRequestDTO);
        }

        #endregion  Update

        #region Delete

        [HttpPut]
        [Route("deletebusinessuser")]
        public async Task<DeleteUserResponseDTO> DeleteBusinessUser([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            return await _tenantUserDeleteForBusinessDS.DeleteTenantUserAsync(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #endregion Business

        #region Customer

        #region Add

        /// <summary>
        /// Add Busienss user.
        /// </summary>
        /// <param name="tenantUserSignUpDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("custsetupaddcustomeruser")]
        public async Task<TenantUserSignUpResponseDTO> AddCustomerUserFromCustomerSetup([FromBody]TenantUserSignUpDTO tenantUserSignUpDTO) {
            return await _tenantUserSignUpForCustomer.SignUpUserAsync(tenantUserSignUpDTO);
        }

        #endregion Add

        #region Update

        [HttpPut]
        [Route("updatecustomersetupuser")]
        public async Task<UpdateTenantUserResponseDTO> UpdateCustomerSetupTenantUser([FromBody]TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO) {
            return await _tenantUserUpdateForCustomerDS.UpdateUserAsync(tenantUserUpdateRequestDTO);
        }

        #endregion Update

        #region Delete

        [HttpPut]
        [Route("deletecustomeruser")]
        public async Task<DeleteUserResponseDTO> DeleteCustomerUser([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            return await _tenantUserDeleteForCustomerDS.DeleteTenantUserAsync(tenantUserIdentificationDTO);
        }

        #endregion Delete

        #endregion Customer

        #region Common Method

        #region Get

        /// <summary>
        /// Get User Detail By User Id.
        /// </summary>
        /// <returns></returns>
        [Route("getuserinfo/{id}")]
        [HttpGet]
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync([FromRoute]Guid id) {
            return await _tenantUserExtDS.GetUserInfoByIdAsync(id);
        }

        [HttpGet]
        [Route("info/{tenantuserid:Guid}")]
        public async Task<TenantUserInfoDTO> GetTenanntUserInfoAsync([FromRoute]Guid tenantuserid) {
            return await _tenantUserExtDS.GetTenantUserInfoAsync(tenantuserid);
        }

        [HttpGet]
        [Route("infobyemail/{email}")]
        public async Task<TenantUserInfoDTO> GetTenanntUserInfoAsync([FromRoute]string email) {
            return await _tenantUserExtDS.GetTenantUserInfoByEmailAsync(email);
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
            await _tenantUserExtDS.UpdateAppUser(dto);
            return new ResponseModelDTO() {
                Id = dto.ID,
                IsSuccess = true,
                Message = "Tenant User profile updated sucessfully"
            };
        }

        [HttpPut]
        [Route("updateinvitationstatus/{status}")]
        public async Task CancelTenantUserInvitation([FromBody]TenantUserIdentificationDTO tenantUserIdentificationDTO, [FromRoute]int status) {
            await _tenantUserExtDS.UpdateTenantUserInvitationStatus(tenantUserIdentificationDTO, status);
        }

        #endregion Update

        #endregion Common Method
    }
}
