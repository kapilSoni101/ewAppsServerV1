/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 12 Aug 2019

 */

using System;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.ApiService.Controllers {

    /// <summary>
    /// Authenticate Controller expose all authenticate related api's.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController:ControllerBase {

        #region Local Member

        IAuthenticationDS _authenticationDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticateDS">The authenticate ds.</param>
        public AuthenticationController(IAuthenticationDS authenticateDS) {
            _authenticationDS = authenticateDS;
        }

        #endregion Constructor

        #region Get
        /// <summary>
        /// Get logo url and name of tenant
        ///  /// check tenant is active or not and tenant is active, deleted
        /// </summary>
        /// <returns></returns>  
        [Route("validsubdomain")]
        [HttpGet]
        public async Task<LoginBrandingDTO> ValidateSubdomainAsync([FromQuery] string sdomain, [FromQuery] string pkey, [FromQuery] int uType) {
            return await _authenticationDS.ValidateSubdomainAsync(sdomain, pkey, uType);
        }

        /// <summary>
        /// Get user token details
        /// </summary>
        /// <returns></returns>
        [Route("tokeninfo/subdomain")]
        [HttpGet]
        public async Task<TokenDataDTO> GetTokenInfoBySubdomainAsync([FromQuery]string sdomain, [FromQuery] string pkey, [FromQuery] string clientId, [FromQuery] int uType) {
            return await _authenticationDS.GetTokenInfoBySubdomainAsync(sdomain, pkey, clientId, uType);
        }

        /// <summary>
        /// Get user token details
        /// </summary>
        /// <returns></returns>
        [Route("tokeninfo/{tokenId}/{pKey}/{appKey}/{userType}")]
        [HttpGet]
        public async Task<TokenDataDTO> GetTokenInfoAsync([FromRoute]Guid tokenId, [FromRoute] string pKey, [FromRoute] string clientId, [FromRoute] int userType) {
            return await _authenticationDS.GetTokenInfoByTokenIdAsync(tokenId, pKey, clientId, userType);
        }

        /// <summary>
        /// Get user token details for change password api
        /// </summary>
        /// <returns></returns>
        [Route("tokeninfochangepass/{subDomain}/{pKey}/{clientId}/{userType}")]
        [HttpGet]
        public async Task<TokenDataDTO> GetTokenInfoBySubdomainFromRouteAsync([FromRoute] string subDomain, [FromRoute]string pKey, [FromRoute] string clientId, [FromRoute] int userType) {
            return await _authenticationDS.GetTokenInfoBySubdomainAsync(subDomain, pKey, clientId, userType);
        }

        [HttpGet]
        [Route("check/token/{tokenid}")]
        public async Task<bool> CheckToken([FromRoute]Guid tokenid) {
            return await _authenticationDS.CheckTokenAsync(tokenid);
        }

        #endregion Get

        #region Delete

        /// <summary>Delete token</summary>
        [HttpPost]
        [Route("delete/token")]
        public async Task DeleteToken([FromBody]TokenInfoDTO tokenInfoDTO) {
            await _authenticationDS.DeleteTokenInfoAsync(tokenInfoDTO);
        }

        #endregion Delete
    }
}
