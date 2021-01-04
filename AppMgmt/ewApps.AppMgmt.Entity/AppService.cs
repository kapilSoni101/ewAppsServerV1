/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 08 August 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {
    /// <summary>
    /// 
    /// </summary>
    [Table("AppService", Schema ="am")]
    public class AppService:BaseEntity {

        [Required]
        public string Name {
            get; set;
        }


        public Guid AppId {
            get; set;
        }

        public bool Active {
            get; set;
        }

        [MaxLength(20)]
        public string ServiceKey {
            get; set;
        }


        [NotMapped]
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }
    }
}
