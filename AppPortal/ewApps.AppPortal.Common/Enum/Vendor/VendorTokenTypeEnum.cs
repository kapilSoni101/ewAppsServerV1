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
    /// Enum for Vendor notification event Token Type.
    /// </summary>
    public enum VendorTokenTypeEnum {

        VendorUserWithNewEmailIdInvite = 1,
        VendorUserWithExistingEmailIdInvite = 2,
        VendorPartnerPrimaryNewUserInvite = 3,
        VendorPartnerPrimaryExistingUserInvite = 4,
        VendorPartnerOtherUserNewEmailId = 5,
        VendorPartnerOtherUserExistingEmailId = 6,
        VendorUserForgotPassword=7,
        VendorPartnerUserForgotPassword = 8,
        VendorInviteWithNewEmail = 9,


  }
}
