﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Gaurav Katiyar <gkatiyar@eworkplaceapps.com>
 * Date: 18 December 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 18 December 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity
{
    /// <summary>
    /// AppUserPreference table represting all the applications and tenant wise notification preferences data.
    /// </summary>
    /// <seealso cref="ewApps.Core.BaseService.BaseEntity" />
    [Table("TenantUserAppPreference", Schema = "be")]
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
    }
}
