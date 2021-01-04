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

using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.EmailService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This interface exposes all the publisher related notitfication services.
    /// </summary>
    public interface IPublisherNotificationHandler {

        /// <summary>
        /// Sends the error email with information provided in input <see cref="ErrorLogEmailDTO"/> model.
        /// </summary>
        /// <param name="errorlogEmailDTO">The error information DTO to be log in email.</param>
        /// <param name="userSession">The instance of current user session.</param>
        void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO, UserSession userSession);


        /// <summary>
        /// Method impletemented to generate notification for invited publisher user with new email.
        /// </summary>
        /// <param name="publisherNotificationDTO">Dto storing the notifiation data.</param>
        Task SendPublisherUserNewEmailInvite(PublisherNotificationDTO publisherNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for invited publisher user with new email.
        /// </summary>
        /// <param name="publisherNotificationDTO">Dto storing the notifiation data.</param>
        Task SendPublisherUserExistingEmailInvite(PublisherNotificationDTO publisherNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for forgot password publisher user.
        /// </summary>
        /// <param name="publisherNotificationDTO">Dto storing the notifiation data.</param>
        Task SendPublisherForgotPasswordEmail(PublisherNotificationDTO publisherNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for new business user invite.
        /// </summary>
        /// <param name="publisherNotificationDTO">Dto storing the notifiation data.</param>
        Task SendBusinessPrimaryUserWithNewEmailInvite(PublisherNotificationDTO publisherNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for existing business user invite.
        /// </summary>
        /// <param name="publisherNotificationDTO">Dto storing the notifiation data.</param>
        Task SendBusinessPrimaryUserWithExistingEmailInvite(PublisherNotificationDTO publisherNotificationDTO);

        Task NewPublisherOnboard(PublisherNotificationDTO publisherNotificationDTO);
        Task BusinessMarkAsActiveInactiveToPublisher(PublisherNotificationDTO publisherNotificationDTO);

        Task BusinessDeletedToBusinessPartnerUser(PublisherNotificationDTO publisherNotificationDTO);
        Task BusinessDeletedToPublisherUser(PublisherNotificationDTO publisherNotificationDTO);
        Task PublisherUserPermissionChanged(PublisherPermissionNotificationDTO publisherPermissionNotificationDTO);
        Task NewPublisherUserOnBoard(PublisherNotificationDTO publisherNotificationDTO);
        Task PublisherUserDeleted(PublisherNotificationDTO publisherNotificationDTO);
        Task PublisherApplicationStatusChangedToBusinessPartnerUser(PublisherNotificationDTO publisherNotificationDTO);
        Task PublisherApplicationStatusChangedToPublisherUser(PublisherNotificationDTO publisherNotificationDTO);

        Task SendEmailForContactUs(ContactUsDTO ContactUsDTO);


        #region Old Methods

        //Task PublisherUserSetPasswordSucess(PublisherNotificationDTO publisherNotificationDTO);
        //Task BusinessExistingSubscriptionPlanChangedToPublisherUser(PublisherNotificationDTO publisherNotificationDTO);
        //Task ApplicationSubscribedToBusiness(PublisherNotificationDTO publisherNotificationDTO);
        //Task BusinessExistingSubscriptionPlanChangedToBusinessUser(PublisherNotificationDTO publisherNotificationDTO);
        //Task BusinessMarkAsActiveInactiveToBusiness(PublisherNotificationDTO publisherNotificationDTO);
        //Task BusinessMarkAsActiveInactiveToBusinessPartner(PublisherNotificationDTO publisherNotificationDTO);
        //Task BusinessDeletedToBusinessUser(PublisherNotificationDTO publisherNotificationDTO);
        //Task PublisherUserMarkAsActiveInActive(PublisherNotificationDTO publisherNotificationDTO);
        //Task PublisherApplicationStatusChangedToBusinessUser(PublisherNotificationDTO publisherNotificationDTO);

        #endregion
    }
}