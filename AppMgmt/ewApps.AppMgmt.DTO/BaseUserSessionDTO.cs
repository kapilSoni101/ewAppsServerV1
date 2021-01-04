using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class BaseUserSessionDTO {


        #region User

        /// <summary>
        /// App user identifier.
        /// </summary>
        public Guid TenantUserId {
            get;
            set;
        }

        /// <summary>
        /// User email.(Yes)
        /// </summary>
        public string UserEmail {
            get;
            set;
        }

        /// <summary>
        /// User Full name. (Yes)
        /// </summary>
        public string UserFullName {
            get;
            set;
        }

        /// <summary>
        /// User permission bitmask. (Yes)
        /// </summary>
        public long PermissionBitMask {
            get;
            set;
        }

        /// <summary>
        /// Admin flag of the user. (Yes)
        /// </summary>
        public bool IsAdmin {
            get;
            set;
        }

        /// <summary>
        /// USers last login time. (Yes)
        /// </summary>
        public DateTime? LastLoginTime {
            get;
            set;
        }

        /// <summary>
        /// USers thumbnail file name. (Yes)
        /// </summary>
        public string UserThumbnailUrl {
            get;
            set;
        }

        /// <summary> (Yes)
        /// User Type.
        /// </summary>
        public int UserType {
            get;
            set;
        }

        /// <summary>
        /// Joinig date of the user. (Yes)
        /// </summary>
        public DateTime? JoinedDate {
            get;
            set;
        }

        /// <summary>
        /// Custome ref id for the custome user. (Check)
        /// </summary>
        public string Phone {
            get;
            set;
        }

        #endregion User

        #region Tenant

        /// <summary>
        /// USer tenant identifier. (Yes)
        /// </summary>
        public Guid TenantId {
            get;
            set;
        }

        /// <summary>
        /// Logged in user tenant name. (Yes)
        /// </summary>
        public string TenantName {
            get;
            set;
        }

        /// <summary> (Yes)
        /// Sub domain name.
        /// </summary>
        public string SubDomainName {
            get;
            set;
        }

        /// <summary>
        /// Tenant identity number. (Check)
        /// </summary>
        public string TenantIdentityNumber {
            get;
            set;
        }

        public int TenantType {
            get;
            set;
        }

        #endregion Tenant

        #region App

        /// <summary>
        /// Cureent Application identifier of the user shows in which application user is logged in. (Yes)
        /// </summary>
        public Guid? CurrentAppId {
            get;
            set;
        }

        /// <summary> 
        /// Applciation name. (Yes)
        /// </summary>
        public string ApplicationName {
            get;
            set;
        }

        public string AppTitle {
            get;
            set;
        }

        #endregion App

        #region Branding

        /// <summary>
        /// Theme key of the user tenant. (Yes)
        /// </summary>
        public string ThemeKey {
            get;
            set;
        }

        /// <summary>
        /// User tenant timezone. (Yes)
        /// </summary>
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        /// Users tenant region. (Yes)
        /// </summary>
        public string Region {
            get;
            set;
        }

        /// <summary>
        /// USers tenatn languge. (Yes)
        /// </summary>
        public string Language {
            get;
            set;
        }

        /// <summary>
        /// Currency of the tenant. (Yes)
        /// </summary>
        public string Currency {
            get;
            set;
        }

        /// <summary>
        /// Date tiem formate for the tenant. (Yes)
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }

        public string TopHeaderLeftLogoUrl {
            get;
            set;
        }

        public string TopHeaderLeftName {
            get;
            set;
        }
        public string TopHeaderLeftPortalName {
            get;
            set;
        }
        public string LeftPoweredBy {
            get;
            set;
        }
        public string ContentCopyrightText {
            get;
            set;
        }

        #endregion Branding

        public long TimeTakenInMiliseconds {
            get;
            set;
        }

    }
}
