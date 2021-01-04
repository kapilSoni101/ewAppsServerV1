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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("TenantLinking", Schema = "am")]
    public class TenantLinking:BaseEntity {

        public const string EntityName = "TenantLinking";

        /// <summary>
        /// The Id of Platfrom Tenant.
        /// </summary>
        public Guid? PlatformTenantId {
            get; set;
        }

        /// <summary>
        /// The Id of Publisher Tenant.
        /// </summary>
        public Guid? PublisherTenantId {
            get; set;
        }

        /// <summary>
        /// The Id of Business Tenant.
        /// </summary>
        public Guid? BusinessTenantId {
            get; set;

        }

        /// <summary>
        /// The Id of BusinessPartner Tenant.
        /// </summary>
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// The Id of Tenant not required in this table.
        /// </summary>
        [NotMapped]
        public override Guid TenantId {
            get; set;
        }
        
        /// <summary>
        /// Deleted Flag is not required in this flag.
        /// </summary>
        [NotMapped]
        public override bool Deleted {
            get; set;
        }

    }
}
