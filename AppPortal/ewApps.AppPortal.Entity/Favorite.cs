using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {

    /// <summary>
    /// Favorite entity represting all the Favorite related properties.
    /// </summary>
    [Table("Favorite", Schema = "ap")]
    public class Favorite:BaseEntity {

        /// <summary>
        /// TenantUserId
        /// </summary>        
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// MenuId
        /// </summary>
        [MaxLength(100)]
        public string MenuKey {
            get; set;
        }

        /// <summary>
        /// URL
        /// </summary>
        [MaxLength(500)]
        public string Url {
            get; set;
        }

        /// <summary>
        /// Portal key
        /// </summary>
        [MaxLength(100)]
        public string PortalKey {
            get; set;
        }

        /// <summary>
        /// AppId
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// System filed
        /// </summary>
        public bool System {
            get; set;
        }


    }
}
