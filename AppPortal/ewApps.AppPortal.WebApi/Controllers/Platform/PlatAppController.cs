/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {


    [Route("api/app")]
    [ApiController]
    public class PlatAppController:ControllerBase {


        #region Local member

        IPlatformAppDS _platformAppDS;

        #endregion

        #region Constructor

        public PlatAppController(IPlatformAppDS platformAppDS) {
            _platformAppDS = platformAppDS;
        }

        #endregion


        #region Get

        /// <summary>
        /// Get all the apps with service count.
        /// </summary>
        /// <returns></returns>
       [Authorize]
        [HttpGet]
        public async Task<List<AppDetailDTO>> GetAppDetailsAsync() {
            return await _platformAppDS.GetAppDetailsAsync();
        }

        /// <summary>
        /// Get application detail based on app ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getappdetailsbyappId")]
        public async Task<AppDetailDTO> GetAppDetailsByAppIdAsync(Guid AppId) {
            return await _platformAppDS.GetAppDetailsByAppIdAsync(AppId);
        }


        /// <summary>
        /// Get app details and services for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("appdetails/{appId}")]
        public async Task<AppAndServiceDTO> GetAppDetailsWithServicesAsync([FromRoute] Guid appId) {
            return await _platformAppDS.GetAppDetailsWithServicesAsync(appId);
        }

        /// <summary>
        /// Get all the publisher for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("publishername/{appId}")]
        public async Task<IEnumerable<StringDTO>> GetPublisherNameAsync([FromRoute] Guid appId) {
            return  await _platformAppDS.GetPublisherListByAppIdAsync(appId);
        }

        /// <summary>
        /// Get all the services for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("services/{appId}")]
        public async Task<List<AppServiceDTO>> GetAppServiceNameAsync([FromRoute] Guid appId) {
            return await _platformAppDS.GetAppServiceNameAsync(appId);
        }

        /// <summary>
        /// Get all the business for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessname/{appId}")]
        public async Task<List<string>> GetBusinessNameByAppIdAsync([FromRoute] Guid appId) {
            return await _platformAppDS.GetBusinessNameByAppIdAsync(appId);
        }

        #endregion

        #region Update

        //ToDo: DBContext2
        /// <summary>
        /// Update the app details and its thumbnail.
        /// </summary>
        /// <param name="appAndServiceDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<ResponseModelDTO> UpdateAppAsync([FromBody] AppAndServiceDTO appAndServiceDTO) {
            await _platformAppDS.UpdateAppAsync(appAndServiceDTO);
            return new ResponseModelDTO() {
                Id = appAndServiceDTO.AppDetailDTO.ID,
                IsSuccess = true,
                Message = "App updated sucessfully"
            };
        }

        #endregion Update
    }
}
