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
    public interface ISubscriptionPlanServiceDS:IBaseDS<SubscriptionPlanService> {

        Task AddSubscriptionPlanServiceAndAttribute(List<AppServiceDTO> appServList, Guid subPlanId, CancellationToken token = new CancellationToken());

        /// <summary>
        /// Gets the plan service and attribute list of provided plan ids.
        /// </summary>
        /// <param name="planIdList">The plan identifier list to get corresponding service and attributes.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns Service and Attributes list corresponding to provided plan id list.</returns>
        Task<List<SubsPlanServiceInfoDTO>> GetPlanServiceAndAttributeListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAndAddSubscribedServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken());
        /// <summary>
        /// Gets the plan service and attribute list of provided plan id.
        /// </summary>
        /// <param name="planId">The plan identifier to get corresponding service and attributes.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns Service and Attributes list corresponding to provided plan id.</returns>
        Task<List<SubsPlanServiceInfoDTO>> GetPlanServiceAndAttributeListByPlanIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken));

    }
}
