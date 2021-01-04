/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

namespace ewApps.Report.Common {

  /// <summary>
  /// This class provides publisher permission list.
  /// </summary>
  [System.Flags]
  public enum PublisherPermissionEnum:long {

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
    ViewTenants = 4,

    /// <summary>
    /// Manage Tenants permission
    /// </summary>
    ManageTenants = 8,

    /// <summary>
    /// View Subscriptions permission
    /// </summary>
    ViewSubscriptions = 16,

    /// <summary>
    /// Manage Subscriptions permission
    /// </summary>
    ManageSubscriptions = 32,

    /// <summary>
    /// Access connector permission 
    /// </summary>
    AccessConnectors = 64,

    /// <summary>
    /// View Reports permission
    /// </summary>
    AccessReports = 128,

    /// <summary>
    /// View Publisher Portal Settings permission
    /// </summary>
    ViewPublisherPortalSettings = 256,

    /// <summary>
    /// Manage Publisher Portal Settings permission
    /// </summary>
    ManagePublisherPortalSettings = 512,

    /// <summary>
    /// View Tickets
    /// </summary>
    ViewTickets = 1024,

    /// <summary>
    /// Manage Tickets
    /// </summary>
    ManageTickets = 2048,

    /// <summary>
    /// Have aall permission
    /// </summary>
    All = None | ViewApplications | ManageApplications | ViewTenants | ManageTenants | ViewSubscriptions | ManageSubscriptions | AccessConnectors |
    AccessReports | ViewPublisherPortalSettings | ManagePublisherPortalSettings | ViewTickets | ManageTickets
  }
}