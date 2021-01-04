using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Setup application user list data trasfer object.
    /// </summary>
    public class TenantUserSetupListDTO {

        /// <summary>
        /// Tenant user identifier
        /// </summary>
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// Identity number for user identification.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// User Fullname
        /// </summary>
        public string FullName {
            get; set;
        }

        /// <summary>
        /// USer Email
        /// </summary>
        public string Email {
            get; set;
        }

        /// User application count
        public int ApplicationCount {
            get; set;
        }

        /// <summary>
        /// User deleted flag.
        /// </summary>
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// User deleted flag.
        /// </summary>
        public DateTime? DeletedOn {
            get; set;
        }

        /// <summary>
        /// User deleted flag.
        /// </summary>
        public string DeletedByName {
            get; set;
        }

    /// <summary>
    /// Permission bit of the user.
    /// </summary>
    [NotMapped]
    public long AdminUserPermissionBitMask
    {
      get; set;
    }
  }
}
