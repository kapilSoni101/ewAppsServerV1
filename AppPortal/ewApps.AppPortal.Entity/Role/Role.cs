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

namespace ewApps.AppPortal.Entity {

    /// <summary>
    /// A user role is a customizable set of rights and privileges. 
    /// Two roles are pre-defined in the system and cannot be edited or deleted 
    /// i.e. Administrator and System Administrator. Role table contains different 
    /// user role (rights and privileges) for users.
    /// System role will be identified by system field in role table.
    /// </summary>
    /// <remarks>
    /// Following fields are mandatory:
    /// <list type="bullet">  
    /// <item>RoleId (System generated)</item>
    /// <item>Name</item>
    /// <item>Active</item> 
    /// <item>CreatedBy</item> 
    /// <item>CreatedDate</item> 
    /// <item>ModifiedBy</item> 
    /// <item>ModifiedDate</item> 
    /// <item>TenantId</item>  
    /// <item>ApplicationId</item>
    /// </list>
    /// </remarks>
    [Table("Role", Schema = "ap")]
    public class Role:BaseEntity {

        public const string EntityName = "Role";

        /// <summary>
        /// Name of the role admin for admin else ID which is a guid.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string RoleName {
            get; set;
        }

        /// <summary>
        /// Admin for admin else same as name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string RoleKey {
            get; set;
        }

        /// <summary>
        /// PermissionBitMask is integer value set of all the permission of the users.
        /// </summary>
        [Required]
        public long PermissionBitMask {
            get; set;
        }

        /// <summary>
        /// Application identifier of the user.
        /// </summary>
        public Guid? AppId {
            get; set;
        }

        /// <summary>
        /// Active inactive status of the role.
        /// </summary>
        [Required]
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Defines the portal to which this role belongs.
        /// </summary>
        [Required]
        public int UserType {
            get; set;
        }

        /// <summary>
        /// Tenant identifier of the role this column is not required in this table beacause role is a master table does not belong to a particular tenant.
        /// </summary>
        [NotMapped]
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }

    }
}