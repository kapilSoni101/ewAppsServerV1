using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class TenantUserAndPermissionViewDTO {

        public Guid TenantUserId {
            get; set;
        }

        public string FirstName {
            get; set;
        }

        public string LastName {
            get; set;
        }

        public string Email {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public string CreatedBy {
            get; set;
        }

        public string Phone {
            get; set;
        }

        public DateTime? JoinedDate {
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
    }
}
