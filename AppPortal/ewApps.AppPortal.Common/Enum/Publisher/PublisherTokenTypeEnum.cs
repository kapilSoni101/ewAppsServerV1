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

namespace ewApps.AppPortal.Common {

  /// <summary>
  /// Enum for Publisher Token Type.
  /// </summary>
  public enum PublisherTokenTypeEnum {

    /// <summary>
    /// Publisher user invitation with new email.
    /// </summary>
    PublisherUserWithNewEmailInvite = 1,

    /// <summary>
    /// Publisher user invitation with existing email.
    /// </summary>
    PublisherUserWithExistingEmailInvite = 2,

    /// <summary>
    /// Publisher user forgot password.
    /// </summary>
    PublisherUserForgotPassword = 3,

    /// <summary>
    /// Business primary user with new email invite.
    /// </summary>
    BusinessPrimaryUserWithNewEmailInvite = 4,

    /// <summary>
    /// Business primary user with existing email invite.
    /// </summary>
    BusinessPrimaryUserWithExistingEmailInvite = 5,

  }
}
