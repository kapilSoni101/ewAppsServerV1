/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 07 July 2019 * 
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {
    [Table("ERPConnector", Schema ="be")]
    public class ERPConnector:BaseEntity {

        /// <summary>
        /// The name  of Connector.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Connector active status identifier.
        /// </summary>
        [Required]
        public bool Active {
            get; set;
        }

        public override Guid TenantId {
            get; set;
        }

        /// <summary>
        ///  For which connector set.
        /// </summary>
        [MaxLength(20)]
        public string ConnectorKey {
            get; set;
        }

    }
}
