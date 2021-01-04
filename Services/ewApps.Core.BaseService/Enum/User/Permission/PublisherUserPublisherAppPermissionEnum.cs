using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum PublisherUserPublisherAppPermissionEnum : long{
        /// <summary>
        /// Dont have any permission
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
        /// View Tenants permission
        /// </summary>
        ViewBusinesses = 4,

        /// <summary>
        /// Manage Tenants permission
        /// </summary>
        ManageBusinesses = 8,

        /// <summary>
        /// View Subscriptions permission
        /// </summary>
        ViewSubscriptions = 16,

        /// <summary>
        /// Access connector permission 
        /// </summary>
        AccessConnectors = 32,

        /// <summary>
        /// View Reports permission
        /// </summary>
        AccessReports = 64,

        /// <summary>
        /// Manage Publisher Portal Settings permission
        /// </summary>
        ManagePublisherPortalSettings = 128,

        /// <summary>
        /// View Tickets
        /// </summary>
        ViewTickets = 256,

        /// <summary>
        /// Manage Tickets
        /// </summary>
        ManageTickets = 512,

        /// <summary>
        /// Have aall permission
        /// </summary>
        All = None | ViewApplications | ManageApplications | ViewBusinesses | ManageBusinesses | ViewSubscriptions | AccessConnectors |
        AccessReports | ManagePublisherPortalSettings | ViewTickets | ManageTickets
    }
}
