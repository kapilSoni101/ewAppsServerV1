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
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This DTO is collection of the data required to genarate business accoun related notifications.
    /// </summary>
    public class BusinessAccountNotificationDTO {

        /// <summary>
        /// User Session.
        /// </summary>
        public UserSession UserSession {
            get; set;
        }

        ///// <summary>
        ///// TenantUser.
        ///// </summary>
        //public TenantUser TenantUser {
        //    get; set;
        //}

        /// <summary>
        /// Publisher Name.
        /// </summary>
        public string PublisherName {
            get; set;
        }

        ///// <summary>
        ///// Applicatin identifier.
        ///// </summary>
        //public App App {
        //    get; set;
        //}

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
        /// Business Partner CompanyName.
        /// </summary>
        public string BusinessPartnerCompanyName {
            get; set;
        }

        /// <summary>
        /// Invitor UserName.
        /// </summary>
        public string InvitorUserName {
            get; set;
        }

        /// <summary>
        /// Portal URL.
        /// </summary>
        public string PortalURL {
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
        /// BusinessPartnerTenantId.
        /// </summary>
        public Guid BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// User status active incative.
        /// </summary>
        public string Status {
            get; set;
        }

        /// <summary>
        /// User status active incative.
        /// </summary>
        public string PartnerType {
            get; set;
        }

        /// <summary>
        /// User portal name.
        /// </summary>
        public string PortalName {
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
        ///AdministratorMessage.
        /// </summary>
        public string AdministratorMessage {
            get; set;
        }

        public int UserType {
            get; set;
        }

        public string ActionDate {
            get; set;
        }

        public string NewUserStatus { get; set; }

        public string OldUserStatus { get; set; }

  }
}