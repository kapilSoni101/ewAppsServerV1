using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.Common {

    /// <summary>
    /// This enum is for app management entity type enum.
    /// 1 - 200 value is reserved for App management entity type enums.
    /// </summary>
    public enum AppMgmtEntityTypeEnum {

        /// <summary>
        /// Tenant entity type enum.
        /// </summary>
        Tenant = 1,

        /// <summary>
        /// Tenant User entity type enum.
        /// </summary>
        TenantUser = 2
    }
}
