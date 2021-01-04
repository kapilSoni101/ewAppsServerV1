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

namespace ewApps.AppPortal.Common {

  /// <summary>
  /// This class provides publisher prefernces list.
  /// </summary>
  [System.Flags]
  public enum PublisherEmailPreferenceEnum :long {

    /// <summary>
    /// All the prefernece are false.
    /// </summary>
    None = 0,

    /// <summary>
    /// New Tenant Onboard.
    /// </summary>
    NewTenantOnboard = 1,

    /// <summary>
    /// Tenant Active Inactive.
    /// </summary>
    TenantActiveInactive = 2,

    /// <summary>
    /// My Permissions Changed.
    /// </summary>
    MyPermissionsChanged = 4,

    /// <summary>
    /// My Active Inactive.
    /// </summary>
    MyActiveInactive = 8,

    /// <summary>
    /// New Platform User Joined.
    /// </summary>
    NewPublisherUserOnboard = 16,

    /// <summary>
    /// Tenant Ticket Status Changed.
    /// </summary>
    TenantTicketStatusChanged = 32,

    /// <summary>
    /// Tenant’s Ticket is reassigned.
    /// </summary>
    TenantTicketReassigned = 64,

    /// <summary>
    /// Tenant’s Ticket priority is changed.
    /// </summary>
    TenantTicketPriorityChanged = 128,

    /// <summary>
    /// New Comment added in Tenant’s Ticket.
    /// </summary>
    TenantTicketNewCommentAdded = 256,

    /// <summary>
    /// attachment added in Tenant’s Ticket.
    /// </summary>
    TenantTicketAttachmentAdded = 512,

    /// <summary>
    /// All the prefernece are true.
    /// </summary>
    All = None | NewTenantOnboard | TenantActiveInactive | MyPermissionsChanged | MyActiveInactive |
    NewPublisherUserOnboard | TenantTicketStatusChanged | TenantTicketReassigned | TenantTicketPriorityChanged | TenantTicketNewCommentAdded | TenantTicketAttachmentAdded

  }
}
