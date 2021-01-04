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
  /// Platform notification events.
  /// </summary>
  public enum PlatformNotificationEvents:long {

//        1.1. Platform inviting a new Publisher. -- 4,5
//1.2. Publisher Admin receives set password success email. -- publisher
//1.3. Platform receives alert for new Publisher onboard. -- 
//1.4. Publisher Portal account status is changed.
//1.5. Platform Admin inviting a new Platform User.
//1.6. Platform User receives set password success email.
//1.7. Platform User gets Forgot Password link. 
//1.8. Platform User account status is changed.
//1.9. Platform User permission(s) changed. 
//1.10. New Platform User is onboard.
//1.11. Existing Platform User is deleted.
//1.12. Publisher User changes Ticket Status, Priority update, Status change, add new comment, add new attachment(s).


    /// <summary>
    /// Platform other user  with new email.
    /// </summary>
    PlatformUserWithNewEmailInvite = 1,

    /// <summary>
    /// Platform other user email.
    /// </summary>
    PlatformUserExistingEmailInvite = 2,

    /// <summary>
    /// Platform user forgot password email .
    /// </summary>
    PlatformUserForgotPassword = 3,

    /// <summary>
    /// Publisher primary user with new email.
    /// </summary>
    PublisherPrimaryUserWithNewEmailInvite = 4,

    /// <summary>
    /// Publisher primary user with new email.
    /// </summary>
    PublisherPrimaryUserWithExistingEmailInvite = 5,

    /// <summary>
    /// Publisher masr as active inactive notify publisher user.
    /// </summary>
    PublisherMarkAsActiveInActiveNotifyPublisherUsers = 6,

    /// <summary>
    /// Publisher masr as active inactive notify platform user.
    /// </summary>
    PublisherMarkAsActiveInActiveNotifyPlatformUsers = 7,

    /// <summary>
    /// Platform user sets password sucessfully.
    /// </summary>
    PlatformUserPasswordSetSucess = 8,

    /// <summary>
    /// Publisher mask as active inactive notify publisher user.
    /// </summary>
    PlatformUserMarkAsActiveInActive = 9,

    /// <summary>
    /// Platform user permissions chnaged.
    /// </summary>
    PlatformUserPermissionChanged = 10,

    /// <summary>
    /// User Joins the platform portal.
    /// </summary>
    PlatformUserOnBoard = 11,

    /// <summary>
    /// Platform User is deleted.
    /// </summary>
    PlatformUserDeleted = 12

  }
}
