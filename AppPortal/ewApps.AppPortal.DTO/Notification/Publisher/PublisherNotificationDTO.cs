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
    public class PublisherNotificationDTO {

        /// <summary>
        /// User session.
        /// </summary>
        public UserSession UserSession {
            get; set;
        }

        /// <summary>
        /// Invited user.
        /// </summary>
        public Guid InvitedUserTenantId {
            get; set;
        }

        /// <summary>
        /// Publisher Name.
        /// </summary>
        public string PublisherCompanyName {
            get; set;
        }

        /// <summary>
        /// Business Name.
        /// </summary>
        public string BusinessCompanyName {
            get; set;
        }

        /// <summary>
        /// Invitor User FullName.
        /// </summary>
        public string InvitorUserFullName {
            get; set;
        }

        /// <summary>
        /// Publisher Portal URL.
        /// </summary>
        public string PublisherPortalURL {
            get; set;
        }

        /// <summary>
        /// Business Portal URL.
        /// </summary>
        public string BusinessPortalURL {
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
        public string SubDomain {
            get; set;
        }

        /// <summary>
        /// Invited user app.
        /// </summary>
        public string AppLicationName {
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
        /// portalName.
        /// </summary>
        public string PortalName {
            get; set;
        }

        /// <summary>
        /// Platform Company Name.
        /// </summary>
        public string PlatformCompanyName {
            get; set;
        }

        /// <summary>
        ///ApplicationURL.
        /// </summary>
        public string ApplicationURL {
            get; set;
        }

        /// <summary>
        ///NewSubscriptionPlanName.
        /// </summary>
        public string NewSubscriptionPlanName {
            get; set;
        }

        /// <summary>
        ///OldSubscriptionPlanName.
        /// </summary>
        public string OldSubscriptionPlanName {
            get; set;
        }

        /// <summary>
        ///Status.
        /// </summary>
        public string Status {
            get; set;
        }

        /// <summary>
        ///AdministratorMessage.
        /// </summary>
        public string AdministratorMessage {
            get; set;
        }
    }

}
