using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class TenantUserAppManagmentDTO {

        public Guid RoleLinkingId {
            get; set;
        }

        public Guid TenantUserId {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public int UserType {
            get; set;
        }

        public long PermissionBitMask {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        public bool Admin {
            get; set;
        }

        /// <summary>
        /// Email preference.
        /// </summary>
        public long EmailPreference {
            get; set;
        }

        /// <summary>
        /// AS preference.
        /// </summary>
        public long ASPreference {
            get; set;
        }

        /// <summary>
        /// SMS preference.
        /// </summary>
        public long SMSPreference {
            get; set;
        }
    }
}
