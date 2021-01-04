using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum CustomerUserCustomerSetupPermissionEnum:long {

        /// <summary>
        /// Dont have any permission
        /// </summary>
        None = 0,

        /// <summary>
        /// view poral users
        /// </summary>
        ViewPortalUsers = 1,

        /// <summary>
        /// manage portal user permission
        /// </summary>
        ManagePortalUsers = 2,

        /// <summary>
        /// Manage configuration
        /// </summary>
        ManageConfiguration = 4,

        /// <summary>
        /// Having all the permissions
        /// </summary>
        All = None | ViewPortalUsers | ManagePortalUsers | ManageConfiguration
  }
}
