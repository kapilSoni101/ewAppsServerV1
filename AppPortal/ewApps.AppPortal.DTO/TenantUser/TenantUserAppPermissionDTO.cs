using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class TenantUserAppPermissionDTO {

        /// <summary>
        /// The Unique Application Id.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// The name  of application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// Permissionbitmask of the user of the application.
        /// </summary>
        public long PermissionBitMask {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public int InvitationStatus {
            get; set;
        }

        public bool Assigned {
            get; set;
        }

        public DateTime? InvitedOn {
            get; set;
        }
        public DateTime? JoinedDate{
            get; set;
        }
        public string InvitedByName {
            get; set;
        }
    }
}
