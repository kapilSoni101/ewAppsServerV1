/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 13 Feb 2019
 */

using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.EmailService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    /// <summary>Interface exposing business notification service</summary>
    public interface IBusinessNotificationHandler {
        /// <summary>
        /// Sends the error email with information provided in input <see cref="ErrorLogEmailDTO"/> model.
        /// </summary>
        /// <param name="errorlogEmailDTO">The error information DTO to be log in email.</param>
        /// <param name="userSession">The instance of current user session.</param>
        void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO, UserSession userSession);


        Task InviteBusinessUser(BusinessAccountNotificationDTO businessNotificationDTO);
        Task InviteBusinessPartnerUser(BusinessAccountNotificationDTO businessNotificationDTO);
//Task ForgotPasswordBusinessPartner(BusinessAccountNotificationDTO businessNotificationDTO);
        Task ForgotPasswordBusiness(BusinessAccountNotificationDTO businessNotificationDTO);

        Task GenerateBusinessPrimaryUserSetPasswordSucessNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessOnBoardNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessNewUserInviteNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessUserWithExistingEmailIdInviteNotification(BusinessAccountNotificationDTO paymentUserNotificationDTO);
        Task GenerateBusinessUserSetPasswordSucessNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessUserOnBoardNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateApplicationAssignedToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateApplicationDeAssignedToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessUserAccountStatusChangedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPermissionChangedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessUserDeletedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerPrimaryUserExistingEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerOtherUserNewEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerOtherUserExistingEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateApplicationDeAssignedToBusinessPartnerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateApplicationRemovedForCustomerToBusinessPartnerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateApplicationRemovedForCustomerToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerDeletedToCustomerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerDeletedToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateExistingBusinessPartnerUserDeletedToPartnerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessPartnerUserDeletedToCustomerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO);
        Task GenerateBusinessForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO);
//Task GenerateCustomerForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO);
    }
}
