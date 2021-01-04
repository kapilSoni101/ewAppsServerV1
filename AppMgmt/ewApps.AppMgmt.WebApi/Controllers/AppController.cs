/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 12 Aug 2019

 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class AppController:ControllerBase {

        IAppDS _appDS;

        public AppController(IAppDS appDS) {
            _appDS = appDS;
        }

        [HttpPut]
        [Route("getappinfo")]
        public async Task<List<AppInfoDTO>> GetAppInfoListByAppIdListAsync([FromBody]List<Guid> appIdList) {
            return await _appDS.GetAppInfoListByAppIdListAsync(appIdList);
        }

        [HttpPut]
        [Route("getappinfobykey")]
        public async Task<List<AppInfoDTO>> GetAppInfoListByAppKeyListAsync([FromBody]List<string> appKeyList) {
            return await _appDS.GetAppInfoListByAppKeyListAsync(appKeyList);
        }
        #region Get AppDetail, AppService, BusinessName For Publisher

        [HttpGet]
        [Route("appdetails/{appId}")]
        public async Task<AppAndServiceDTO> GetAppAndServiceDetailsByAppIdAsync([FromRoute] Guid appId) {
            return await _appDS.GetAppAndServiceDetailsByAppIdAsync(appId);
        }

        [HttpGet]
        [Route("servicename/{appId}")]
        public async Task<List<AppServiceDTO>> GetServiceNameByAppIdAsync([FromRoute] Guid appId) {
            return await _appDS.GetServiceNameByAppIdAsync(appId);
        }

        #endregion Get AppService, BusinessName For Publisher

        /// <summary>
        /// Gets the application list that matches input parameters.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active] only active applications will fetch.</param>
        /// <param name="subscriptionMode">The subscription mode to filter applications.</param>
        /// <returns>Returns application list that matches given input parameters.</returns>
        [HttpGet]
        [Route("list/{active}/{subscriptionMode}")]
        public async Task<List<AppInfoDTO>> GetApplicationListAsync([FromRoute] bool active, [FromRoute] AppSubscriptionModeEnum subscriptionMode) {
            return await _appDS.GetApplicationListAsync(active, subscriptionMode);
        }

        #region Get AppService, BusinessName For Publisher

        [HttpPut]
        [Route("servicelist")]
        public async Task<List<AppServiceDTO>> GetAppServiceAsync([FromBody] List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO) {
            return await _appDS.GetAppServiceAsync(pubBusinessSubsPlanAppServiceDTO);

        }

        [HttpPut]
        [Route("serviceattributelist")]
        public async Task<List<AppServiceDTO>> GetAppServiceAttributeListAsync([FromBody] List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO) {
            return await _appDS.GetAppServiceAttributeListAsync(pubBusinessSubsPlanAppServiceDTO);

        }

        /// <summary>
        /// Get all the business for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessname/{appId}/{publisherTenantId}")]
        public async Task<List<string>> GetBusinessNameByAppIdAsync([FromRoute] Guid appId, [FromRoute] Guid publisherTenantId) {
            return await _appDS.GetBusinessNameByAppIdAsync(appId, publisherTenantId);
        }

        /// <summary>
        /// Get all the business for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessname/{appId}")]
        public async Task<List<string>> GetBusinessNameByAppIdPlatAsync([FromRoute] Guid appId) {
            return await _appDS.GetBusinessNameByAppIdPlatAsync(appId);
        }
        #endregion Get AppService, BusinessName For Publisher

        #region Update

        /// <summary>
        /// Update the app details and its thumbnail.
        /// </summary>
        /// <param name="appAndServiceDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<ResponseModelDTO> UpdateAppAndServiceAsync([FromBody] AppAndServiceDTO appAndServiceDTO) {
            await _appDS.UpdateAppAndServiceAsync(appAndServiceDTO);
            return new ResponseModelDTO() {
                Id = appAndServiceDTO.AppDetailDTO.ID,
                IsSuccess = true,
                Message = "App updated sucessfully"
            };
        }


        #endregion Update

    }
}