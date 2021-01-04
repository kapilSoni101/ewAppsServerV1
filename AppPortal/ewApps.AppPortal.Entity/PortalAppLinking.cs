/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 20 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 20 August 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    /// <summary>
    /// This table represent linking of portal application with app.
    /// </summary>
    [Table("PortalAppLinking", Schema = "ap")]
    public class PortalAppLinking:BaseEntity {
        /// <summary>
        /// The Id of Application
        /// </summary>
        [Required]
        public Guid AppId {
            get; set;
        }
        /// <summary>
        /// The Id of Portal Application
        /// </summary>
        [Required]
        public Guid PortalId {
            get; set;
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