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
    public interface IPlatformNotificationHandler {

        /// <summary>
        /// Sends the error email with information provided in input <see cref="ErrorLogEmailDTO"/> model.
        /// </summary>
        /// <param name="errorlogEmailDTO">The error information DTO to be log in email.</param>
        /// <param name="userSession">The instance of current user session.</param>
        void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO, UserSession userSession);

        /// <summary>
        /// Method impletemented to generate notification for invited platform user with new email.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        Task SendPlatformUserWithNewEmailInviteAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for invited platform user with existing email.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task SendPlatformUserWithExistingEmailInviteAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for user forgot password.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task SendPlatformUserForgotPasswordEmailAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for invited publisher primary user with new email.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task SendPublisherPrimaryUserWithNewEmailInvitateAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for invited publisher primary user with existing email.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task SendPublisherPrimaryUserWithExistingEmailInvitateAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for active\inactive publisher notify to publisher users.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PublisherActiveInActiveNotifyToPublisherAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method impletemented to generate notification for active\inactive publisher notify to platform users.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PublisherActiveInActiveNotifyToPlatformAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method genenrate notification for platform user sets password succesfully.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PlatformUserSetPasswordSucessfullyAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method genenrate notification for platform user status is chnaged that is when active / inactive.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PlatformUserMarkActiveInActiveAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method genenrate notification for platform permissions are changed.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PlatformUserPermissionChangedAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method genenrate notification for platform user is logind in for the first time.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PlatformUserOnBoardAsync(PlatformNotificationDTO platformNotificationDTO);

        /// <summary>
        /// Method genenrate notification for platform user is deleted.
        /// </summary>
        /// <param name="platformNotificationDTO">Dto storing the notifiation data.</param>
        /// <returns></returns>
        Task PlatformUserDeletedAsync(PlatformNotificationDTO platformNotificationDTO);

    }
}