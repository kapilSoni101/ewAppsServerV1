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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("AppUserTypeLinking", Schema = "am")]
    public class AppUserTypeLinking:BaseEntity {

        /// <summary>
        /// User Type defines the portal of  the user.
        /// </summary>
        [Required]
        public int UserType {
            get; set;
        }

        /// <summary>
        /// The Id of Application
        /// </summary>
        [Required]
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// User Type defines the portal of  the partner.
        /// </summary>
        public int PartnerType {
            get; set;
        }

        /// <summary>
        /// The Id of Tenant
        /// </summary>
        [NotMapped]
        public Guid TenantId {
            get; set;
        }
    }
}
