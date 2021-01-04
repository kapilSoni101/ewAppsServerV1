/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface ISubscriptionPlanServiceAttributeDS:IBaseDS<SubscriptionPlanServiceAttribute> {

        Task AddSubscriptionPlanAttribute(List<AppServiceAttributeDTO> attrListDTO, Guid subPlanId, Guid subPlanServiceId, CancellationToken token = new CancellationToken());
   
        /// <summary>
        /// Gets the plan service attribute list by plan ids.
        /// </summary>
        /// <param name="planIdList">The plan identifier list to find application service attributes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns application service attribute list that are defined under given plan ids.</returns>
        Task<List<SubsPlanServiceAttributeInfoDTO>> GetPlanServiceAttributeListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken));

    }
}
