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

using System;
using System.Collections.Generic;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Data trasfer object having all the required properties for creating notification data fro the platform.
    /// </summary>
    public class PlatformNotificationDTO {

        /// <summary>
        /// user session.
        /// </summary>
        public UserSession UserSession {
            get; set;
        }

        ///// <summary>
        ///// Invited user.
        ///// </summary>
        //public TenantUser InvitedTenantUser {
        //  get; set;
        //}

        ///// <summary>
        ///// Invited user app.
        ///// </summary>
        //public App App {
        //    get; set;
        //}

        /// <summary>
        /// Invited user.
        /// </summary>
        public Guid InvitedUserTenantId {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string PlatformCompanyName {
            get; set;
        }

        /// <summary>
        /// Invitor User FullName.
        /// </summary>
        public string InvitorUserFullName {
            get; set;
        }

        /// <summary>
        /// Platform Portal URL.
        /// </summary>
        public string PlatformPortalURL {
            get; set;
        }

        /// <summary>
        /// Publisher Portal URL.
        /// </summary>
        public string PublisherPortalURL {
            get; set;
        }

        /// <summary>
        /// Platform CopyRigthText.
        /// </summary>
        public string CopyRigthText {
            get; set;
        }



        /// <summary>
        /// Platform Name.
        /// </summary>
        public string TenantLanguage {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string PasswordCode {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string InvitedUserEmail {
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
        public Guid InvitedUserIdentityUserId {
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
        public Guid InvitedUserAppId {
            get; set;
        }

        /// <summary>
        /// Platform Name.
        /// </summary>
        public string InvitedUserAppKey {
            get; set;
        }


        /// <summary>
        /// Platform Name.
        /// </summary>
        public string PortalName {
            get; set;
        }

        public string PublisherCompanyName {
            get; set;
        }

        public string SubDomain {
            get; set;
        }

        public string Status {
            get; set;
        }

        //
        //public NotificationUserSessionDTO MyProperty {
        //    get; set;
        //}
    }
}
