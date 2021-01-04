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
    /// Enum for business notification event Token Type.
    /// </summary>
    public enum BusinessTokenTypeEnum {

        BusinessUserWithNewEmailIdInvite = 1,

        BusinessUserWithExistingEmailIdInvite = 2,

        BusinessPartnerPrimaryNewUserInvite = 3,
        BusinessPartnerPrimaryExistingUserInvite = 4,
        BusinessPartnerOtherUserNewEmailId = 5,
        BusinessPartnerOtherUserExistingEmailId = 6,
        BusinessUserForgotPassword=7,
        BusinessPartnerUserForgotPassword = 8,


    #region Old

    BusinessPartnerUserInvitation = 200,

        #endregion Old
    }
}
