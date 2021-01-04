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
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This interface exposes all the methods to get platform receipents list.
    /// </summary>
    public interface IPlatformNotificationRecipientDS {
        List<NotificationRecipient> GetInvitedBusinessPrimaryUserRecipientList(Guid appId, Guid tenantId, Guid appUserId);
        List<NotificationRecipient> GetInvitedPublisherUserRecipientList(Guid appId, Guid tenantId, Guid appUserId);

        /// <summary>
        /// Get the invited user data generateing mail.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedPlatformUser(Guid appId, Guid tenantId, Guid appUserId);

        List<NotificationRecipient> GetInvitedPublihserUser(Guid appId, Guid tenantId, Guid appUserId);

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

        /// <summary>
        /// Get all the publisher all user.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllPublisherUsers(Guid tenantId);

        /// <summary>
        /// Get all the platform user with preference
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="emailPreference"></param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllPlatformUserWithPrerence(Guid tenantId, long emailPreference);

        /// <summary>
        /// Get specefic user with the preference.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appUserId"></param>
        /// <param name="emailPreference"></param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid appId, Guid tenantId, Guid appUserId, long emailPreference);

        List<NotificationRecipient> GetPlatformUsersWithApplicationPermissionAndPreference(Guid tenantId, long emailPrefrence);

        List<NotificationRecipient> GetForgotPasswordPlatformUser(Guid tenantId, Guid appUserId);

    }
}