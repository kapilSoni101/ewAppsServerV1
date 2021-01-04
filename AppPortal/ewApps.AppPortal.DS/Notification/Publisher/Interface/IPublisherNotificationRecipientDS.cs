/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {
    public interface IPublisherNotificationRecipientDS {

        /// <summary>
        /// Get the invited user.
        /// </summary>
        /// <param name="appId">application identifier</param>
        /// <param name="tenantId">tenant identifier</param>
        /// <param name="appUserId">user tenant identifier</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedUser(Guid appId, Guid tenantId, Guid appUserId);

        List<NotificationRecipient> GetPublisherSupportUsers();

        List<NotificationRecipient> GetPlatformUsersWithApplicationPermissionAndPrefrence(Guid tenantId, long emailPrefence);

        List<NotificationRecipient> GetBusinessUserWithApplicationAccess(Guid tenantId, Guid appId);

        List<NotificationRecipient> GetBusinessUserWithApplicationAccessWithoutStatus(Guid tenantId, Guid appId);

        List<NotificationRecipient> GetPublisherUserWithApplicationPermission(Guid tenantId, Guid appId);
        List<NotificationRecipient> GetPublisherUsersWithTenantPermissionAndPrefrence(Guid tenantId, Guid appId);
        List<NotificationRecipient> GetAllBusinessUsers(Guid tenantId);
        List<NotificationRecipient> GetAllBusinessPartnerUsers(Guid tenantId, Guid businessPartnerTenantId);
        List<NotificationRecipient> GetAllPlatformUsersWithPreference();
        List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid tenantId, Guid tenantUserId, long emailPreference, int userType, int userStatus);
        List<NotificationRecipient> GetBusinessPartnerUserWithApplicationAccess(Guid businessTenantId, Guid businessPartnerTenantId, Guid appId);

        /// <summary>
        /// Get publisher support users with preference
        /// </summary>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherSupportUsersWithTicketStatusPreference();

        /// <summary>
        /// Get publisher support users with preference
        /// </summary>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherSupportUsersWithTicketReassingedPreference();

        /// <summary>
        /// Get publisher support users with preference
        /// </summary>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherSupportUsersWithTicketPriorityChangePreference();

        /// <summary>
        /// Get publisher support users with preference
        /// </summary>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherSupportUsersWithCommentAddedToTicketPreference();

        /// <summary>
        /// Get publisher support users with preference
        /// </summary>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherSupportUsersWithAttcahmentAddedToTicketPreference();

        List<NotificationRecipient> GetForgotPasswordPublisherUser(Guid tenantId, Guid tenantUserId);
    }
}