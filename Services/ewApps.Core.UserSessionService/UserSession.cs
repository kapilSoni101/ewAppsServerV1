using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.UserSessionService {

    [Table("UserSession", Schema = "core")]
    public class UserSession {
        [Key]
        public Guid ID {
            get; set;
        }

        [Required]
        [MaxLength(100)]
        public string UserName {
            get; set;
        }

        [Required]
        public Guid TenantUserId {
            get; set;
        }

        [Required]
        public Guid TenantId {
            get; set;
        }

        [Required]
        [MaxLength(100)]
        public string TenantName {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        [MaxLength(4000)]
        public string IdentityToken {
            get; set;
        }

        public int UserType {
            get;
            set;
        }

        [MaxLength(100)]
        public string Subdomain {
            get;
            set;
        }

        [MaxLength(100)]
        public string IdentityServerId {
            get;
            set;
        }
    }

}
