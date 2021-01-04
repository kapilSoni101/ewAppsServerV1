/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 12 Aug 2019

 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.ApiService.Controllers {

    /// <summary>
    /// Authenticate Controller expose all authenticate related api's.
    /// </summary>
    [Route("api/appmgmtauth")]
    [ApiController]
    public class AuthenticationController:ControllerBase {

        #region Local Member

        ITenantDS _tenantDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="tenantDS">The authenticate ds.</param>
        public AuthenticationController(ITenantDS tenantDS) {
            _tenantDS = tenantDS;
        }

        #endregion Constructor

        #region Get      

        /// <summary>
        /// Get tenant info details by tenantid
        /// </summary>
        /// <returns></returns>
        [Route("tenantinfotenantId/{tenantId}/{userType}")]
        [HttpGet]
        public async Task<TenantInfoDTO> GetTenantInfoByTenantIdAsync([FromRoute]Guid tenantId, [FromRoute]int userType) {
            return await _tenantDS.GetTenantInfoByTenantIdAsync(tenantId, userType);
        }

        /// <summary>
        /// Get tenant info details by subdomain
        /// </summary>
        /// <returns></returns>
        [Route("tenantinfo/{subDomain}/{userType}")]
        [HttpGet]
        public async Task<TenantInfoDTO> GetTenantInfoBySubdomainAsync([FromRoute]string subDomain, [FromRoute] int userType) {
            return await _tenantDS.GetTenantInfoBySubdomainAsync(subDomain, userType);
        }

        #endregion Get
    }
}
