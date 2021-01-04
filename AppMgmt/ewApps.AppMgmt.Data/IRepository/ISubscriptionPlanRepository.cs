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
    /// operations on SubscriptionPlan entity.
    /// </summary>
    public interface ISubscriptionPlanRepository:IBaseRepository<SubscriptionPlan> {

        #region Get

        /// <summary>
        /// Get subscrption plan by appId.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<SubscriptionPlan> GetSubscriptionPlansByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get subscription plan by application id and  tenantid.
        /// </summary>
        /// <param name="appId">Applicationid</param>
        /// <param name="publisherTenantId">tenantid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<SubscriptionPlan>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get application specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="tenantId">The tenant identifier to get tenant specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByTenantIdAsync(Guid tenantId, bool planState, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the subscription plan info by application identifier and plan state.
        /// </summary>
        /// <param name="id">The plan identifier to specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Gets the subscription service name list by subscription plan identifier asynchronous.
        /// </summary>
        /// <param name="subsPlanId">The subs plan identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns service name list that matches provided sbuscription plan id.</returns>
        IEnumerable<string> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get

    }
}
