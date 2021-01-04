/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Payment.Entity {

    /// <summary>
    /// It contains linking between User and Role.
    /// </summary>
    /// <remarks>
    /// Following fields are mandatory:
    /// <list type="bullet"> 
    /// <item>RoleLinkingId [System Generated]</item>  
    /// <item>UserId</item>    
    /// <item>RolePermissionId</item>   
    /// </list>
    /// </remarks>
    [Table("RoleLinking", Schema = "pay")]
    public class RoleLinking:BaseEntity {

        public const string EntityName = "RoleLinking";

        /// <summary>
        /// App user for which this role linking belongs to .
        /// </summary>
        [Required]
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// Role identifier of the user.
        /// </summary>
        [Required]
        public Guid RoleId {
            get; set;
        }

        /// <summary>
        /// App identifier of the user.
        /// </summary>
        [Required]
        public Guid AppId {
            get; set;
        }

    }
}
