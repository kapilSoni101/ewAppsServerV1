/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 16 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 16 November 2018
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {

    [Table("TokenInfo", Schema = "ap")]
    public class TokenInfo:BaseEntity {

        [MaxLength(4000)]
        public string TokenData {
            get; set;
        }

        public DateTime CreatedDate {
            get; set;
        }

        public int TokenType {
            get; set;
        }

        public Guid TenantUserId {
            get; set;
        }

        [MaxLength(20)]
        public string AppKey {
            get; set;
        }

        public int UserType {
            get; set;
        }

        [NotMapped]
        public override DateTime? CreatedOn {
            get; set;
        }
        [NotMapped]
        public override Guid UpdatedBy {
            get; set;
        }
        [NotMapped]
        public override DateTime? UpdatedOn {
            get; set;
        }
        [NotMapped]
        public override bool Deleted {
            get; set;
        }

        [NotMapped]
        public override Guid CreatedBy {
            get; set;
        }

    }
}
