/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 03 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 03 September 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS
{

  /// <summary>
  /// This interface provides method to manage Publisher signup operations.
  /// </summary>
  public interface IPublisherSignUpDS
  {
    /// <summary>
    /// Add's publisher and primary user realated information.
    /// </summary>
    /// <param name="publisherSignUpDTO">The publisher sign up dto.</param>
    /// <param name="cancellationToken">The async task token to notify about task cancellation.</param>
    /// <returns>Returns newly signed up publisher tenant and it's primary user information.</returns>
    Task<TenantSignUpResponseDTO> PublisherSignUpAsync(PublisherSignUpRequestDTO publisherSignUpDTO, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Gets the subscription plan list by application identifier and plan state.
    /// </summary>
    /// <param name="appId">The application identifier to get application specific subscription.</param>
    /// <param name="planState">The plan state to filter subscription plan.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns subscription plan list that matches the input parameters.</returns>
    Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Gets the plan service and attribute list of provided plan id.
    /// </summary>
    /// <param name="planId">The plan identifier to get corresponding service and attributes.</param>
    /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
    /// <returns>Returns Service and Attributes list corresponding to provided plan id.</returns>
    Task<List<SubsPlanServiceInfoDTO>> GetServiceAndAttributeDetailByPlanId(Guid planId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Reinvites the publisher admin user mail is sent to the user with password set link.
    /// </summary>
    /// <param name="tenantUserIdentificationDTO">tenat user identification DTO for users complete identification for tenant and application.</param>
    /// <returns></returns>
    Task ReInviteTenantUserAsync(TenantUserIdentificationDTO tenantUserIdentificationDTO);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantUserId"></param>
    /// <param name="bizTenantId"></param>
    /// <param name="appId"></param>
    /// <param name="subDomain"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task ReInvitePrimaryPublisherUserAsync(Guid tenantUserId, Guid pubTenantId, string subDomain, CancellationToken token = default(CancellationToken));
  }
}