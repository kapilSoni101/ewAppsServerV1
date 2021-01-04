using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {

    /// <summary>
    /// Portal table represting all the portal applications.
    /// </summary>
    [Table("Portal", Schema = "ap")]
    public class Portal:BaseEntity {
        /// <summary>
        /// The name  of application.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Type of the user.
        /// </summary>
        public int UserType {
            get;
            set;
        }
        /// <summary>
        /// key of the portal application.
        /// </summary>
        [MaxLength(20)]
        public string PortalKey {
            get;
            set;
        }

        /// <summary>
        /// Tenant identifier.
        /// </summary>
        [NotMapped]
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }
    }
}
