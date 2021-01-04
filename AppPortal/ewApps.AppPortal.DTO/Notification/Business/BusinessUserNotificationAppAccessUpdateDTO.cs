using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {
    public class BusinessUserNotificationAppAccessUpdateDTO {

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
        /// Date
        /// </summary>
        [NotMapped]
        public DateTime ActionDate {
            get; set;
        }

        /// <summary>
        /// user Session
        /// </summary>
        [NotMapped]
        public UserSession userSession {
            get; set;
        }

        /// <summary>
        /// Publisher Name.
        /// </summary>
        public string UpdatedBy {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }
    }
}
