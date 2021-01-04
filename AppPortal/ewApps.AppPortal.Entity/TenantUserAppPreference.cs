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

namespace ewApps.AppPortal.Entity
{

    /// <summary>
    /// AppUserPreference table represting all the applications and tenant wise notification preferences data.
    /// </summary>
    [Table("TenantUserAppPreference", Schema = "ap")]
    public class TenantUserAppPreference : BaseEntity
    {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "TenantUserAppPreference";

        /// <summary>
        /// TenantUser Identifier.
        /// </summary>
        [Required]
        public Guid TenantUserId
        {
            get; set;
        }

        /// <summary>
        /// App to which this linking belowngs to.
        /// </summary>
        [Required]
        public Guid AppId
        {
            get; set;
        }

        /// <summary>
        /// Email preference bit mask of the user for the application in this tenant.
        /// </summary>
        [Required]
        public long EmailPreference
        {
            get; set;
        }

        /// <summary>
        /// AS preference bit mask of the user for the application in this tenant.
        /// </summary>
        [Required]
        public long ASPreference
        {
            get; set;
        }

        /// <summary>
        /// SMS preference bit mask of the user for the application in this tenant.
        /// </summary>
        [Required]
        public long SMSPreference
        {
            get; set;
        }

        /// <summary>
        /// ID identifier.
        /// </summary>
        public Guid ID
        {
            get; set;
        }

        /// <summary>
        /// CreatedBy identifier.
        /// </summary>
        public Guid CreatedBy
        {
            get; set;
        }

        /// <summary>
        /// CreatedOn identifier.
        /// </summary>
        public DateTime CreatedOn
        {
            get; set;
        }

        /// <summary>
        /// UpdatedBy identifier.
        /// </summary>
        public Guid UpdatedBy
        {
            get; set;
        }

        /// <summary>
        /// UpdatedOn identifier.
        /// </summary>
        public DateTime UpdatedOn
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is deleted.
        /// </summary>
        public Boolean Deleted
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        public Guid TenantId
        {
            get; set;
        }
    }
}
