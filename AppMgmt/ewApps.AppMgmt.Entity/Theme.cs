/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    /// <summary>
    /// Theme entity represting all the Theme related properties.
    /// </summary>
    [Table("Theme", Schema = "am")]
    public class Theme:BaseEntity {

        /// <summary>
        /// The Name of theme.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ThemeName {
            get; set;
        }

        /// <summary>
        /// The Url of Preview Image.
        /// </summary>
        [Required]
        public string PreviewImageUrl {
            get; set;
        }

        /// <summary>
        /// The Status of theme.
        /// </summary>
        [Required]
        public bool Active {
            get; set;
        }

        /// <summary>
        /// key of theme.
        /// </summary>
        [Required]
        public string ThemeKey {
            get; set;
        }

        /// <summary>
        /// Id of Tenant.
        /// </summary>
        [NotMapped]
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }
    }
}
