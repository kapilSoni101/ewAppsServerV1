using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {

    [System.Flags]
    public enum PlatformUserPlatformAppPermissionEnum: long {

        /// <summary>
        /// Dont have any permission.
        /// </summary>
        None = 0,

        /// <summary>
        /// View Applications permission
        /// </summary>
        ViewApplications = 1,

        /// <summary>
        /// Manage Applications permission
        /// </summary>
        ManageApplications = 2,

        /// <summary>
        /// View Businesses permission
        /// </summary>
        ViewPublishers = 4,

        /// <summary>
        /// Manage Businesses permission
        /// </summary>
        ManagePublishers = 8,

        /// <summary>
        /// View business permission
        /// </summary>
        ViewBusinesses = 16,

        /// <summary>
        /// View subscription permission
        /// </summary>
        ViewSubscription = 32,

        /// <summary>
        /// Manage subscription permission
        /// </summary>
        ManageSubscription = 64,

        /// <summary>
        /// View Subscriptions permission
        /// </summary>
        AccessConnectors = 128,

        /// <summary>
        /// Manage Subscriptions permission
        /// </summary>
        AccessReports = 256,

        /// <summary>
        /// View Reports permission
        /// </summary>
        ManagePlatformPortalSettings = 512,

        /// <summary>
        /// View Portal Settings permission
        /// </summary>
        ViewTickets = 1024,

        /// <summary>
        /// Manage Portal Settings permission
        /// </summary>
        ManageTickets = 2048,

        /// <summary>
        /// Have aall permission
        /// </summary>
        All = None | ViewApplications | ManageApplications | ViewPublishers | ManagePublishers | ViewBusinesses | ViewSubscription | ManageSubscription  
            | AccessConnectors | AccessReports | ManagePlatformPortalSettings | ViewTickets | ManageTickets

    }
}
