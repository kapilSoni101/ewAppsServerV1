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
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// This class implement business logic for app user
  /// </summary>
  public interface IPubSubscriptionPlanDS {

    Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken));
    Task<SubscriptionPlanInfoDTO> GetPubSubscriptionPlaninfoByIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the subscription service name list by subscription plan identifier.
        /// </summary>
        /// <param name="subsPlanId">The subs plan identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns service name list that matches provided subscription plan id.</returns>
        Task<IEnumerable<string>> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken));
  }
}