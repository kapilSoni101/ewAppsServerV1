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
    /// Token Type enum for payment.
    /// </summary>
    public enum PaymentBusinessUserTokenTypeEnum {

        /// <summary>
        /// Business User SetPassword.
        /// </summary>
        PaymentBusinessNewUserInvite = 1,

        /// <summary>
        /// Payment Business User with existing emailid Invited.
        /// </summary>
        PaymentBusinessExistingUserInvite = 2,

        /// <summary>
        /// Business Partner Primary User New EmailId Invited.
        /// </summary>
        BusinessPartnerPrimaryUserNewEmailIdInvited = 3,

        /// <summary>
        /// Business Partner Primary User Existing EmailId Invited.
        /// </summary>
        BusinessPartnerPrimaryUserExistingEmailIdInvited = 4,

        /// <summary>
        /// Business Partner Other User New EmailId Invited.
        /// </summary>
        BusinessPartnerOtherUserNewEmailIdInvited = 5,

        /// <summary>
        /// Business Partner Other User Existing EmailId Invited.
        /// </summary>
        BusinessPartnerOtherUserExistingEmailIdInvited = 6,

        /// <summary>
        /// Payment Business User Forgot Password Email.
        /// </summary>
        PaymentBusinessUserForgotPasswordEmail = 7
    }
}