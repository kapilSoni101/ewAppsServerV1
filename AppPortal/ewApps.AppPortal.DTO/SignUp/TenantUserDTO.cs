using System;

namespace ewApps.AppPortal.DTO {
    public class TenantUserDTO {

        public Guid TenantUserId {
            get; set;
        }

        public Guid IdentityUserId {
            get; set;
        }

        public string FirstName {
            get; set;
        }

        public string LastName {
            get; set;
        }

        public string FullName {
            get; set;
        }

        public string Email {
            get; set;
        }

        public string Code {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public string Phone {
            get; set;
        }

        public Guid TenantId {
            get;
            set;
        }

        public bool Active {
            get;
            set;
        } = true;

        public long PermissionBitMask {
            get;
            set;
        }
    }
}
