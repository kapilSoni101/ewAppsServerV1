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
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController:ControllerBase {

        #region Local member

        ISubscriptionPlanDS _subscriptionPlanDS;
        ISubscriptionPlanServiceDS _subscriptionPlanServiceDS;

        #endregion

        #region Constructor

        public SubscriptionPlanController(ISubscriptionPlanDS subscriptionPlanDS, ISubscriptionPlanServiceDS subscriptionPlanServiceDS) {
            _subscriptionPlanDS = subscriptionPlanDS;
            _subscriptionPlanServiceDS = subscriptionPlanServiceDS;
        }

        #endregion

        #region Add/Update Methods

        /// <summary>
        /// Adds a subscription with services and their attributes.
        /// </summary>
        /// <param name="addUdpateDTO">add update dto </param>
        /// <param name="token">token</param>
        /// <returns>Success response objedt..</returns>
        [HttpPost]
        [Route("subscriptionplanwithattribute")]
        public async Task<ResponseModelDTO> AddSubscriptionPlanWithAttribute(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
            Guid planId = await _subscriptionPlanDS.AddSubscriptionPlanWithServiceAttributeAsync(addUdpateDTO, token);

            return new ResponseModelDTO() {
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
        public async Task<ResponseModelDTO> UpdateSubscriptionPlanWithAttribute(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
            await _subscriptionPlanDS.UpdateSubscriptionPlanWithServiceAttributeAsync(addUdpateDTO, token);

            return new ResponseModelDTO() {
                Id = new Guid(),
                IsSuccess = true,
                Message = ""
            };

        }

        /// <summary>
        /// Deletes a subscription with services and their attributes.
        /// </summary>
        /// <param name="planId">plan id to delete</param>
        /// <param name="token"> token </param>
        /// <returns>Success response objedt..</returns>
        [HttpDelete]
        [Route("subscriptionplanwithattribute/{planId}")]
        public async Task<ResponseModelDTO> DeleteSubscriptionPlanWithAttribute(Guid planId, CancellationToken token = new CancellationToken()) {
            await _subscriptionPlanDS.DeletePlanByPlanId(planId);

            return new ResponseModelDTO() {
                Id = new Guid(),
                IsSuccess = true,
                Message = ""
            };

        }

        #endregion Add/Update Methods

        #region Get

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get application specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        [HttpGet]
        [Route("list/{appId}/{planState}")]
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanDS.GetSubscriptionPlanListByAppIdAsync(appId, planState, cancellationToken);
        }

        /// <summary>
        /// Gets the subscription plan list by tenant identifier and plan state.
        /// </summary>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        [HttpGet]
        [Route("list/{planState}")]
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPlanAndTenantAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanDS.GetSubscriptionPlanListByTenantIdAsync(planState, cancellationToken);
        }

        ///// <summary>
        ///// Get app services with attributes the app by AppId.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("appserviceswithattribute/{appId}")]
        //public async Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync([FromRoute] Guid appId, CancellationToken token = default(CancellationToken)) {
        //  return await _platformSubscriptionPlanDS.GetAppServicesDetailWithAttributesAsync(appId);
        //}
        [HttpPut]
        [Route("services/list")]
        public async Task<List<SubsPlanServiceInfoDTO>> GetSubscriptionPlanServiceAndAttributeByPlanIds([FromBody] List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanServiceDS.GetPlanServiceAndAttributeListByPlanIdsAsync(planIdList, cancellationToken);
        }

        /// <summary>
        /// Gets the plan service and attribute list of provided plan id.
        /// </summary>
        /// <param name="planId">The plan identifier to get corresponding service and attributes.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns Service and Attributes list corresponding to provided plan id.</returns>
        [HttpGet]
        [Route("{planId}/services")]
        public async Task<List<SubsPlanServiceInfoDTO>> GetPlanServiceAndAttributeListByPlanIdAsync([FromRoute] Guid planId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanServiceDS.GetPlanServiceAndAttributeListByPlanIdAsync(planId, cancellationToken);
        }

        [HttpGet]
        [Route("detail/{planId}")]
        public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid planId) {
            return await _subscriptionPlanDS.GetSubscriptionPlaninfoByIdAsync(planId);
        }

        /// <summary>
        /// Gets the subscription plan list for pub tenant by plan state.
        /// </summary>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        [HttpGet]
        [Route("list/publisherplan/{planState}")]
        public async Task<List<SubscriptionPlanInfoDTO>> GetPubSubscriptionPlanListByStateAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanDS.GetSubscriptionPlanListByPubTenantIdAsync(planState, cancellationToken);
        }


        /// <summary>
        /// Gets the subscription service name list by subscription plan identifier asynchronous.
        /// </summary>
        /// <param name="subsPlanId">The subs plan identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns service name list that matches provided sbuscription plan id.</returns>
        [HttpGet]
        [Route("{subsPlanId}/servicenames")]
        public async Task<IEnumerable<string>> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanDS.GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(subsPlanId, cancellationToken);
        }

        ///// <summary>
        ///// Get app services with attributes the app by AppId.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("appserviceswithattribute/{appId}")]
        //public async Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync([FromRoute] Guid appId, CancellationToken token = default(CancellationToken)) {
        //  return await _platformSubscriptionPlanDS.GetAppServicesDetailWithAttributesAsync(appId);
        //}

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
    }
}
