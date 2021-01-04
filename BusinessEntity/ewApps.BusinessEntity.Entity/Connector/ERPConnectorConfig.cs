/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    [Table("ERPConnectorConfig", Schema = "be")]
    public class ERPConnectorConfig:BaseEntity {


        [MaxLength(4000)]
        public string SettingJson {
            get; set;
        }

        [Required]
        public bool Active {
            get; set;
        }

        [Required]
        public Guid TenantId {
            get; set;
        }

        
        /// <summary>
        ///  For which connector app config set.
        /// </summary>
        [MaxLength(20)]
        public string ConnectorKey {
            get;set;
        }

        /// <summary>
        /// Contain connector status.
        /// </summary>
        [MaxLength(100)]
        public string Status {
            get;set;
        }

        /// <summary>
        /// Message
        /// </summary>
        [MaxLength(4000)]
        public string Message {
            get; set;
        }


    }
}
