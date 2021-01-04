/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 August 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Business class contains all add/update/delete/get methods for Business.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PubBusinessController:ControllerBase {

        #region Local variables

        IBusinessDS _businessDS;
        IBusinessAndUserDS _businessUserDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize constructor for Business Tenant.
        /// </summary>
        /// <param name="businessDS">Business data service.</param>
        public PubBusinessController(IBusinessDS businessDS, IBusinessAndUserDS businessUserDS) {
            _businessDS = businessDS;
            _businessUserDS = businessUserDS;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get filter business list by login tenant.
        /// </summary>
        /// <returns>return login tenant business list.</returns>
        [Route("list")]
        [HttpPut]
        public async Task<List<BusinessViewModelDQ>> GetBusinessListAsync([FromBody]ListDateFilterDTO filterDto, CancellationToken token = default(CancellationToken)) {
            return await _businessDS.GetBusinessListAsync(filterDto, token);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="id">Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<UpdateTenantModelDQ> GetBusinessDetailAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            return await _businessUserDS.GetBusinessUpdateModelAsync(id, token);
        }

        #endregion Get

        #region POST

        /// <summary>
        /// Method is used to add business with all child entities.
        /// </summary>
        /// <param name="dto">Business registration model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AddBusiness")]
        [HttpPost]
        public async Task<ResponseModelDTO> AddBusiness(BusinessRegistrationDTO dto, CancellationToken token = default(CancellationToken)) {
            return await _businessUserDS.RegisterBusinessAsync(dto, token);
        }

        /// <summary>
        /// Method is used to add business with all child entities.
        /// </summary>
        /// <param name="dto">Business registration model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AddAuthBusiness")]
        [HttpPost]
        public async Task<ResponseModelDTO> AddAuthBusiness([FromBody]BusinessRegistrationDTO dto, CancellationToken token = default(CancellationToken)) {
            return await _businessUserDS.RegisterBusinessAsync(dto, token);
        }

        /// <summary>
        /// Get vendor list for publisher.
        /// </summary>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public async Task<BusinessResponseModelDTO> UpdateAsync([FromBody]UpdateTenantModelDQ dto, CancellationToken token = default(CancellationToken)) {
            return await _businessUserDS.UpdateBusinessTenantAsync(dto, token);
        }

        #endregion POST

    }
}
