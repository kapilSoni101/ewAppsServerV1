using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {
    public class PublisherPermissionNotificationDTO {

        [NotMapped]
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
        public string PlatformCompanyName {
            get; set;
        }

        /// <summary>
        /// Publisher Name.
        /// </summary>
        public string PublisherCompanyName {
            get; set;
        }

        /// <summary>
        /// Invitor User FullName.
        /// </summary>
        public string InvitorUserFullName {
            get; set;
        }

        [NotMapped]
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
        public string SubDomain {
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
        /// TimeZone
        /// </summary>
        public string TimeZone {
            get; set;
        }


        /// <summary>
        /// Date Time Format
        /// </summary>
        public string DateTimeFormat {
            get; set;
        }

        [NotMapped]
        /// <summary>
        /// Date Time Format
        /// </summary>
        public DateTime DateTime {
            get; set;
        }


    }
}
