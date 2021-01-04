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

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Publisher Notification events.
    /// </summary>
    public enum PublisherNotificationEvent:long {

        #region New Events

        /// <summary>
        /// Publisher user invitation with new email.
        /// </summary>
        InvitePublisherUserWithNewEmail = 1,

        /// <summary>
        /// Publisher user invitation with existing email.
        /// </summary>
        InvitePublisherUserWithExistingEmail = 2,

        /// <summary>
        /// Publisher user forgot password.
        /// </summary>
        PublisherUserForgotPassword = 3,

        /// <summary>
        /// Business primary user with new email invite.
        /// </summary>
        InviteBusinessPrimaryUserWithNewEmail = 4,

        /// <summary>
        /// Business primary user with existing email invite.
        /// </summary>
        InviteBusinessPrimaryUserWithExistingEmail = 5,


        NewBusinessOnBoardToPubUser = 6,

        /// <summary>
        ///  Business Active Inactive To Publisher User.
        /// </summary>
        BusinessStatusChangedToPubUser = 7,

        BusinessIsDeletedToPubUser = 8,

        BusinessApplicationAccessUpdatedToPubUser = 9,

        ApplicationIsUpdatedToPubUser = 10,

        ApplicationStatusChangedToPubUser = 11,

        /// <summary>
        /// Publisher user permission is changed.
        /// </summary>
        PublisherUserPermissionChanged = 12,


        /// <summary>
        /// User Joins the platform portal.
        /// </summary>
        PublisherUserOnBoard = 13,

        /// <summary>
        /// Publisher User is deleted.
        /// </summary>
        PublisherUserDeleted = 14,

        ContactUsToPlatformUser = 15,

        #endregion

        #region Old Events
        ///// <summary>
        ///// Publisher user invitation with new email.
        ///// </summary>
        //PublisherUserWithNewEmailInvite = 1,

        ///// <summary>
        ///// Publisher user invitation with existing email.
        ///// </summary>
        //PublisherUserWithExistingEmailInvite = 2,

        ///// <summary>
        ///// Publisher user forgot password.
        ///// </summary>
        //PublisherUserForgotPassword = 3,

        ///// <summary>
        ///// Business primary user with new email invite.
        ///// </summary>
        //BusinessPrimaryUserWithNewEmailInvite = 4,

        ///// <summary>
        ///// Business primary user with existing email invite.
        ///// </summary>
        //BusinessPrimaryUserWithExistingEmailInvite = 5,

        ///// <summary>
        ///// Application is subscribed to business
        ///// </summary>
        //ApplicationSubscribedToBusiness = 6,

        ///// <summary>
        ///// Business existing subscription plan changed to publisher user
        ///// </summary>
        //BusinessExistingSubscriptionPlanChangedToPublisherUser = 7,

        ///// <summary>
        ///// Business existing subscription plan changed to business user
        ///// </summary>
        //BusinessExistingSubscriptionPlanChangedToBusinessUser = 8,

        ///// <summary>
        /////  Business Active Inactive To Publisher User.
        ///// </summary>
        //BusinessActiveInactiveToPublisherUser = 9,

        ///// <summary>
        /////  Business Active Inactive To business User.
        ///// </summary>
        //BusinessActiveInactiveToBusinesUser = 10,

        ///// <summary>
        /////  Business Active Inactive To business partner User.
        ///// </summary>
        //BusinessActiveInactiveToBusinessPartnerUser = 11,

        ///// <summary>
        /////  Business is deleted To Publisher User.
        ///// </summary>
        //BusinessDeletedToPublisherUser = 12,

        ///// <summary>
        /////  Business is deleted To business User.
        ///// </summary>
        //BusinessDeletedToBusinesUser = 13,

        ///// <summary>
        /////  Business is deleted To business partner User.
        ///// </summary>
        //BusinessDeletedBusinessPartnerUser = 14,

        ///// <summary>
        ///// User sets password succesfully.
        ///// </summary>
        //PublisherUserSetPasswordSucess = 15,

        ///// <summary>
        ///// Publisher primary user login for the first time.
        ///// </summary>
        //PublisherOnBoard = 16,

        ///// <summary>
        ///// Publisher user status is changed.
        ///// </summary>
        //PublisherUserMarkAsActiveInactive = 17,

        ///// <summary>
        ///// Publisher user permission is changed.
        ///// </summary>
        //PublisherUserPermissionChanged = 18,

        ///// <summary>
        ///// User Joins the platform portal.
        ///// </summary>
        //PublisherUserOnBoard = 19,

        ///// <summary>
        ///// Publisher User is deleted.
        ///// </summary>
        //PublisherUserDeleted = 20,

        ///// <summary>
        ///// Publisher Application Status  is Changed notify To Publisher User.
        ///// </summary>
        //PublisherApplicationStatusChangedToPublisherUser = 21,

        ///// <summary>
        ///// Publisher Application Status  is Changed notify To business User.
        ///// </summary>
        //PublisherApplicationStatusChangedToBusinessUser = 22,

        ///// <summary>
        ///// Publisher Application Status  is Changed notify To business partner User.
        ///// </summary>
        //PublisherApplicationStatusChangedToBusinessPartnerUser = 23,

        ///// <summary>
        ///// Contact us 
        ///// </summary>
        //ContactUsToPlatformUser = 24


        #endregion

    }
}
