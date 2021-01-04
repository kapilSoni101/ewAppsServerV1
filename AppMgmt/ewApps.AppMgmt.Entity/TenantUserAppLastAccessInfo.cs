/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 1 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 14 November 2018
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    /// <summary>
    /// This table stores user infomation about login info for a particualr application and tenant.
    /// </summary>
    [Table("TenantUserAppLastAccessInfo", Schema = "am")]
    public class TenantUserAppLastAccessInfo:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "TenantUserAppLastAccessInfo";

        /// <summary>
        /// Login DateTime of the application.
        /// </summary>
        [Required]
        public DateTime LoginDateTime {
            get; set;
        }

        /// <summary>
        /// TimeZone of the application.
        /// </summary>
        [MaxLength(20)]
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// Region of the application.
        /// </summary> 
        [MaxLength(20)]
        public string Region {
            get; set;
        }

        /// <summary>
        /// Browser of the application.
        /// </summary>  
        [MaxLength(20)]
        public string Browser {
            get; set;
        }

        /// <summary>
        /// Language of the application.
        /// </summary>   
        [MaxLength(20)]
        public string Language {
            get; set;
        }

        /// <summary>
        /// Application Unique identifier of application.
        /// </summary>    
        [Required]
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// Application User Unique identifier of application.
        /// </summary>
        [Required]
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// Delete flag of the application we do not need this column as we are hard deleting the rows of this table.
        /// </summary>
        [NotMapped]
        public override bool Deleted {
            get => base.Deleted;
            set => base.Deleted = value;
        }

    }
}