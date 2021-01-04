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
    /// All events for which we need to generate notification from business-setup .
    /// </summary>
    [System.Flags]
    public enum BusinessNotificationEventEnum:long {

        #region New

        //BusinessPrimaryUserSetPasswordSucess = 1,
        //NewBusinessOnBoard = 2,
        BusinessUserWithNewEmailIdInvite = 3, // InUse
        BusinessUserWithExistingEmailIdInvite = 4,  // InUse
        //BusinessUserSetPasswordSucess = 5,
        //NewBusinessUserOnBoard = 6,
        //ApplicationAssignedToBusinessUser = 7,
        //ApplicationDeAssignedToBusinessUser = 8,
        //BusinessUserMarkActiveInActive = 9,
        //BusinessUserPermissionChanged = 10,
        //BusinessUserDeleted = 11,
        BusinessPartnerPrimaryNewUserInvite = 12,  // InUse
        //BusinessPartnerPrimaryExistingUserInvite = 13,
        //BusinessPartnerOtherUserNewEmailId = 14,
        //BusinessPartnerOtherUserExistingEmailId = 15,
        //ApplicationDeAssignedToBusinessPartnerUser = 16,
        //ApplicationRemovedForCustomerToBusinessPartnerUser = 17,
        //ApplicationRemovedForCustomerToBusinessUser = 18,
        //BusinessPartnerDeletedToPartnerUser = 19,
        //BusinessPartnerDeletedToBusinessUser = 20,
        //ExistingBusinessPartnerUserDeletedToPartnerUser = 21,
        //ExistingBusinessPartnerUserDeletedToBusinessUser = 22,
        BusinessUserForgotPassword = 23, // InUse

        #endregion New

        #region Old

        BusinessUserInvitation = 100,

        BusinessPartnerUserInvitation = 200,

        ForgotPasswordBusiness = 300,

        //ForgotPasswordBusinessPartner = 400,

        BusinessPartnerNewMail = 500

        #endregion Old
    }
}

//BusinessPrimaryUserSetPasswordSucess
//NewBusinessOnBoard
//BusinessUserWithNewEmailIdInvite
//BusinessUserWithExistingEmailIdInvite
//BusinessUserSetPasswordSucess
//NewBusinessUserOnBoard
//ApplicationAssignedToBusinessUser
//ApplicationDeAssignedToBusinessUser
//BusinessUserMarkActiveInActive
//BusinessUserPermissionChanged
//BusinessUserDeleted
//BusinessPartnerPrimaryNewUserInvite
//BusinessPartnerPrimaryExistingUserInvite
//BusinessPartnerPrimaryNewUserInvite
//BusinessPartnerPrimaryExistingUserInvite
//BusinessPartnerOtherUserNewEmailId
//BusinessPartnerOtherUserExistingEmailId
//ApplicationDeAssignedToBusinessPartnerUser
//ApplicationRemovedForCustomerToBusinessPartnerUser
//ApplicationRemovedForCustomerToBusinessUser
//BusinessPartnerDeletedToPartnerUser
//BusinessPartnerDeletedToBusinessUser
//ExistingBusinessPartnerDeletedToPartnerUser
//ExistingBusinessPartnerDeletedToBusinessUser
//BusinessUserForgotPassword


