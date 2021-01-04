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
  public enum PlatformEmailPreferenceEnum :long {

    /// <summary>
    /// All the prefernece are false.
    /// </summary>
    None = 0,

    /// <summary>
    /// New Publisher Onboared.
    /// </summary>
    NewPublisherOnboared = 1,

    /// <summary>
    /// Publisher Active Inactive.
    /// </summary>
    PublisherActiveInactive = 2,

    /// <summary>
    /// My Permission Changed.
    /// </summary>
    MyPermissionChanged = 4,

    /// <summary>
    /// My ActiveInactive.
    /// </summary>
    MyActiveInactive = 8,

    /// <summary>
    /// New Platform User Joined.
    /// </summary>
    NewPlatformUserOnboard = 16,

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
    All = None | NewPublisherOnboared | PublisherActiveInactive | MyPermissionChanged 
               | MyActiveInactive | NewPlatformUserOnboard | TenantTicketStatusChanged 
               | TenantTicketReassigned | TenantTicketPriorityChanged | TenantTicketNewCommentAdded | TenantTicketAttachmentAdded

  }
}
