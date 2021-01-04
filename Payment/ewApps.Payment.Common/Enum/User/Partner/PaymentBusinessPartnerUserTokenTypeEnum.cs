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

namespace ewApps.Payment.Common {

  /// <summary>
  /// Token Type enum for payment business partner user.
  /// </summary>
  public enum PaymentBusinessPartnerUserTokenTypeEnum {

    /// <summary>
    /// Business Partner User NewEmailId is inviated.
    /// </summary>
    BusinessPartnerUserNewEmailIdAdded = 1,

    /// <summary>
    /// Business Partner User Existing EmailId is invited.
    /// </summary>
    BusinessPartnerUserExistingEmailIdAdded = 2,

    /// <summary>
    /// Payment Business Customer Forgot Password.
    /// </summary>
    PaymentBusinessCustomerForgotPassword = 3
  }
}
