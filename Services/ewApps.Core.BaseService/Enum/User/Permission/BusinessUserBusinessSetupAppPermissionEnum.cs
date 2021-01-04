using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum BusinessUserBusinessSetupAppPermissionEnum:long {

        /// <summary>
        /// Dont have any permission
        /// </summary>
        None = 0,

        /// <summary>
        /// View Business Partners permission
        /// </summary>
        ViewBusinessPartners = 1,

        /// <summary>
        /// Manage Business Partners permission
        /// </summary>
        ManageBusinessPartners = 2,

        /// <summary>
        /// View Business Users  permission
        /// </summary>
        ViewBusinessUsers = 4,

        /// <summary>
        /// Manage Business Users permission
        /// </summary>
        ManageBusinessUsers = 8,

        /// <summary>
        /// View Application Setup  permission
        /// </summary>
        ViewApplicationSetup = 16,

        /// <summary>
        /// Manage Application Setup permission
        /// </summary>
        ManageApplicationSetup = 32,

        /// <summary>
        /// Access Configuration permission
        /// </summary>
        AccessConfiguration = 64,

        /// <summary>
        /// Access Branding permission
        /// </summary>
        AccessBranding = 128,

        /// <summary>
        /// Have all permission
        /// </summary>
        All = None | ViewBusinessPartners | ManageBusinessPartners | ViewBusinessUsers | ManageBusinessUsers
            | ViewApplicationSetup | ManageApplicationSetup | AccessConfiguration | AccessBranding
    }
}
