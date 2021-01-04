/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {

    /// <summary>
    /// Conatins method to add/update business tenant and supported data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController:ControllerBase {

        #region Local variables

        IUserSessionManager _userSessionManager;
        ITenantUserDS _tenantUserDS;
        ITenantSignUpForBusinessDS _tenantSignUpForBusinessDS;
        ITenantSignUpForPublisherDS _tenantForPublisherDS;
        ITenantSignUpForCustomerDS _customerSignUpDS;
        ITenantForBusinessDS _tenantForBusinessDS;
        ITenantUpdateForBusinessDS _tenantUpdateForBusinessDS;
        ITenantDS _tenantDS;
        ITenantLinkingDS _tenantLinkingDS;
        ITenantUpdateForPublisherDS _tenantUpdateForPublisherDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="tenantBusinessDS">A tenant business wrapper class.</param>
        /// <param name="tenantUserDS">Tenant user service class.</param>
        /// <param name="customerSignUpDS"></param>
        /// <param name="singUpTenantAndTenantUserDS"></param>
        /// <param name="tenantForBusinessDS"></param>
        /// <param name="tenantUpdateForBusinessDS"></param>
        /// <param name="userSessionManager">User session manager.</param>
        public TenantController(ITenantUpdateForPublisherDS tenantUpdateForPublisherDS, ITenantSignUpForPublisherDS singUpTenantAndTenantUserDS, ITenantSignUpForCustomerDS customerSignUpDS, ITenantSignUpForBusinessDS tenantBusinessDS, ITenantUserDS tenantUserDS,
                ITenantForBusinessDS tenantForBusinessDS,
                ITenantUpdateForBusinessDS tenantUpdateForBusinessDS,
                IUserSessionManager userSessionManager,
                 ITenantDS tenantDS, ITenantLinkingDS tenantLinkingDS) {
            _tenantSignUpForBusinessDS = tenantBusinessDS;
            _tenantUserDS = tenantUserDS;
            _tenantForBusinessDS = tenantForBusinessDS;
            _tenantUpdateForBusinessDS = tenantUpdateForBusinessDS;
            _userSessionManager = userSessionManager;
            _tenantForPublisherDS = singUpTenantAndTenantUserDS;
            _customerSignUpDS = customerSignUpDS;
            _tenantDS = tenantDS;
            _tenantLinkingDS = tenantLinkingDS;
            _tenantUpdateForPublisherDS = tenantUpdateForPublisherDS;
        }

        #endregion Constructor

        #region Publisher
        [HttpPut]
        [Route("publishersignup/validate")]
        public async Task<PublisherPreSignUpRespDTO> ValidatePublisherPreSignUpRequestAsync(PublisherPreSignUpReqDTO publisherPreSignUpReqDTO) {
            return await _tenantForPublisherDS.ValidatePublisherPreSignUpRequest(publisherPreSignUpReqDTO);
        }

        [HttpPut]
        [Route("publisherupdate/predata")]
        public async Task<PublisherPreUpdateRespDTO> GetPublisherPreUpdateRequestDataAsync(PublisherPreUpdateReqDTO publisherPreUpdateReqDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _tenantForPublisherDS.GetPublisherPreUpdateRequestDataAsync(publisherPreUpdateReqDTO, cancellationToken);
        }


        #region Add/Update Publisher 

        [HttpPost]
        [Route("publishersignup")]
        public async Task<TenantSignUpResponseDTO> PublisherSignUpAsync([FromBody] PublisherSignUpDTO publisherSignUpDTO) {
            return await _tenantForPublisherDS.PublisherSignUpAsync(publisherSignUpDTO);
        }

        [HttpPut]
        [Route("publisherupdate")]
        public async Task<bool> PublisherUpdateAsync([FromBody] TenantUpdateForPublisherDTO tenantUpdateForPublisherDTO) {
            await _tenantUpdateForPublisherDS.UpdatePublisherTenantAsync(tenantUpdateForPublisherDTO);
            return true;
        }

        #endregion Add/Update Publisher  

        #endregion

        #region Add/Update Business

        /// <summary>
        /// Add tenant and its supported entities.
        /// </summary>
        /// <param name="reqDto">Publisher get request model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("businesssignup")]
        public async Task<TenantSignUpForBusinessResDTO> BusinessSignupAsync([FromBody] BusinessSignUpDTO reqDto, CancellationToken token = default(CancellationToken)) {
            TenantSignUpForBusinessResDTO dto = await _tenantSignUpForBusinessDS.BusinessSignupAsync(reqDto, token);
            return dto;
        }

        /// <summary>
        /// Add tenant and its supported entities.
        /// </summary>
        /// <param name="reqDto">Publisher get request model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("updatebusiness")]
        //ToDo: url is 'businessupdate'
        public async Task<BusinessResponseModelDTO> UpdateBusinessAsync([FromBody] UpdateTenantModelDTO reqDto, CancellationToken token = default(CancellationToken)) {
            BusinessResponseModelDTO dto = await _tenantUpdateForBusinessDS.UpdateBusinessTenantAsync(reqDto, token);
            return dto;
        }

        #endregion Add/Update Business

        #region Add/Update Customer Tenant


        /// <summary>
        /// Tenants the sign up for customer asynchronous.
        /// </summary>
        /// <param name="reqDto">The req dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("customersignup")]
        public async Task<bool> TenantSignUpForCustomerAsync([FromBody] List<TenantSignUpForCustomerReqDTO> reqDto, CancellationToken token = default(CancellationToken)) {
            TenantSignUpForCustomerResDTO result = await _customerSignUpDS.TenantSignUpForCustomerAsync(reqDto, token);
            return true;
        }

        #endregion Add/Update Customer Tenant     

        #region Get

        /// <summary>
        /// Get tenant model update model creatyed for business.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="token"></param>
        /// <returns>return tenant and related entity object for business.</returns>
        [HttpGet]
        [Route("gettenantforbusiness/{tenantId}")]
        public async Task<UpdateTenantModelDQ> GetBusinessUpdateModelAsync([FromRoute]Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantForBusinessDS.GetBusinessUpdateModelAsync(tenantId, token);
        }

        /// <summary>
        /// Get tenant by TenantId.
        /// </summary>
        /// <param name = "tenantId" > Tenant Id</param>
        /// <returns>return tenant and related entity </returns>
        [HttpGet]
        [Route("gettenantinfo/{tenantId}")]
        public async Task<TenantInfoDTO> GetTenantByIdAsync([FromRoute]Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantDS.GetTenantByIdAsync(tenantId, token);
        }

        /// <summary>
        /// Get tenant by TenantId .
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>        
        /// <returns>return tenant and related entity </returns>
        [HttpGet]
        [Route("gettenantlinkinginfo/{tenantId}/{tenantType}")]
        public async Task<TenantInfoDTO> GetTenantLinkingByTenantIdAndTypeAsync([FromRoute]Guid tenantId, [FromRoute]TenantType tenantType, CancellationToken token = default(CancellationToken)) {
            return await _tenantLinkingDS.GetTenantByTenantIdAndTypeAsync(tenantId, tenantType, token);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// Delete busines tenant.
        /// </summary>
        /// <param name="tenantId">Tenantid of business.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletebusiness/{tenantId}")]        
        public async Task DeleteBusinessAsync([FromRoute]Guid tenantId, CancellationToken token = default(CancellationToken)) {
            await _tenantForBusinessDS.DeleteBusinessTenantAsync(tenantId, token);
        }

        #endregion Delete

    }
}
