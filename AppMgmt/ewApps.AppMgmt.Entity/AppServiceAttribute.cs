/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur<rthakur@eworkplaceapps.com>
 * Date: 10 January 2019
 * 
 * Contributor/s: Sourabh Agrawal 
 * Last Updated On: 08 August 2019
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("AppServiceAttribute", Schema = "am")]
    public class AppServiceAttribute:BaseEntity {

        [MaxLength(100)]
        public string Name {
            get; set;
        }

        public Guid AppServiceId {
            get; set;
        }

        public bool Active {
            get; set;
        }

        [MaxLength(20)]
        public string AttributeKey {
            get; set;
        }


        [NotMapped]
        public override Guid TenantId {
            get;
            set;
        }
    }
}
