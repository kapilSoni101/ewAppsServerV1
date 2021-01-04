/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore
 * Last Updated On: 30 January 2019
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("UserTenantLinking", Schema = "am")]
    public class UserTenantLinking:BaseEntity {

        public const string EntityName = "UserTenantLinking";

        /// <summary>
        /// The Id of TenantUser for which this linking belowngs.
        /// </summary>
        [Required]
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// The Id of SubTenant if the user is of partner type.
        /// </summary>
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// User type of the user.
        /// </summary>
        [Required]
        public int UserType {
            get; set;
        }

        /// <summary>
        /// Partner type of the user.
        /// </summary>
        public int? PartnerType {
            get; set;
        }

        /// <summary>
        /// Tenant's primary user flag.
        /// </summary>
        public bool IsPrimary {
            get; set;
        }

    }
}
