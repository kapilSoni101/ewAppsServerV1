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
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Data {

  /// <summary>
  /// Provides functionality to an object to perform database 
  /// operations on SubscriptionPlanServiceAttribute entity.
  /// </summary>
  public interface ISubscriptionPlanServiceAttributeRepository:IBaseRepository<SubscriptionPlanServiceAttribute> {
        /// <summary>
        /// Gets the plan service attribute list by plan ids.
        /// </summary>
        /// <param name="planIdList">The plan identifier list to find application service attributes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns application service attribute list that are defined under given plan ids.</returns>
        Task<List<SubsPlanServiceAttributeInfoDTO>> GetPlanServiceAttributeListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken));
           

    }
}
