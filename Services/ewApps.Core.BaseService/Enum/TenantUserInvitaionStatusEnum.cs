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

namespace ewApps.Core.BaseService {
    /// <summary>
    /// This enum describes the status of the user.
    /// </summary>
    public enum TenantUserInvitaionStatusEnum {

        /// <summary>
        /// When user is invited.
        /// </summary>
        Invited = 1,

        /// <summary>
        /// When user accepts the invitation.
        /// </summary>
        Accepted = 2,

        /// <summary>
        /// All the invitaion linked are expiled.
        /// </summary>
        InvitationCanceled =3
    }
}
