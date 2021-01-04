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
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {

    /// <summary>Interface exposing business notification receipent service</summary>
    public interface IBusinessNotificationReceipentDataService {

        /// <summary>
        /// Get the invited user.
        /// </summary>
        /// <param name="appId">Application identifier for whisch user is invited.</param>
        /// <param name="tenantId">Tenant identifier to which user belongs to.</param>
        /// <param name="tenantUserId">Teanntuser id of the invited user.</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedUser(Guid appId, Guid tenantId, Guid tenantUserId);

        /// <summary>
        /// Get the invited business users where is is of businesss type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get the invited business deleted users where is is of businesssPartner type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedDeletedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get the invited business partner users where is is of businesssPartner type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessPartnerUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get the invited business partner deleted users where is is of businesssPartner type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessPartnerDeletedUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get all the publisher users with given permissons  and preference.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <param name="eventId">business event for which notification is genratted</param>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherUserWithPermissionAndPrefrence(Guid tenantId, Guid appId , long eventId);

        /// <summary>
        /// Get all the business user of the particular tennat.
        /// </summary>
        /// <param name="tenantId">tenant identifier</param>
        /// <param name="appId">business application id to be passed.</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessUser(Guid tenantId, Guid appId);

        /// <summary>
        /// Get all the business user of the particular tennat for particularevent.
        /// </summary>
        /// <param name="tenantId">tenant identifier</param>
        /// <param name="appId">business application id to be passed.</param>
        /// <param name="eventId">business notification event .</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessUserWithPrefence(Guid tenantId, Guid appId, long eventId);

        /// <summary>
        /// Get all the business user having particular permission(either first permission or having second permission) and a perticular notification perefence on.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the tenantuser</param>
        /// <param name="appId">application identifier for the tenantuser</param>
        /// <param name="eventId">emeail preferece bitmask</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessUserWithPermissionsAndPrefernces(Guid tenantId, Guid appId, long eventId);

        /// <summary>
        /// Get all the business partner user having particular permission(either first permission or having second permission) and a perticular notification perefence on.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the tenantuser</param>
        /// <param name="appId">application identifier for the tenantuser</param>
        /// <param name="businessPastnerTenantId">business partner tenant identifier to identify to wich customer the partner belongs</param>
        /// <param name="eventId">emeail preferece bitmask</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessPartnerUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, Guid businessPastnerTenantId, long eventId);

        List<NotificationRecipient> GetForgotPasswordBusinessUser(Guid tenantId, Guid tenantUserId);
    }
}
