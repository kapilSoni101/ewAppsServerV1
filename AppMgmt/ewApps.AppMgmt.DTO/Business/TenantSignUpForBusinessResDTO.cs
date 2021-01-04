using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Created tenant information.
    /// </summary>
    public class TenantSignUpForBusinessResDTO {

        /// <summary>
        /// Publisher tenantid.
        /// </summary>
        public Guid PublisherTenantId {
            get; set;
        }

        /// <summary>
        /// Tenantid
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// Created by userid.
        /// </summary>
        public Guid CreatedBy {
            get; set;
        }

        /// <summary>
        /// craetedon
        /// </summary>
        public DateTime CreatedOn {
            get; set;
        }

        /// <summary>
        /// Business application id.
        /// </summary>
        public Guid BusinessApplicationId {
            get; set;
        }

        /// <summary>
        /// The signed-up tenant user information.
        /// </summary>
        public TenantUserDTO TenantUserInfo {
            get; set;
        }

        /// <summary>
        /// User details about app and its permissions.
        /// </summary>
        public List<UserAppRelationDTO> UserAppRelationDTOList {
            get; set;
        }

    }

}
