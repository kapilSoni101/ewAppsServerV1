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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

  [Route("api/[controller]")]
  [ApiController]
  public class PlatSubscriptionPlanController:ControllerBase {

    #region Local member

    IPlatformSubscriptionPlanDS _platformSubscriptionPlanDS;

    #endregion

    #region Constructor

    public PlatSubscriptionPlanController(IPlatformSubscriptionPlanDS platformSubscriptionPlanDS) {
      _platformSubscriptionPlanDS = platformSubscriptionPlanDS;
    }

    #endregion


    #region Get

    /// <summary>
    /// Get app services with attributes the app by AppId.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("appserviceswithattribute/{appId}")]
    public async Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync([FromRoute] Guid appId, CancellationToken token = default(CancellationToken)) {
      return await _platformSubscriptionPlanDS.GetAppServicesDetailWithAttributesAsync(appId);
    }

    #endregion

    //#region Update

    ////ToDo: DBContext2
    ///// <summary>
    ///// Update the app details and its thumbnail.
    ///// </summary>
    ///// <param name="appAndServiceDTO"></param>
    ///// <returns></returns>
    //[HttpPut]
    //[Route("update")]
    //public async Task<ResponseModelDTO> UpdateAppAsync([FromBody] AppAndServiceDTO appAndServiceDTO) {
    //    await _platformAppDS.UpdateAppAsync(appAndServiceDTO);
    //    return new ResponseModelDTO() {
    //        Id = appAndServiceDTO.AppDetailDTO.ID,
    //        IsSuccess = true,
    //        Message = "App updated sucessfully"
    //    };
    //}

    //#endregion Update


    [HttpPost]
    [Route("subscriptionplanwithattribute")]
    public async Task<ResponseModelDTO> AddSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken())
    {
      Guid planId = new Guid();
      await _platformSubscriptionPlanDS.AddSubscriptionPlanWithServiceAttributeAsync(addUdpateDTO, token);

      return new ResponseModelDTO()
      {
        Id = planId,
        IsSuccess = true,
        Message = ""
      };

    }

    /// <summary>
    /// Adds a subscription with services and their attributes.
    /// </summary>
    /// <param name="addUdpateDTO">add update dto </param>
    /// <param name="token">token</param>
    /// <returns>Success response objedt..</returns>
    [HttpPut]
    [Route("subscriptionplanwithattribute")]
    public async Task<ResponseModelDTO> UpdateSubscriptionPlanWithAttribute(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()){
      await _platformSubscriptionPlanDS.UpdateSubscriptionPlanWithServiceAttributeAsync(addUdpateDTO, token);

      return new ResponseModelDTO()
      {
        Id = new Guid(),
        IsSuccess = true,
        Message = ""
      };

    }

    /// <summary>
    /// Delete a subscription with services and their attributes.
    /// </summary>
    /// <param name="planId">plan id to delete</param>
    /// <param name="token">token</param>
    /// <returns>Success response objedt..</returns>
    [HttpDelete]
    [Route("subscriptionplanwithattribute/{planid}")]
    public async Task<ResponseModelDTO> DeleteSubscriptionPlanWithAttribute(Guid planId, CancellationToken token = new CancellationToken())
    {
      await _platformSubscriptionPlanDS.DeleteSubscriptionPlanWithServiceAttributeAsync(planId, token);

      return new ResponseModelDTO()
      {
        Id = new Guid(),
        IsSuccess = true,
        Message = ""
      };

    }

    /// <summary>
    /// Gets the subscription plan list by tenant identifier and plan state.
    /// </summary>
    /// <param name="planState">The plan state to filter subscription plan.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns subscription plan list that matches the input parameters.</returns>
    [HttpGet]
    [Route("list/{planState}")]
    public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPlanAndTenantAsync(BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
      return await _platformSubscriptionPlanDS.GetSubscriptionPlanListByTenantIdAsync(planState, cancellationToken);
    }

    [HttpGet]
    [Route("detail/{planId}")]
    public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid planId)
    {
      return await _platformSubscriptionPlanDS.GetSubscriptionPlaninfoByIdAsync(planId);
    }

  }
}
