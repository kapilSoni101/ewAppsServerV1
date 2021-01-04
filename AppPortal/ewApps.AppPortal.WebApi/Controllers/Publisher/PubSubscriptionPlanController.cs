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
  public class PubSubscriptionPlanController:ControllerBase {

    #region Local member

    IPubSubscriptionPlanDS _pubSubscriptionPlanDS;

    #endregion

    #region Constructor

    public PubSubscriptionPlanController(IPubSubscriptionPlanDS pubSubscriptionPlanDS) {
      _pubSubscriptionPlanDS = pubSubscriptionPlanDS;
    }

    #endregion

    #region Get

    /// <summary>
    /// Get app services with attributes the app by AppId.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("list/publisherplan/{planState}")]
    
    public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _pubSubscriptionPlanDS.GetSubscriptionPlanListByPubTenantIdAsync(planState, cancellationToken);
    }

    [HttpGet]
    [Route("detail/publisherplan/{planId}")]
    public async Task<SubscriptionPlanInfoDTO> GetPubSubscriptionPlaninfoByIdAsync(Guid planId) {
      return await _pubSubscriptionPlanDS.GetPubSubscriptionPlaninfoByIdAsync(planId);
    }

        /// <summary>
        /// Gets the plan service name list by pub business subs plan identifier asynchronous.
        /// </summary>
        /// <param name="subsPlanId">The subscription plan identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns service name list that matches provided publisher business tenant id.</returns>
        [HttpGet]
        [Route("{subsPlanId}/servicenames")]
        public async Task<IEnumerable<String>> GetPlanServiceNameListByPubBusinessSubsPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _pubSubscriptionPlanDS.GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(subsPlanId, cancellationToken);
        }
        #endregion

    }
}
