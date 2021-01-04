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

namespace ewApps.Shipment.Common {

  /// <summary>
  /// Token Type enum for payment.
  /// </summary>
  public enum ShipmentTokenTypeEnum {

    /// <summary>
    /// Shipment Business User New Email Invite.
    /// </summary>
    ShipmentBusinessUserNewEmailInvite = 1,

    /// <summary>
    /// Shipment Business User Existing Email Invite.
    /// </summary>
    ShipmentBusinessUserExistingEmailInvite = 2,

    /// <summary>
    /// Forgot password mail.
    /// </summary>
    ShipmentUserForgotPassword = 3
  }
}
