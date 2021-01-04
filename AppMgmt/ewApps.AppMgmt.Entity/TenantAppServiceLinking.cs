using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("TenantAppServiceLinking", Schema = "am")]
    public class TenantAppServiceLinking:BaseEntity {

        [Required]
        public Guid ServiceId {
            get; set;
        }

        [Required]
        public Guid ServiceAttributeId {
            get; set;
        }

        [Required]
        public Guid AppId {
            get; set;
        }

        // TenantId

    }
}
