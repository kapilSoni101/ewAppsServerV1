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
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// This class implement business logic for app user
  /// </summary>
  public interface IPlatformSubscriptionPlanDS {

    Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync(Guid appId, CancellationToken token = default(CancellationToken));

    Task AddSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken());

    Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByTenantIdAsync(BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken));

    Task UpdateSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken());

    /// <summary>
    /// Gets the subscription plan info by plan id.
    /// </summary>
    /// <param name="planId">Planid.</param>
    /// <param name="cancellationToken">token</param>
    /// <returns>Returns subscription plan detail info that matches given plan id.</returns>
    Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Deletes the subscription plan info by plan id.
    /// </summary>
    /// <param name="planId">Planid.</param>
    /// <param name="cancellationToken">token</param>
    /// <returns>Returns subscription plan detail info that matches given plan id.</returns>
    Task DeleteSubscriptionPlanWithServiceAttributeAsync(Guid planId, CancellationToken token = new CancellationToken());

    ///// <summary>
    ///// Update AppUser For Publisher
    ///// </summary>
    ///// <param name="dto"></param>
    //void UpdateAppUser(AppUserDTO dto);

    ///// <summary>
    ///// Gets the user info by UserId.
    ///// </summary>
    ///// <param name="appUserId"></param>
    ///// <returns></returns>
    //Task<AppUserDTO> GetUserInfoByIdAsync(Guid appUserId);

    ///// <summary>
    ///// Gets the list of the users by userType.
    ///// </summary>
    ///// <returns></returns>
    //Task<List<AppUserDetailsDTO>> GetAllUsersByUserTypeAsync(bool deleted);

    ///// <summary>
    ///// Deletes the appUser by id.
    ///// </summary>
    ///// <param name="appUserId"></param>
    ///// <returns></returns>
    //Task<DeleteUserResponseDTO> DeleteTenantUserAsync(Guid appUserId);

    ///// <summary>
    ///// Update User joined Date for business.
    ///// </summary>
    ///// <returns></returns>
    //Task UpdateUserJoinedDateAsync(AppJoinedDateInfoRequest appJoinedDateInfoRequest, CancellationToken token = default(CancellationToken));


    ///// <summary>
    ///// Updates the user details.
    ///// </summary>
    ///// <param name="dto"></param>
    ///// <returns></returns>
    //Task<UpdateTenantUserResponseDTO> UpdateUserAsync(AppUserDetailsDTO dto);


    ///// <summary>
    ///// Add Publisher User.
    ///// </summary>
    ///// <param name="appUserDTO"></param>
    ///// <returns></returns>
    //Task AddPlatformUserAsync(AppUserDTO appUserDTO);

    ///// <summary>
    ///// Delete users by tenantid.
    ///// </summary>
    ///// <param name="tenantId"></param>
    ///// <returns></returns>
    //Task DeleteUserByTenantIdAsync(Guid tenantId);


    ///// <summary>
    ///// Updates the user invitaion status.
    ///// </summary>
    ///// <param name="identityuserid"></param>
    ///// <param name="tenantId"></param>
    ///// <returns></returns>
    //Task UpdateUserInvitationStatusAsync(Guid identityuserid, Guid tenantId);

    ///// <summary>
    ///// Send forgot password email
    ///// </summary>
    ///// <param name="forgotPasswordDTO"></param>
    //Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);

    ///// <summary>
    ///// Get token detail by given token id
    ///// </summary>
    ///// <param name="tokenId">The Token Id</param>
    ///// <returns></returns>
    //Task<TokenDataDTO> GetTokenInfoByTokenId(Guid tokenId, string appkey);

    ///// <summary>
    ///// Get token detail by given subdoamin
    ///// </summary>
    ///// <param name="subdomain">The Tenant Subdomain.</param>
    ///// <returns></returns>
    //Task<TokenDataDTO> GetTokenInfoBySubdomain(string subdomain, string appkey);

    ///// <summary>
    ///// Send user reinvitaion mail and update invitor details.
    ///// </summary>
    ///// <param name="appUserId"></param>
    ///// <returns></returns>
    //Task ReInviteUser(Guid appUserId);
  }
}