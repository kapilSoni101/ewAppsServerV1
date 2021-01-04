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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi.Controllers {


  [Route("api/[controller]")]
  [ApiController]
  public class AppServiceController:ControllerBase {

    IAppServiceDS _appServiceDS;

    public AppServiceController(IAppServiceDS appServiceDS) {
      _appServiceDS = appServiceDS;
    }

    #region GetMethods

    [HttpGet]
    [Route("appserviceswithattribute/{appid}")]
    public async Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
      return await _appServiceDS.GetAppServicesDetailWithAttributesAsync(appId, token);
    }

        #endregion GetMethods

        //[HttpPut]
        //[Route("getappinfo")]
        //public async Task<List<AppInfoDTO>> GetAppInfoListByAppIdListAsync([FromBody]List<Guid> appIdList) {
        //    return await _appDS.GetAppInfoListByAppIdListAsync(appIdList);
        //}

        //[HttpPut]
        //[Route("getappinfobykey")]
        //public async Task<List<AppInfoDTO>> GetAppInfoListByAppKeyListAsync([FromBody]List<string> appKeyList) {
        //    return await _appDS.GetAppInfoListByAppKeyListAsync(appKeyList);
        //}

        //#region Update

        ///// <summary>
        ///// Update the app details and its thumbnail.
        ///// </summary>
        ///// <param name="appAndServiceDTO"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("update")]
        //public async Task<ResponseModelDTO> UpdateAppAndServiceAsync([FromBody] AppAndServiceDTO appAndServiceDTO) {
        //    await _appDS.UpdateAppAndServiceAsync(appAndServiceDTO);
        //    return new ResponseModelDTO() {
        //        Id = appAndServiceDTO.AppDetailDTO.ID,
        //        IsSuccess = true,
        //        Message = "App updated sucessfully"
        //    };
        //}


        //#endregion Update

        /// <summary>
        /// Update the app details and its thumbnail.
        /// </summary>
        /// <param name="appAndServiceDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateappservice/{appId}/{tenantId}")]
        public async Task<ResponseModelDTO> UpdateBusinessAppServiceInfoAsync([FromBody] List<PayAppServiceDetailDTO> updatedAppServiceList,[FromRoute] Guid appId, [FromRoute]Guid tenantId) {
            await _appServiceDS.UpdateBusinessAppServiceInfoAsync(updatedAppServiceList, appId,tenantId);
            return new ResponseModelDTO() {
                Id = updatedAppServiceList[0].AppServiceAttributeList[0].AppServiceAccountList[0].ID,
                IsSuccess = true,
                Message = "AccountDetail updated sucessfully"
            };
        }

    }
}