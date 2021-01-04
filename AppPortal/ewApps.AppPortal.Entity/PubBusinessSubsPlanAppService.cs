/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    /// <summary>
    /// This class contains all application services a publisher owned for their business.
    /// </summary>
    [Table("PubBusinessSubsPlanAppService", Schema = "ap")]
    public class PubBusinessSubsPlanAppService:BaseEntity {

        public Guid AppId {
            get; set;
        }

        [Required]
        public Guid AppServiceId {
            get; set;
        }

        [Required]
        public Guid AppServiceAttributeId {
            get; set;
        }

        [Required]
        public Guid PubBusinessSubsPlanId {
            get; set;
        }

        [Required]
        [MaxLength(200)]
        public string ServiceName {
            get; set;
        }

        [Required]
        [MaxLength(200)]
        public string ServiceAttributeName {
            get; set;
        }

        [Required]
        public Guid SubsPlanAppServiceId {
            get; set;
        }

        [Required]
        public Guid SubsPlanAppServiceAttributeId {
            get; set;
        }

    }
}
