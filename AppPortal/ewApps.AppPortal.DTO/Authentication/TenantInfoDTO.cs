// DbQuery

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
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    public class TenantInfoDTO:BaseDTO {

        public string SubDomainName {
            get; set;
        }
        /// <summary>Tenant identifier</summary>
        public new Guid TenantId {
            get; set;
        }

        public string TenantName {
            get; set;
        }
        public Guid? PlatformTenantId {
            get; set;
        }
        public Guid? PublisherTenantId {
            get; set;
        }
        public Guid? BusinessTenantId {
            get; set;
        }
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        public bool Active {
            get; set;
        }
        public bool Deleted {
            get; set;
        }


    }
}
