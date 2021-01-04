/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 20 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 Aug 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Base DTO for user session.
    /// </summary>
    public class BaseUserSessionAndAppDTO {

        // Done
        #region User

        /// <summary>
        /// Teannt user identifier to idnetifie the useruniquely.
        /// </summary>
        public Guid TenantUserId {
            get;
            set;
        }

        /// <summary>
        /// Login user email userd as userid for the user.
        /// </summary>
        public string UserEmail {
            get;
            set;
        }

        /// <summary>
        /// Login user full name.
        /// </summary>
        public string UserFullName {
            get;
            set;
        }

        /// <summary>
        /// Login Users thumbnail to be showd as user pofile image.
        /// </summary>
        public string UserThumbnailUrl {
            get;
            set;
        }

        /// <summary> 
        /// User tyep of the login user.
        /// </summary>
        public int UserType {
            get;
            set;
        }

        /// <summary>
        /// Joinig date of the user.
        /// </summary>
        [NotMapped]
        public DateTime? JoinedDate {
            get;
            set;
        }

        /// <summary>
        /// Phone number for constact of the login user.
        /// </summary>
        public string Phone {
            get;
            set;
        }

        #endregion User

        // Done
        #region Tenant

        /// <summary>
        /// Tenant identifier of the user to idnetify to which tenant belowngs to.
        /// </summary>
        public Guid TenantId {
            get;
            set;
        }

        /// <summary>
        /// Logged in user tenant name.
        /// </summary>
        public string TenantName {
            get;
            set;
        }

        /// <summary>
        /// Sub domain name of the tenant to which user belongs to.
        /// </summary>
        public string SubDomainName {
            get;
            set;
        }

        /// <summary>
        /// Tenant identity number.
        /// </summary>
        public string TenantIdentityNumber {
            get;
            set;
        }

        /// <summary>
        /// Tenant Type of the tenant to which user is login.
        /// </summary>
        public int TenantType {
            get;
            set;
        }

        #endregion Tenant

        // Done
        #region Portal app details

        /// <summary>
        /// Application ID of the portal to which user is login in.
        /// </summary>
        public Guid PortalAppId {
            get;
            set;
        }

        /// <summary> 
        /// Login portal Applciation name.
        /// </summary>
        public string PortalApplicationName {
            get;
            set;
        }

        /// <summary>
        /// Portal apptile for branding.
        /// </summary>
        public string PortalAppTitle {
            get;
            set;
        }

        /// <summary>
        /// Portal TopHeader LeftLogoUrl.
        /// </summary>
        public string PortalTopHeaderLeftLogoUrl {
            get;
            set;
        }

        /// <summary>
        /// Portal TopHeader LeftName.
        /// </summary>
        public string PortalTopHeaderLeftName {
            get;
            set;
        }

        /// <summary>
        /// Portal TopHeader Left PortalName.
        /// </summary>
        public string PortalTopHeaderLeftPortalName {
            get;
            set;
        }

        #endregion Portal app details

        /// <summary>
        /// List of all the applications
        /// </summary>
        [NotMapped]
        public List<UserSessionAppDTO> AppList {
            get;
            set;
        }

        // Done
        #region Branding and general localazation fields

        /// <summary>
        /// User tenant timezone.
        /// </summary>
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        /// Users tenant region.
        /// </summary>
        public string Region {
            get;
            set;
        }

        /// <summary>
        /// USers tenant languge.
        /// </summary>
        public string Language {
            get;
            set;
        }

        /// <summary>
        /// Currency of the tenant.
        /// </summary>
        public string Currency {
            get;
            set;
        }

        /// <summary>
        /// Date tiem formate for the tenant.
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }

        /// <summary>
        /// Powered by text.
        /// </summary>
        public string LeftPoweredBy {
            get;
            set;
        }

        /// <summary>
        /// Copyright text.
        /// </summary>
        public string ContentCopyrightText {
            get;
            set;
        }

        #endregion Branding and general localazation fields

        /// <summary>
        /// To find time required in execution of the api.
        /// </summary>
        [NotMapped]
        public long TimeTakenInMiliseconds {
            get;
            set;
        }
    }
}
