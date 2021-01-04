/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This DTO is collection of the data required to genarate business permission related notifications.
    /// </summary>
    public class CustomerUserPermissionChangeNotificationGeneralDTO {

        /// <summary>
        /// Publisher Name.
        /// </summary>
        public string PublisherName {
            get; set;
        }

        /// <summary>
        /// Application Name.
        /// </summary>
        public string ApplicationName {
            get; set;
        }

        /// <summary>
        /// Sub Domain of the tenant.
        /// </summary>
        public string SubDomain {
            get; set;
        }

        /// <summary>
        /// Business Company Name.
        /// </summary>
        public string BusinessCompanyName {
            get; set;
        }

        /// <summary>
        /// Copy RightText.
        /// </summary>
        public string CopyRightText {
            get; set;
        }

        /// <summary>
        /// Tenant identifier of the user.
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public Guid InvitedUserId {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string InvitedUserFullName {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string UserFullName {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public Guid? InvitedUserAppId {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string InvitedUserAppKey {
            get; set;
        }        

        /// <summary>
        /// user Session
        /// </summary>
        [NotMapped]
        public UserSession userSession {
            get; set;
        }



    }
}

