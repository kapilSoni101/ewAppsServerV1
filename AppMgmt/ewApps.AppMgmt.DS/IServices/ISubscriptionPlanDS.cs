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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface ISubscriptionPlanDS:IBaseDS<SubscriptionPlan> {

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
        Task<List<TenantApplicationSubscriptionDTO>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get application specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the subscription plan list by plan identifier list and plan state.
        /// </summary>
        /// <param name="plandIdList ">The suscription plan ids list get plan information.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPlanIdListAsync(List<Guid> plandIdList , BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Gets the subscription plan list by tenant identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get tenant specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken));

        ///// <summary>
        ///// Gets the subscription plan service and attribute by plan ids.
        ///// </summary>
        ///// <param name="planIdList">The plan identifier list to get list of subscriptoin plan list.</param>
        ///// <returns>Returns subscription plan list that matches given plan ids.</returns>
        //Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanServiceAndAttributeByPlanIdsAsync(List<Guid> planIdList);

        /// <summary>
        /// Gets the subscription plan info by plan id.
        /// </summary>
        /// <param name="planId">Planid.</param>
        /// <returns>Returns subscription plan detail info that matches given plan id.</returns>
        Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Gets the subscription service name list by subscription plan identifier asynchronous.
        /// </summary>
        /// <param name="subsPlanId">The subs plan identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns service name list that matches provided sbuscription plan id.</returns>
        Task<IEnumerable<string>> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get

        #region Add/Update/Delete Methods

        /// <summary>
        /// Adds a subscription with services and their attributes.
        /// </summary>
        /// <param name="addUdpateDTO">add update dto </param>
        /// <param name="token">token</param>
        /// <returns>Newly generated plan id.</returns>
        Task<Guid> AddSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken());

        /// <summary>
        /// Updates the sub plan wirhwith services and attribute.
        /// </summary>
        /// <param name="addUdpateDTO">add update dto </param>
        /// <param name="token">token</param>
        Task UpdateSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken());

        /// <summary>
        /// Deletes a plan by given id.
        /// </summary>
        /// <param name="planId">plan id to delete </param>
        Task DeletePlanByPlanId(Guid planId);

      
        #endregion Add/Update/Delete Methods
    }
}
