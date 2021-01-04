// DBQuery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Contains business basic propereties to show in list.
    /// </summary>
    public class BusinessViewModelDQ: BaseDQ {
        public new Guid TenantId {
            get; set;
        }

        public new DateTime? CreatedOn {
            get; set;
        }
        public new DateTime? UpdatedOn {
            get; set;
        }
        public string IdentityNumber {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string VarId {
            get; set;
        }


        public int ApplicationCount {
            get; set;
        }

        public int TotalAdminUsers {
            get; set;
        }

        public int TotalNonAdminUsers {
            get; set;
        }

        public int TotalUser {
            get; set;
        }

        public string UpdatedByFullName {
            get; set;
        }

        /// <summary>
        /// Tenant active status identifier.
        /// </summary>
        public bool Active {
            get; set;
        }

        public string BackendERP {
            get; set;
        }

        public string TimeZone {
            get; set;
        }
    }
}
