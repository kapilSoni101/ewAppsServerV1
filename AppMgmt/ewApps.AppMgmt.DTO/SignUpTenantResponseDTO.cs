using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppMgmt.Entity;
using ewApps.Core.ExceptionService;

namespace ewApps.AppMgmt.DTO {
    public class TenantSignUpResponseDTO {

        /// <summary>
        /// The signed-up tenant information.
        /// </summary>
        public TenantDTO TenantInfo {
            get; set;
        }

        /// <summary>
        /// The signed-up tenant user information.
        /// </summary>
        public TenantUserDTO TenantUserInfo {
            get; set;
        }
    }
}
